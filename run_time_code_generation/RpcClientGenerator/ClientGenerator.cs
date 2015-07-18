using System;
using System.Reflection;

namespace RpcClientGenerator
{
	public static class ClientGenerator
	{
		public static T GenerateRpcClient<T> () where T : class
		{
			throw new NotImplementedException ();
		}

		private static string GenerateMethodCode(MethodInfo method)
		{
			throw new NotImplementedException ();
		}

		private static string GenerateInterfaceWrapperCode<T>()
		{
			throw new NotImplementedException ();
		}
	}
}

