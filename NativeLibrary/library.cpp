#include "library.h"

#include <iostream>
#include <thread>

#include "nativelibrary_export.h"


class NATIVELIBRARY_NO_EXPORT NativeLibrary {
    PassMessageCallback pass_message_ptr = nullptr;
    std::thread bg_thread;
    volatile bool terminating = false;

    void bg() const {
        while (!terminating) {
            PassMessage(u8"foo");
            PassMessage(u8"bar");
            PassMessage(u8"🤯");
        }
    }

public:
    explicit NativeLibrary(const PassMessageCallback message_callback) {
        pass_message_ptr = message_callback;
        bg_thread = std::thread(&NativeLibrary::bg, this);
    }

    ~NativeLibrary() {
        terminating = true;

        std::cout << "waiting for thread " << bg_thread.native_handle() << std::endl;
        bg_thread.join();

        pass_message_ptr = nullptr;
    }

    void PassMessage(const msgstr_t message) const {
        if (pass_message_ptr == nullptr)
            throw std::runtime_error("not initialized");

        pass_message_ptr(message);
    }
};

NATIVELIBRARY_EXPORT NativeLibrary* NativeLibrary_Initialize(const PassMessageCallback message_callback) {
    const auto instance = new NativeLibrary(message_callback);
    std::cout << "created " << instance << std::endl;
    return instance;
}

NATIVELIBRARY_EXPORT void NativeLibrary_Test(const NativeLibrary* instance) {
}

NATIVELIBRARY_EXPORT void NativeLibrary_Deinitialize(const NativeLibrary* instance) {
    if (instance == nullptr)
        throw std::invalid_argument("instance null");

    std::cout << "deinititializing " << instance << std::endl;
    delete instance;
}
