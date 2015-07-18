using System;

namespace RpcClientGenerator
{
	public class MockRpcClient
	{
		Random random = new Random();

		public MockRpcClient(string server, int port)
		{
			Console.WriteLine ("Creating client to {0}:{1}", server, port);
		}

		public TimeSpan Timeout { get; set; }

		public object ExecuteMethod(string methodName, object parameters)
		{
			Console.WriteLine("Executing {0}({1})", methodName, parameters);
			int choice = random.Next (100);
			switch (choice % 3) 
			{
			case 0:
				byte[] bytes = new byte[5];
				random.NextBytes (bytes);
				return bytes;
			case 1:
				return "great success";
			case 2:
				throw new Exception ("Some failure");
			}

			return null;
		}
	}
}

