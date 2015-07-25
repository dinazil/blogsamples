using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using MathOperationsServiceLibrary;

namespace MathOperationsServiceHost
{
	public class Program
	{
		public static int Main(string[] args)
		{
			Uri baseAddress = new Uri ("http://localhost:12345/MathOperations");
			using (ServiceHost host = new ServiceHost(typeof(MathOperationsService), baseAddress))
			{
				host.AddServiceEndpoint(
					typeof(IMetadataExchange),
					MetadataExchangeBindings.CreateMexHttpBinding(),
					baseAddress + "/mex"
				);

				host.AddServiceEndpoint (typeof(IMathOperationsService),
					new BasicHttpBinding (),
					baseAddress);

				host.Open();

				Console.WriteLine("The service is ready at...");
				foreach (var ep in host.Description.Endpoints) 
				{
					Console.WriteLine ($"\t{ep.Address}");
				}
				Console.WriteLine("Press <Enter> to stop the service.");
				Console.ReadLine();

				host.Close();
			}

			return 0;
		}
	}
}

