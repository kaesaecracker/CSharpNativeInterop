#include "library.h"

#include <iostream>
#include <thread>

#include "nativelibrary_export.h"

namespace nativelibrary {
    NativeLibrary::NativeLibrary(ILogger* logger) {
        this->logger = logger;
        bg_thread = std::thread(&NativeLibrary::bg, this);
    }

    NativeLibrary::~NativeLibrary() {
        terminating = true;

        std::cout << "waiting for thread " << bg_thread.native_handle() << std::endl;
        bg_thread.join();
    }

    void NativeLibrary::Test() const {
    }

    void NativeLibrary::bg() const {
        while (!terminating) {
            logger->log_info(u8"foo");
            logger->log_info(u8"bar");
            logger->log_info(u8"🤯");
        }
    }

    NATIVELIBRARY_EXPORT NativeLibrary* NativeLibrary_Constructor(ILogger* logger) {
        return new NativeLibrary(logger);
    }

    NATIVELIBRARY_EXPORT void NativeLibrary_Test(const NativeLibrary* instance) {
        instance->Test();
    }

    NATIVELIBRARY_EXPORT void NativeLibrary_Destructor(const NativeLibrary* instance) {
        delete instance;
    }
}
