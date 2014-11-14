#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <crtdbg.h>

#ifdef _DEBUG
#define DEBUG_CLIENTBLOCK   new( _CLIENT_BLOCK, __FILE__, __LINE__)
#else
#define DEBUG_CLIENTBLOCK
#endif // _DEBUG

#ifdef _DEBUG
#define new DEBUG_CLIENTBLOCK
#endif

int* globalLeak = new int[5];

int main()
{
	char* localLeak = new char[5];
	localLeak[0] = 'H';
	localLeak[1] = 'e';
	localLeak[2] = 'l';
	localLeak[3] = 'l';
	localLeak[4] = 'o';

	_CrtDumpMemoryLeaks();

	return 0;
}