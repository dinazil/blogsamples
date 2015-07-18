using System;
using System.Linq;
using System.Reflection;

namespace RpcClientGenerator
{
	public static class ClientGenerator
	{
		public static T GenerateRpcClient<T> (TimeSpan timeout) where T : class
		{
			throw new NotImplementedException ();
		}

		private static string GenerateMethodCode(MethodInfo method)
		{
			throw new NotImplementedException ();
		}

		private static string GeneratePropertiesCode()
		{
			return @"public TimeSpan Timeout { get; set; }";
		}

		private static string GeneratePrefixCode<T>()
		{
			string interfaceName = typeof(T).Name;
			return @"
					public class Client : ${interfaceName}
					{
					";
		}

		private static string GenerateSuffixCode()
		{
			return @"
					}";
		}

		private static string GenerateInterfaceWrapperCode<T>()
		{
			string start = GeneratePrefixCode<T> ();
			string properties = GeneratePropertiesCode ();
			string end = GenerateSuffixCode ();

			var methodInfos = typeof(T).GetMethods (BindingFlags.Public | BindingFlags.Instance);
			string methods = string.Join(Environment.NewLine, methodInfos.Select(GenerateMethodCode));

			return "${start} ${properties} ${methods} ${end}";
		}
	}
}

