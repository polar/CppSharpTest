#pragma once

#include <string>
#include <vector>

#include "Interop/CppSharp.hpp"

  struct _DLLEXPORT_ Test
  {
    Test();
    Test(const Test&);
    ~Test();

    std::string item1;
    std::string item2;
    std::string item3;
    std::string item4;

    unsigned long long addr()  const;

    unsigned long long aItem1();
    unsigned long long aItem2();
    unsigned long long aItem3();
    unsigned long long aItem4();

    int oItem1();
    int oItem2();
    int oItem3();
    int oItem4();

    void print();

    static Test getTest();
    static Test &getStaticTest();
  };
