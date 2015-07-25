using System;

namespace RpcClientGenerator
{
	public class ManualRemoteMathOperations : IRemoteMathOperations
	{
		private IRpcClient _client;

		public ManualRemoteMathOperations(IRpcClient client)
		{
			_client = client;
		}

		public TimeSpan Timeout
		{
			get { return _client.Timeout; }
			set 
			{
				_client.Timeout = value;
			}
		}

		public int Increment(int n)
		{
			return (int)_client.ExecuteMethod ("Increment", n);
		}

		public int increment(int n)
		{
			return (int)_client.ExecuteMethod ("Increment", n);
		}

		public int Decrement(int n)
		{
			return (int)_client.ExecuteMethod ("Decrement", n);
		}

		public double SquareRoot(double x)
		{
			return (double)_client.ExecuteMethod ("SquareRoot", x);
		}
	}
}

