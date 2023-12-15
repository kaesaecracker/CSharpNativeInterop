#pragma once

#include <string>

#include "nativelibrary_export.h"

typedef int64_t MessageHandle;
typedef char8_t msgstr_t[];

typedef MessageHandle (*PassMessageCallback)(const msgstr_t message);

class NATIVELIBRARY_NO_EXPORT NativeLibrary;

NATIVELIBRARY_EXPORT NativeLibrary* NativeLibrary_Initialize(PassMessageCallback message_callback);

NATIVELIBRARY_EXPORT void NativeLibrary_Deinitialize(const NativeLibrary* instance);

NATIVELIBRARY_EXPORT void NativeLibrary_Test(const NativeLibrary* instance);
