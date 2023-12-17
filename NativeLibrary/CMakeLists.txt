cmake_minimum_required(VERSION 3.27)
project(NativeLibrary)

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(CMAKE_INTERPROCEDURAL_OPTIMIZATION FALSE)
set(CMAKE_CXX_VISIBILITY_PRESET hidden)

# force UTF-8 on windows
add_compile_options("$<$<C_COMPILER_ID:MSVC>:/utf-8>")
add_compile_options("$<$<CXX_COMPILER_ID:MSVC>:/utf-8>")

include(FetchContent)
FetchContent_Declare(fmt
        GIT_REPOSITORY https://github.com/fmtlib/fmt.git
        GIT_TAG 10.1.1
        OVERRIDE_FIND_PACKAGE
)
FetchContent_MakeAvailable(fmt)
find_package(fmt REQUIRED)

add_library(NativeLibrary SHARED
        CsLogger.cpp
        shared.h
        exports.cpp
        LoadGenerator.cpp
        LoadGenerator.h
)
target_link_libraries(NativeLibrary PRIVATE fmt::fmt-header-only)

include(GenerateExportHeader)
generate_export_header(NativeLibrary)
target_include_directories(NativeLibrary PUBLIC "$<BUILD_INTERFACE:${CMAKE_CURRENT_BINARY_DIR}>")
