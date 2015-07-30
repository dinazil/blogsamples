using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MathRunner
{
	public class ManualRemoteMathOperationsClient : IMathOperationsService
	{
		public delegate IMathOperationsService ClientGenerator();

		private ClientGenerator _clientGenerator;
		private IMathOperationsService _client;

		public ManualRemoteMathOperationsClient (ClientGenerator clientGenerator)
		{
			_clientGenerator = clientGenerator;
			_client = clientGenerator ();
		}

		private void RegenerateClientIfNeeded()
		{
            var commObject = _client as ICommunicationObject;
			if (commObject == null || commObject.State == CommunicationState.Faulted) 
			{
                commObject.Abort();
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

