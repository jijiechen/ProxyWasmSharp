
using System.Text.RegularExpressions;
using WebAssembly;
using Xunit;

namespace ProxyWasmSharp.Tests
{

    public class UnitTest1
    {
        

        [Fact]
        public void Test1()
        {
            var stubFuncs = new List<StubFunction>()
            {
                new() { Name = "fd_fdstat_set_flags" },
                new() { Name = "poll_oneoff" },
                new() { Name = "fd_filestat_get" },
                new() { Name = "clock_res_get" },
                new() { Name = "fd_filestat_set_size" },
                new() { Name = "fd_pread" },
                new() { Name = "path_open" },
                new() { Name = "path_filestat_get" },
                new() { Name = "path_readlink" },
                new() { Name = "fd_prestat_get" },
                new() { Name = "fd_prestat_dir_name" },
                new() { Name = "fd_tell" },
                new() { Name = "path_unlink_file" },
            };

            var module = Module.ReadFromBinary(@"C:\Users\jijie\Projects\ProxyWasmSharp\ProxyWasmSharp.Examples\bin\Debug\net7.0\ProxyWasmSharp.Examples.wasm");
            
            foreach (var stubFunc in stubFuncs)
            {
                stubFunc.ImportMatcher = CreateFunctionMatcher(stubFunc.Name);
            }
            
            // C:\Users\jijie\Downloads\wabt-1.0.24\bin\wasm2wat.exe C:\Users\jijie\Projects\ProxyWasmSharp\ProxyWasmSharp.Examples\bin\Debug\net7.0\ProxyWasmSharp.Examples.wasm --output C:\Users\jijie\Projects\ProxyWasmSharp\ProxyWasmSharp.Examples\bin\Debug\net7.0\ProxyWasmSharp.Examples.wasm.txt
            // C:\Users\jijie\Downloads\wabt-1.0.24\bin\wat2wasm.exe C:\Users\jijie\Projects\ProxyWasmSharp\ProxyWasmSharp.Examples\bin\Debug\net7.0\converted.wasm.txt --output C:\Users\jijie\Projects\ProxyWasmSharp\ProxyWasmSharp.Examples\bin\Debug\net7.0\converted.wasm
            var wasmTxtPath = @"C:\Users\jijie\Projects\ProxyWasmSharp\ProxyWasmSharp.Examples\bin\Debug\net7.0\ProxyWasmSharp.Examples.wasm.txt";
            var newWasmTxtPath = @"C:\Users\jijie\Projects\ProxyWasmSharp\ProxyWasmSharp.Examples\bin\Debug\net7.0\converted.wasm.txt";
            using var oldFS = File.OpenRead(wasmTxtPath);
            using var newFS = File.OpenWrite(newWasmTxtPath);
            
            CopyWABT(oldFS, newFS, stubFuncs, module);
        }


        void CopyWABT(Stream input, Stream output, List<StubFunction> stubFunctions, Module module)
        {
            using var reader = new StreamReader(input);
            using var writer = new StreamWriter(output);
            var line = reader.ReadLine();
            var funcSectionStarted = false;
            while (line != null)
            {
                if (!funcSectionStarted && line.StartsWith("  (func "))
                {
                    funcSectionStarted = true;
                    // 将需要生成的 stub func 输出到新文件
                    foreach (var stubFunction in stubFunctions)
                    {
                        if (!string.IsNullOrEmpty(stubFunction.FunctionBody))
                        {
                            writer.WriteLine(stubFunction.FunctionBody);
                        }
                    }
                }

                if (!funcSectionStarted)
                {
                    var firstMatched = stubFunctions.FirstOrDefault(f =>
                    {
                        if (!string.IsNullOrEmpty(f.FunctionBody))
                        {
                            return false;
                        }

                        var matchResult = f.ImportMatcher.Match(line);
                        if (!matchResult.Success)
                        {
                            return false;
                        }

                        var typeIndex = uint.Parse(matchResult.Groups["type_index"].Value);
                        var funcSignature = module.Types[(int)typeIndex];
                        f.InitFunctionBody(matchResult.Groups["func_name"].Value, typeIndex, funcSignature);
                        return true;
                    });

                    if (firstMatched == null)
                    {
                        writer.WriteLine(line);
                    }
                }
                else
                {
                    // 过了 import 之后，直接完整复制到 output 即可
                    writer.WriteLine(line);
                }
                line = reader.ReadLine();
            }
        }

        Regex CreateFunctionMatcher(string name)
        {
            const string matchTemplate = @"\s\s\(import\s""wasi_snapshot_preview1""\s""{0}""\s\(func\s(?<func_name>[^\s]+)\s\(type\s(?<type_index>\d+)\)\)\)";
            var funcMatcher = string.Format(matchTemplate, name);
            return new Regex(funcMatcher, RegexOptions.Compiled);
        }
    }


    class StubFunction
    {
        public string Name { get; set; }
        
        public string FunctionBody { get; private set; }
        
        public Regex ImportMatcher { get; set; }
        

        public void InitFunctionBody(string funcName, uint typeIndex, WebAssemblyType funcSignature)
        {
            var paramString = "";
            if (funcSignature.Parameters.Count > 0)
            {
                var paramListString = string.Join(" ", funcSignature.Parameters.Select(ToWasmTypeString));
                paramString = $"(param {paramListString}) ";
            }
            
            const string unSupportedTemplate = @"  (func {0} (type {1}) {2}(result i32)
    (local i32)
    i32.const 52
    local.set {3}
    local.get {3}
    return)";

            FunctionBody = string.Format(unSupportedTemplate, funcName, typeIndex, paramString, funcSignature.Parameters.Count);
        }


        static string ToWasmTypeString(WebAssemblyValueType type)
        {
            switch (type)
            {
                case WebAssemblyValueType.Int32:
                    return "i32";
                case WebAssemblyValueType.Int64:
                    return "i64";
                case WebAssemblyValueType.Float32:
                    return "f32";
                case WebAssemblyValueType.Float64:
                    return "f64";
                default:
                    throw new System.InvalidCastException($"Unsupported wasm type: {type}");
            }
        }
    }
}