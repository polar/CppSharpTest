#include "Test.hpp"

int main(int argc, char** argv)
{
	Test s;
	printf("Size of std::string: %zd\n", sizeof(std::string));
	printf("Size of Test: %zd\n", sizeof(Test));
	s.print();
}