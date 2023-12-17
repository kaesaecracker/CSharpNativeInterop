#pragma once

#include <thread>

#include "logger.h"
#include "nativelibrary_export.h"

namespace nativelibrary {

    typedef int64_t MessageHandle;

    class NATIVELIBRARY_NO_EXPORT NativeLibrary {
        ILogger* logger;
        std::thread bg_thread;
        volatile bool terminating = false;

        void bg() const;
    public:
        explicit NativeLibrary(ILogger* logger);
        ~NativeLibrary();

        void Test() const;
    };

    NATIVELIBRARY_EXPORT NativeLibrary* NativeLibrary_Constructor(ILogger* logger);

    NATIVELIBRARY_EXPORT void NativeLibrary_Destructor(const NativeLibrary* instance);

    NATIVELIBRARY_EXPORT void NativeLibrary_Test(const NativeLibrary* instance);

}
