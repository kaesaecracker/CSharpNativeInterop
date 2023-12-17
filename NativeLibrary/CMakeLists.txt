cmake_minimum_required(VERSION 3.27)
project(NativeLibrary)

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_INTERPROCEDURAL_OPTIMIZATION FALSE)
set(CMAKE_CXX_VISIBILITY_PRESET hidden)

add_library(NativeLibrary SHARED
        library.cpp
        logger.cpp
        shared.h
        exports.cpp
)

include(GenerateExportHeader)
generate_export_header(NativeLibrary)
target_include_directories(NativeLibrary PUBLIC "$<BUILD_INTERFACE:${CMAKE_CURRENT_BINARY_DIR}>")
