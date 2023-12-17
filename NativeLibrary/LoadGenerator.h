#pragma once
#include <thread>

#include "CsLogger.h"

namespace nativelibrary {
    class LoadGenerator {
        std::thread load_thread;
        CsLogger* logger;
        volatile bool terminating = false;

        void background_function() const;

    public:
        explicit LoadGenerator(CsLogger* logger);

        ~LoadGenerator();
    };
}
