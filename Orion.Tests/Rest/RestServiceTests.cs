using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Orion.Configuration;
using Orion.Rest;
using Orion.Rest.AspNetCore;
using System;
using System.Net.Http;

namespace Orion.Tests.Rest
{
	[Route("api/v1/relay")]
	public class RelayController : Controller
	{
		[HttpGet]
		public IActionResult Get(string value)
		{
			return Ok(value);
		}

		public static string GetResponse(IRestService service, string value)
		{
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage responseMessage = client.GetAsync($"{service.BaseAddress}api/v1/relay?value={value}").Result;
				return responseMessage.Content.ReadAsStringAsync().Result;
			}
		}
	}

	[TestFixture]
	public class RestServiceTests
	{
		[Test]
		public void AspNetCoreConfiguration()
		{
			using (Orion orion = new Orion())
			{
				using (var service = orion.GetService<JsonFileConfigurationService<AspNetCoreConfiguration>>())
				{
					Assert.IsNotNull(service);
					Assert.IsNotNull(service.Configuration);
				}
			}
		}

		[TestCase("test")]
		public void IRestService_Start(string responseValue)
		{
			using (Orion orion = new Orion())
			{
				using (IRestService service = orion.GetService<IRestService>())
				{
					Assert.IsNotNull(service);
					Assert.IsNotNull(service.BaseAddress);

					string testResponse = RelayController.GetResponse(service, responseValue);
					Assert.IsNotNull(testResponse);
					Assert.AreEqual(responseValue, testResponse);
				}
			}
		}

		[Test]
		public void IRestService_Stop()
		{
			using (Orion orion = new Orion())
			{
				using (IRestService service = orion.GetService<IRestService>())
				{
					Assert.IsNotNull(service);
					Assert.IsNotNull(service.BaseAddress);

					service.Shutdown();

					Assert.Throws<AggregateException>(() => RelayController.GetResponse(service, String.Empty));
				}
			}
		}
	}
}
