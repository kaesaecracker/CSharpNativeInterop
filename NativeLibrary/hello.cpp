#include "hello.h"

#include <iostream>
#include "nativelibrary_export.h"

NATIVELIBRARY_EXPORT void nativelibrary::Hello() {
    std::cout << "Hello, World!" << std::endl;
}
