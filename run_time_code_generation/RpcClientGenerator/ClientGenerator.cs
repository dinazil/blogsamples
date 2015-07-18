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
			Console.WriteLine (code);




			throw new NotImplementedException ();
		}

		private static string GenerateMethodCode(MethodInfo method)
		{
			string returnType = method.ReturnType.Name;
			string name = method.Name;
			string parameterType = method.GetParameters ().Single ().ParameterType.Name;
			return $"\t{returnType} {name}({parameterType} parameter){Environment.NewLine}\t{{{Environment.NewLine}\t\treturn _client.ExecuteMethod(\"{name}\", parameter, Timeout);{Environment.NewLine}\t}}{Environment.NewLine}";
		}

		private static string GeneratePropertiesCode()
		{
			return $"\tpublic TimeSpan Timeout {{ get; set; }} = TimeSpan.FromMinutes(4){Environment.NewLine}";
		}


		private static string GeneratePrefixCode<T>()
		{
			string interfaceName = typeof(T).Name;
			return $"public class Client : {interfaceName}{Environment.NewLine}{{{Environment.NewLine}\tprivate readonly RpcClientGenerator.MockClient _client {{ Timeout = Timeout }};{Environment.NewLine}";
		}

		private static string GenerateSuffixCode()
		{
			return "}\n";
		}

		private static string GenerateInterfaceWrapperCode<T>()
		{
			string start = GeneratePrefixCode<T> ();
			string properties = GeneratePropertiesCode ();
			string end = GenerateSuffixCode ();

			var methodInfos = typeof(T).GetMethods (BindingFlags.Public | BindingFlags.Instance);
			string methods = string.Join(Environment.NewLine, methodInfos.Select(GenerateMethodCode));

			return $"{start}{Environment.NewLine}{properties}{Environment.NewLine}{methods}{Environment.NewLine}{end}";
		}
	}
}

