namespace ProxyWasmSharp.Sdk.Internals;

internal enum MapType : int
{

    HttpRequestHeaders = 0,
    HttpRequestTrailers = 1,
    HttpResponseHeaders = 2,
    HttpResponseTrailers = 3,
    HttpCallResponseHeaders = 6,
    HttpCallResponseTrailers = 7,

}

internal enum BufferType : int
{
    HttpRequestBody = 0,
    HttpResponseBody = 1,
    DownstreamData = 2,
    UpstreamData = 3,
    HttpCallResponseBody = 4,
    GrpcReceiveBuffer = 5,
    VmConfiguration = 6,
    PluginConfiguration = 7,
    CallData = 8,
}


internal enum LogLevel : int
{
    Trace    = 0,
    Debug    = 1,
    Info     = 2,
    Warn     = 3,
    Error    = 4,
    Critical = 5,
    Max      = 6,
}

internal enum MetricType  : int
{
    Counter   = 0,
    Gauge     = 1,
    Histogram = 2
}


internal enum StreamType  : int
{

    Request    = 0,
    Response   = 1,
    Downstream = 2,
    Upstream   = 3,

}


internal enum Status  : int
{
    OK              = 0,
    NotFound        = 1,
    BadArgument     = 2,
    Empty           = 7,
    CasMismatch     = 8,
    InternalFailure = 10,
    Unimplemented   = 12,
}


