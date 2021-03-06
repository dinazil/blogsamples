﻿using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using MathOperationsServiceLibrary;

namespace MathOperationsServiceHost
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Uri baseAddress = new Uri ("http://localhost:12345/MathOperations");
			using (ServiceHost host = new ServiceHost(typeof(MathOperationsService), baseAddress))
			{
				ServiceMetadataBehavior smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
				// If not, add one
				if (smb == null)
					smb = new ServiceMetadataBehavior();
				smb.HttpGetEnabled = true;
				host.Description.Behaviors.Add(smb);

				host.AddServiceEndpoint(
					typeof(IMetadataExchange),
					MetadataExchangeBindings.CreateMexHttpBinding(),
					"mex"
				);

				host.AddServiceEndpoint (typeof(IMathOperationsService),
					new BasicHttpBinding (), "");

				host.AddServiceEndpoint (typeof(IMathOperationsService),
					new NetTcpBinding (), "net.tcp://localhost:12346/MathOperations");

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
		}
	}
}

