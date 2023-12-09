#pragma once

#include <cstdint>
#include <string>

#include "nativelibrary_export.h"

typedef int64_t StatusCode;
typedef int64_t MessageHandle;
typedef char8_t msgstr_t[];

typedef MessageHandle (*PassMessageCallback)(const msgstr_t message);

NATIVELIBRARY_EXPORT StatusCode NativeLibrary_Initialize(PassMessageCallback message_callback);

NATIVELIBRARY_EXPORT void NativeLibrary_Test();
