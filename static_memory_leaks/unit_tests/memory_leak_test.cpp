#include <boost/test/unit_test.hpp>

int* globalLeak = new int[5];

BOOST_AUTO_TEST_SUITE(MemoryLeakTests)

BOOST_AUTO_TEST_CASE(DummyTest)
{
	char* localLeak = new char[5];
	localLeak[0] = 'H';
	localLeak[1] = 'e';
	localLeak[2] = 'l';
	localLeak[3] = 'l';
	localLeak[4] = 'o';

	BOOST_REQUIRE(true);
}

BOOST_AUTO_TEST_SUITE_END()