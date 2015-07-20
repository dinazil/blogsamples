using System;

namespace RpcClientGenerator
{
	public interface IRpcClient
	{
		TimeSpan Timeout { get; set; }

		object ExecuteMethod (string methodName, object parameters);
	}
}
	