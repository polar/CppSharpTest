#include "Test.hpp"

int main(int argc, char** argv)
{
	Test s;
	printf("Size of std::string: %d\n", sizeof(std::string));
	printf("Size of Test: %d\n", sizeof(Test));
	s.print();
}