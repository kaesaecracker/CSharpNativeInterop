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

    typedef void (*LogMethod)(LogLevel level, const interopstr_t message);

    class CsLogger {
        LogMethod _log_method;

    public:
        explicit CsLogger(LogMethod log_method);

        void log_debug(const interopstr_t message) const;

        void log_info(const interopstr_t message) const;

        void log_error(const interopstr_t message) const;
    };
}
