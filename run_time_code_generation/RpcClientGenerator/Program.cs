using System;
using System.CodeDom.Compiler;

namespace RpcClientGenerator
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var remoteClient = new MockMathOperationsClient ();
			var smartClient = ClientGenerator.GenerateRpcClient<IRemoteMathOperations> (remoteClient);
			Console.WriteLine (smartClient.Decrement (1));
			Console.WriteLine (smartClient.Increment (0));
			Console.WriteLine (smartClient.SquareRoot (4));
		}
	}
}
