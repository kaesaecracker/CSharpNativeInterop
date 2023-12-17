#pragma once

#include <cstdint>
#include <string>

#include "shared.h"

namespace nativelibrary {
    enum LogLevel: int32_t {
        // shamelessly copied from dotnet sources
        // MIT licensed code begins here
        Trace = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Critical = 5,
        // MIT licensed code ends here
    };

    typedef void (*LogMethod)(LogLevel level, const char message[]);

    class CsLogger {
        LogMethod _log_method;

    public:
        explicit CsLogger(LogMethod log_method);

        void log(LogLevel level, const std::string& message) const;

        void log_debug(const std::string& message) const {
            log(Debug, message);
        }

        void log_info(const std::string& message) const {
            log(Information, message);
        }

        void log_error(const std::string& message) const {
            log(Error, message);
        }
    };
}
