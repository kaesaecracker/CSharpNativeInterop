#include "logger.h"

namespace nativelibrary {
    ILogger::ILogger(const LogMethod log_method) {
        _log_method = log_method;
    }

    void ILogger::log_debug(const interopstr_t message) const {
        _log_method(Debug, message);
    }

    void ILogger::log_info(const interopstr_t message) const {
        _log_method(Information, message);
    }

    void ILogger::log_error(const interopstr_t message) const {
        _log_method(Error, message);
    }
}
