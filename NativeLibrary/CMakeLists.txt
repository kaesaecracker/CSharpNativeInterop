cmake_minimum_required(VERSION 3.27)
project(NativeLibrary)

set(CMAKE_CXX_STANDARD 23)

add_library(NativeLibrary SHARED
        library.cpp
        hello.cpp)

include(GenerateExportHeader)
generate_export_header(NativeLibrary)
target_include_directories(NativeLibrary PUBLIC "$<BUILD_INTERFACE:${CMAKE_CURRENT_BINARY_DIR}>")

