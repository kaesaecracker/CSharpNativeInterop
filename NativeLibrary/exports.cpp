#include <iostream>
#include <nativelibrary_export.h>

#include "LoadGenerator.h"
#include "CsLogger.h"

extern "C"
namespace nativelibrary
NATIVELIBRARY_EXPORT {
    CsLogger* CsLogger_Constructor(LogMethod log_method) {
        return new CsLogger(log_method);
    }

    void CsLogger_Destructor(const CsLogger* instance) {
        delete instance;
    }

    void print_hello_world() {
        std::cout << "Hello, World!" << std::endl;
    }

    LoadGenerator* LoadGenerator_Constructor(CsLogger* logger) {
        return new LoadGenerator(logger);
    }

    void LoadGenerator_Destructor(const LoadGenerator* instance) {
        delete instance;
    }
}
