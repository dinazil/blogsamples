using System;
using System.Linq;
using System.Reflection;

namespace RpcClientGenerator
{
	public static class ClientGenerator
	{
		public static T GenerateRpcClient<T> (TimeSpan timeout) where T : class
		{
			string code = GenerateInterfaceWrapperCode<T> ();
			throw new NotImplementedException ();
		}

		private static string GenerateMethodCode(MethodInfo method)
		{
			string returnType = method.ReturnType.Name;
			string name = method.Name;
			string parameterType = method.GetParameters ().Single ().ParameterType.Name;
			return @"
					${returnType} ${name}(${parameterType} parameter)
					{
						return _client.ExecuteMethod(${name}, parameter, Timeout);
					}
			";
		}

		private static string GeneratePropertiesCode()
		{
			return @"public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(4)";
		}

		private static string GeneratePrefixCode<T>()
		{
			string interfaceName = typeof(T).Name;
			return @"
					public class Client : ${interfaceName}
					{
						private readonly RpcClientGenerator.MockClient _client { Timeout = Timeout };
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

