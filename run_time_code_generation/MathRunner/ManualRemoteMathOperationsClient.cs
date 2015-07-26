using System;
using System.ServiceModel;

namespace MathRunner
{
	public class ManualRemoteMathOperationsClient : ClientBase<IMathOperationsService>, IMathOperationsService
	{
		public delegate MathOperationsServiceClient ClientGenerator();

		private ClientGenerator _clientGenerator;
		private MathOperationsServiceClient _client;

		public ManualRemoteMathOperationsClient (ClientGenerator clientGenerator)
		{
			_clientGenerator = clientGenerator;
			_client = clientGenerator ();
		}

		private void RegenerateClientIfNeeded()
		{
			if (_client == null || _client.State == CommunicationState.Faulted) 
			{
				_client = _clientGenerator ();
			}
		}

		public int Increment(int n) 
		{
			try
			{
				return _client.Increment(n);
			}
			finally 
			{
				RegenerateClientIfNeeded ();
			}
		}

		public int Decrement(int n) 
		{
			try
			{
				return _client.Decrement(n);
			}
			finally 
			{
				RegenerateClientIfNeeded ();
			}
		}

		public double SquareRoot(double x) 
		{
			try
			{
				return _client.SquareRoot(x);
			}
			finally 
			{
				RegenerateClientIfNeeded ();
			}
		}

		public void Timeout(System.TimeSpan time) 
		{
			try
			{
				_client.Timeout(time);
			}
			finally 
			{
				RegenerateClientIfNeeded ();
			}
		}
	}
}

