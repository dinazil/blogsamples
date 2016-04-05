// This is the main DLL file.

#include "stdafx.h"

#include "..\native_processing\native_processor.h"

#include "WrappedProcessing.h"

bool WrappedProcessing::WrapperProcessor::IsPrime(int n)
{
	return NativeProcessor::isPrime(n);
}