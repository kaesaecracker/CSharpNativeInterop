#include <iostream>
#include <nativelibrary_export.h>

#include "library.h"
#include "logger.h"

extern "C"
namespace NATIVELIBRARY_EXPORT nativelibrary {
     ILogger* ILogger_Constructor(LogMethod log_method) {
        return new ILogger(log_method);
    }

     void ILogger_Destructor(const ILogger* instance) {
        delete instance;
    }

    NativeLibrary* NativeLibrary_Constructor(ILogger* logger) {
        return new NativeLibrary(logger);
    }

    void NativeLibrary_Test(const NativeLibrary* instance) {
        instance->Test();
    }

    void NativeLibrary_Destructor(const NativeLibrary* instance) {
        delete instance;
    }

    void print_hello_world() {
        std::cout << "Hello, World!" << std::endl;
    }
}
