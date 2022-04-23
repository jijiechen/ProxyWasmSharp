#include <mono-wasi/driver.h>
#include <assert.h>
#include "dotnet_method.h"

DEFINE_DOTNET_METHOD(system_console_writeline, "System.Private.CoreLib.dll", "System", "Console", "WriteLine");

__attribute__((export_name("proxy_on_context_create")))
void proxy_on_context_create(uint32_t contextID, uint32_t pluginContextID) {
    system_console_writeline(NULL, (void* []) {"proxy_on_context_create"});
}

__attribute__((export_name("proxy_on_log")))
void proxy_on_log(uint32_t contextID) {
    system_console_writeline(NULL, (void* []) {"proxy_on_log"});
}

__attribute__((export_name("proxy_on_done")))
int proxy_on_done(uint32_t contextID) {
    system_console_writeline(NULL, (void* []) {"proxy_on_done"});
    return 1;
}

__attribute__((export_name("proxy_on_delete")))
void proxy_on_delete(uint32_t contextID) {
    system_console_writeline(NULL, (void* []) {"proxy_on_delete"});
}