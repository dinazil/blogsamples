using System;
using System.ServiceModel;

namespace MathRunner
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string baseAddress = "net.tcp://localhost:12346/MathOperations";
			using (var client = new MathOperationsServiceClient(
				new NetTcpBinding(), 
				new EndpointAddress(baseAddress)))
			{
				client.InnerChannel.OperationTimeout = TimeSpan.FromSeconds (2);

				Console.WriteLine ("Click Enter to start...");
				Console.ReadLine ();
				Console.WriteLine (client.Increment (0));
				Console.WriteLine (client.Decrement (3));
				Console.WriteLine (client.SquareRoot (9));

				try
				{
					Console.WriteLine (client.SquareRoot (-16));
				}
				catch (Exception ex)
				{
					Console.WriteLine (ex.Message); 
				}

				Console.WriteLine (client.SquareRoot (25));

				try
				{
					client.Timeout(TimeSpan.FromSeconds(5));
				}
				catch (Exception ex)
				{
					Console.WriteLine (ex.Message); 
				}

				Console.WriteLine (client.SquareRoot (36));

			}
		}
	}
}
