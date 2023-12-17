#include "LoadGenerator.h"

#include <fmt/core.h>

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
        size_t tid = std::hash<std::thread::id>()(std::this_thread::get_id());

        logger->log_info(fmt::format("starting {}", tid));

        while (!terminating) {
            logger->log_debug(fmt::format("looping {}", tid));
        }

        logger->log_info(fmt::format("stopping {}", tid));
    }
}
