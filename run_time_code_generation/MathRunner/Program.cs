using System;
using System.ServiceModel;

namespace MathRunner
{
	class MainClass
	{
		private static IMathOperationsService Generator ()
		{
			string baseAddress = "net.tcp://localhost:12346/MathOperations";
			var client = new MathOperationsServiceClient (
				             new NetTcpBinding (), 
				             new EndpointAddress (baseAddress));
			client.InnerChannel.OperationTimeout = TimeSpan.FromSeconds (2);

			return client;
		}

		public static void Main (string[] args)
		{
			var client = new ManualRemoteMathOperationsClient (Generator);

			Console.WriteLine ("Click Enter to start...");
			Console.ReadLine ();
			Console.WriteLine (client.Increment (0));
			Console.WriteLine (client.Decrement (3));
			Console.WriteLine (client.SquareRoot (9));

			try {
				Console.WriteLine (client.SquareRoot (-16));
			} catch (Exception ex) {
				Console.WriteLine (ex.Message); 
			}

			Console.WriteLine (client.SquareRoot (25));

			try {
				client.Timeout (TimeSpan.FromSeconds (5));
			} catch (Exception ex) {
				Console.WriteLine (ex.Message); 
			}

			Console.WriteLine (client.SquareRoot (36));


		}
	}
}
