#include "CsLogger.h"

namespace nativelibrary {
    CsLogger::CsLogger(const LogMethod log_method) {
        _log_method = log_method;
    }

    void CsLogger::log_debug(const interopstr_t message) const {
        _log_method(Debug, message);
    }

    void CsLogger::log_info(const interopstr_t message) const {
        _log_method(Information, message);
    }

    void CsLogger::log_error(const interopstr_t message) const {
        _log_method(Error, message);
    }
}
