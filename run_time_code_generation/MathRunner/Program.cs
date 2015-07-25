using System;
using System.ServiceModel;

namespace MathRunner
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string baseAddress = "http://localhost:12345/MathOperations";
			using (var client = new MathOperationsServiceClient(new BasicHttpBinding(), new EndpointAddress(baseAddress)))
			{
				Console.WriteLine ("Click Enter to start...");
				Console.ReadLine ();
				Console.WriteLine (client.Increment (0));
				Console.WriteLine (client.Decrement (3));
				Console.WriteLine (client.SquareRoot (9));
			}
		}
	}
}
