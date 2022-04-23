using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ProxyWasmSharp.Sdk.Internals;

public class Imports
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_log(int logLevel, byte* messageData, int messageSize);  

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_send_local_response(uint statusCode, byte* statusCodeDetailData,
		int statusCodeDetailsSize, byte* bodyData, int bodySize, byte* headersData, int headersSize, int grpcStatus); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_get_shared_data(byte* keyData, int keySize, byte** returnValueData, int* returnValueSize, uint* returnCas); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_set_shared_data(byte* keyData, int keySize, byte* valueData, int valueSize, uint cas); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_register_shared_queue(byte* nameData, int nameSize, uint* returnID); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_resolve_shared_queue( byte* vmIDData, int vmIDSize,  byte* nameData, int nameSize, uint* returnID); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_dequeue_shared_queue (uint queueID, byte** returnValueData , int* returnValueSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_enqueue_shared_queue (uint queueID,  byte* valueData, int valueSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_get_header_map_value(MapType mapType,  byte* keyData, int keySize, byte** returnValueData , int* returnValueSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_add_header_map_value(MapType mapType,  byte* keyData, int keySize,  byte* valueData, int valueSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_replace_header_map_value(MapType mapType,  byte* keyData, int keySize,  byte* valueData, int valueSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_remove_header_map_value(MapType mapType,  byte* keyData, int keySize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_get_header_map_pairs(MapType mapType, byte** returnValueData , int* returnValueSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_set_header_map_pairs(MapType mapType,  byte* mapData, int mapSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_get_buffer_bytes(BufferType bufferType, int start, int maxSize, byte** returnBufferData , int* returnBufferSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_set_buffer_bytes(BufferType bufferType, int start, int maxSize,  byte* bufferData, int bufferSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_continue_stream(StreamType streamType); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_close_stream(StreamType streamType); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_http_call(byte* upstreamData, int upstreamSize, byte* headerData, 
		int headerSize, byte* bodyData, int bodySize, byte* trailersData, int trailersSize, uint timeout, uint* calloutIDPtr); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_call_foreign_function( byte* funcNamePtr, int funcNameSize,  byte* paramPtr, int paramSize, byte** returnData , int* returnSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_set_tick_period_milliseconds(uint period); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_set_effective_context(uint contextID); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_done(); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_define_metric(MetricType metricType,  byte* metricNameData, int metricNameSize, uint* returnMetricIDPtr); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_increment_metric (uint metricID, long offset); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_record_metric (uint metricID, ulong value ); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_get_metric (uint metricID, ulong* returnMetricValue); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_get_property( byte* pathData, int pathSize, byte** returnValueData , int* returnValueSize); 

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static unsafe extern Status proxy_set_property( byte* pathData, int pathSize,  byte* valueData, int valueSize); 
}