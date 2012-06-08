using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eaa.LtaBlog.Application.RavenDb;
using Raven.Client.MvcIntegration;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Eaa.LtaBlog.Application.App_Start.RavenDb), "Start")]
namespace Eaa.LtaBlog.Application.App_Start
{
	public static class RavenDb
	{
		public static void Start()
		{
			RavenDbStore.Initialize();

			RavenProfiler.InitializeFor(RavenDbStore.DocumentStore, "HashedPassword", "PasswordSalt");
		}
	}
}