#include "CsLogger.h"

namespace nativelibrary {
    CsLogger::CsLogger(const LogMethod log_method) {
        _log_method = log_method;
    }

    void CsLogger::log(const LogLevel level, const std::string& message) const {
        _log_method(level, message.c_str());
    }
}
