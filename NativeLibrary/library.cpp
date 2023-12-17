#include "library.h"

#include <iostream>
#include <thread>

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
        logger->log_debug(u8"test debug message");
    }

    void NativeLibrary::bg() const {
        while (!terminating) {
            logger->log_info(u8"foo");
            logger->log_info(u8"bar");
            logger->log_info(u8"🤯");
        }
    }
}
