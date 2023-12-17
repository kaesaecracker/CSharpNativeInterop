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

add_library(NativeLibrary SHARED
        CsLogger.cpp
        shared.h
        exports.cpp
        LoadGenerator.cpp
        LoadGenerator.h
)

include(GenerateExportHeader)
generate_export_header(NativeLibrary)
target_include_directories(NativeLibrary PUBLIC "$<BUILD_INTERFACE:${CMAKE_CURRENT_BINARY_DIR}>")
