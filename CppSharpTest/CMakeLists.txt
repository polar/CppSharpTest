cmake_minimum_required(VERSION 3.15)
project(PolarCppSharpTest)

set(CMAKE_CXX_STANDARD 14)

if (UNIX)
  set(PLATFORM linux)
  set(CMAKE_CXX_FLAGS ${CMAKE_CXX_FLAGS} -D__GLIBCXX_USE_CXX11_ABI=1)
elseif(WIN32)
  set(PLATFORM win)
endif()

add_library(ALK SHARED 
     Generated/${PLATFORM}/x64/ALK/ALK-symbols.cpp 
     CppAPI/Test.cpp
     )

     
add_library(Std-symbols SHARED 
     Generated/${PLATFORM}/x64/ALK/Std-symbols.cpp
     )

# Install up so that the libraries get placed in the executable directory.

set(CMAKE_INSTALL_PREFIX ..)
install(TARGETS ALK
                DESTINATION
                bin/Debug/netcoreapp3.1
                COMPONENT
                Libraries)

install(TARGETS Std-symbols
                DESTINATION
                bin/Debug/netcoreapp3.1
                COMPONENT
                Libraries)

add_executable(main CppAPI/Main.cpp)
target_link_libraries(main PRIVATE ALK)