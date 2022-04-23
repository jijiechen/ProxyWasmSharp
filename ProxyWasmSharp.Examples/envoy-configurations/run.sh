#!/bin/bash

docker run -it --rm \
  -v /mnt/c/Users/jijie/Projects/ProxyWasmSharp/ProxyWasmSharp.Examples/:/var/csharp-examples/ \
  -p 8080:8080 -p 8443:8443 \
  envoyproxy/envoy:v1.22.0 -l debug -c /var/csharp-examples/envoy-configurations/static.yaml
  
  
   
