#include "LoadGenerator.h"

namespace nativelibrary {
    LoadGenerator::LoadGenerator(CsLogger* logger) {
        this->logger = logger;
        load_thread = std::thread(&LoadGenerator::background_function, this);
    }

    LoadGenerator::~LoadGenerator() {
        terminating = true;
        load_thread.join();
    }

    void LoadGenerator::background_function() const {
        logger->log_info(u8"starting");

        while(!terminating) {
            logger->log_debug(u8"loop");
        }

        logger->log_info(u8"stopping");
    }
}
