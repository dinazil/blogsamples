using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedProcessing
{
	public static class ManagedProcessor
	{
		public static bool IsPrime(int n)
		{
			if (n <= 1) return false;
			if (n == 2) return true;
			for (int i = 2; i < n; ++i) //inefficient on purpose
			{
				if (n % i == 0) return false;
			}
			return true;
		}
	}
}
