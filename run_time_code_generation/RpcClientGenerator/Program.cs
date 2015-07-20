using System;
using System.CodeDom.Compiler;

namespace RpcClientGenerator
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var mclient = new ManualRemoteMathOperations (new MockMathOperationsClient());
			Console.WriteLine (mclient.Increment (1));
			Console.WriteLine (mclient.Decrement (3));
			Console.WriteLine (mclient.SquareRoot (9));

			var client = ClientGenerator.GenerateRpcClient<IRemoteMathOperations> (TimeSpan.FromSeconds(10));
		}
	}
}
