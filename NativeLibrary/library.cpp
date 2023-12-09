#include "library.h"

#include <iostream>
#include "nativelibrary_export.h"

static PassMessageCallback PassMessagePtr = nullptr;

void PassMessage(const msgstr_t message)
{
    if (PassMessagePtr == nullptr)
        throw std::runtime_error("not initialized");
    PassMessagePtr(message);
}

NATIVELIBRARY_EXPORT StatusCode NativeLibrary_Initialize(PassMessageCallback message_callback)
{
    if (PassMessagePtr != nullptr )
        return -1; // already initialized
    if (message_callback == nullptr)
        return -2; // invalid args
    PassMessagePtr = message_callback;
    return 0;
}

NATIVELIBRARY_EXPORT void NativeLibrary_Test()
{
    PassMessage(u8"foo");
    PassMessage(u8"bar");
    PassMessage(u8"🤯");
}
