using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace RpcClientGenerator
{
	public static class ClientGenerator
	{
		public static T GenerateRpcClient<T> (TimeSpan timeout) where T : class
		{
			string code = GenerateInterfaceWrapperCode<T> (); 
			var provider = CodeDomProvider.CreateProvider("CSharp");
			var parameters = new CompilerParameters ();
			parameters.ReferencedAssemblies.Add (typeof(T).Assembly.Location);
			var result = provider.CompileAssemblyFromSource (parameters, code);
			if (result.Errors.HasErrors) 
			{
				Console.WriteLine (code);
				Console.WriteLine ("Errors:");
				foreach (var error in result.Errors) 
				{
					Console.WriteLine (error);
				}
			}

			throw new NotImplementedException ();
		}

		private static string GenerateMethodCode(MethodInfo method)
		{
			string returnType = method.ReturnType.FullName;
			string name = method.Name;

			string parameterType = method.GetParameters ().Single ().ParameterType.FullName;
			return $"\tpublic {returnType} {name}({parameterType} parameter){Environment.NewLine}\t{{{Environment.NewLine}\t\treturn ({returnType})_client.ExecuteMethod(\"{name}\", parameter);{Environment.NewLine}\t}}{Environment.NewLine}";
		}

		private static string GeneratePropertiesCode()
		{
			return $"\tpublic TimeSpan Timeout {{ get; set; }} = TimeSpan.FromMinutes(4);{Environment.NewLine}";
		}


		private static string GeneratePrefixCode<T>()
		{
		
			string interfaceName = typeof(T).FullName;
			return $"using System;{Environment.NewLine}public class Client : {interfaceName}{Environment.NewLine}{{{Environment.NewLine}\tprivate readonly RpcClientGenerator.MockRpcClient _client = new RpcClientGenerator.MockRpcClient {{ Timeout = Timeout }};{Environment.NewLine}";
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

