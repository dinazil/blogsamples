using System;
using System.CodeDom.Compiler;
using System.IO;
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

		private static string GeneratePrefixCode<T>()
		{
			string interfaceName = typeof(T).FullName;

			var code = GetFormattingString("prefix");
			return code.Replace ("{interfaceName}", interfaceName);
		}

		private static string GenerateMethodCode(MethodInfo method)
		{
			string returnType = method.ReturnType.FullName;
			string methodNname = method.Name;
			string parameterType = method.GetParameters ().Single ().ParameterType.FullName;

			var code = GetFormattingString("method");
			code = code.Replace ("{returnType}", returnType);
			code = code.Replace ("{methodName}", methodNname);
			return code.Replace ("{parameterType}", parameterType);
		}

		private static string GenerateSuffixCode()
		{
			return GetFormattingString("suffix");
		}

		private static string GenerateInterfaceWrapperCode<T>()
		{
			string start = GeneratePrefixCode<T> ();

			string end = GenerateSuffixCode ();

			var methodInfos = typeof(T).GetMethods (BindingFlags.Public | BindingFlags.Instance);
			string methods = string.Join(Environment.NewLine, methodInfos.Select(GenerateMethodCode));

			return $"{start}{Environment.NewLine}{methods}{Environment.NewLine}{end}";
		}

		private static string GetFormattingString(string resource)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "RpcClientGenerator.Resources." + resource + ".txt";

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}
	}
}

