using System;
using System.Threading;

namespace MathOperationsServiceLibrary
{
	public class MathOperationsService : IMathOperationsService
	{
		public int Increment (int n)
		{
			return n + 1;
		}

		public int Decrement (int n)
		{
			return n - 1;
		}

		public double SquareRoot (double x)
		{
			return Math.Sqrt (x);
		}

		public void Timeout (TimeSpan time)
		{
			Thread.Sleep ((int)time.TotalMilliseconds);
		}
	}
}

