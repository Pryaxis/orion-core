using NUnit.Framework;
using Orion.Configuration;
using Orion.Rest;
using Orion.Rest.Owin;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Orion.Tests.Rest
{
	public class RelayController : ApiController
	{
		IRestService _restService;

		public RelayController(IRestService restService)
		{
			_restService = restService;
			Assert.IsNotNull(_restService);
		}

		public IHttpActionResult Get(string value)
		{
			Assert.IsNotNull(_restService);
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
		public void OwinConfiguration()
		{
			using (Orion orion = new Orion())
			{
				using (IConfigurationService<OwinConfiguration> service = orion.GetService<JsonFileConfigurationService<OwinConfiguration>>())
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
					Assert.AreNotEqual(responseValue, testResponse);
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
