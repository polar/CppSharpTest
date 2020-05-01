#include "Test.hpp"

#include <iostream>
  unsigned long long Test::aItem1()
  {
      return (unsigned long long) &item1;
  }
  unsigned long long Test::aItem2()
  {
      return (unsigned long long) &item2;
  }
  unsigned long long Test::aItem3()
  {
      return (unsigned long long) &item3;
  }
  unsigned long long Test::aItem4()
  {
      return (unsigned long long) &item4;
  }

  unsigned long long Test::addr() const
  {
      return (unsigned long long)this;
  }

  int Test::oItem1()
  {
      return (int) (aItem1() - addr());
  }
  int Test::oItem2()
  {
      return (int) (aItem2() - addr());
  }
  int Test::oItem3()
  {
      return (int) (aItem3() - addr());
  }
  int Test::oItem4()
  {
      return (int) (aItem4() - addr());
  }

  Test::Test() : item1("item1"), item2("item2"), item3("item3"), item4("item4")
  {
      std::cout << " new Test() is at " << (void *) addr() << " : " << addr() << std::endl;
  }

  Test::~Test() = default;
  
  void Test::print()
  {
      std::cout << "Test is at    " << (void *) addr() << " : " << addr() << std::endl;
      std::cout << "Test:item1 at " << (void *) aItem1() << " : " << aItem1() << " at " << oItem1() << " is '" << item1 << "'" << std::endl;
      std::cout << "Test:item2 at " << (void *) aItem2() << " : " << aItem2() << " at " << oItem2() << " is '" << item2 << "'" << std::endl;
      std::cout << "Test:item3 at " << (void *) aItem3() << " : " << aItem3() << " at " << oItem3() << " is '" << item3 << "'" << std::endl;
      std::cout << "Test:item4 at " << (void *) aItem4() << " : " << aItem4() << " at " << oItem4() << " is '" << item4 << "'" << std::endl;
  }

  static Test staticTest;

  Test &Test::getStaticTest() {
      return staticTest;
  }

  Test Test::getTest()
  {
      Test eatme;
      eatme.print();
      return eatme;
  }

  Test::Test(const Test &t) :item1("copy1"), item2("copy2"), item3("copy3"), item4("copy4")
  {
    std::cout << "Test::CopyConstructor on " << (void *) t.addr() << " : " << t.addr()
              << " ==> " << (void *) addr() << " : " << addr() << std::endl;
  }
