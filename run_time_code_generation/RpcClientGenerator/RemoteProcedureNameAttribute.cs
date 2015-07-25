using System;

namespace RpcClientGenerator
{
	[AttributeUsage(AttributeTargets.Method)]
	public class RemoteProcedureNameAttribute : Attribute
	{
		public string Name { get; set;}
		public RemoteProcedureNameAttribute (string name)
		{
			Name = name;
		}
	}
}
	