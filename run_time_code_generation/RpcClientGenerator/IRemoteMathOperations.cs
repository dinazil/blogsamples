using System;

namespace RpcClientGenerator
{
	public interface IRemoteMathOperations
	{
		int Increment(int n);
		int Decrement(int n);
		double SquareRoot(double x);

		[RemoteProcedureName("Increment")]
		int increment(int n);
	}
}

