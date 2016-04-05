#include "native_processor.h"

bool NativeProcessor::isPrime(int n)
{
	if (n <= 1) return false;
	if (n == 2) return true;
	for (int i = 2; i < n; ++i) //inefficient on purpose
	{
		if (n % i == 0) return false;
	}
	return true;
}
