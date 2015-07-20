using System;

namespace RpcClientGenerator
{
	public class MockMathOperationsClient : IRpcClient
	{
		public TimeSpan Timeout { get; set; }

		public object ExecuteMethod(string methodName, object parameters)
		{
			switch (methodName) 
			{
				case "Increment":
					return (int)parameters + 1;
				case "Decrement":
					return (int)parameters - 1;
				case "SquareRoot":
					return Math.Sqrt ((double)parameters);
			}
			throw new NotSupportedException ();
		}
	}
}

