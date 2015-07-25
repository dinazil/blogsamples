using System;
using System.ServiceModel;

namespace MathOperationsServiceLibrary
{
	[ServiceContract]
	public interface IMathOperationsService
	{
		[OperationContract]
		int Increment(int n);

		[OperationContract]
		int Decrement(int n);

		[OperationContract]
		double SquareRoot(double x);

		[OperationContract]
		void Timeout(TimeSpan time);
	}
}
	