#pragma once
#include <cstdint>
#include <nativelibrary_export.h>

#include "shared_types.h"

namespace nativelibrary {

    enum LogLevel: int32_t {
        // shamelessly copied from dotnet sources
        // MIT license begining here
        Trace = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Critical = 5,
        // MIT license ends here
    };

    typedef void (*LogMethod)(LogLevel level, const interopstr_t message);

    class NATIVELIBRARY_EXPORT ILogger {
        LogMethod _log_method;

    public:
        explicit ILogger(LogMethod log_method);

        void log_debug(const interopstr_t message) const;
        void log_info(const interopstr_t message) const;
        void log_error(const interopstr_t message) const;
    };

    NATIVELIBRARY_EXPORT ILogger* ILogger_Constructor(LogMethod log_method);
    NATIVELIBRARY_EXPORT void ILogger_Destructor(const ILogger* instance);
}
