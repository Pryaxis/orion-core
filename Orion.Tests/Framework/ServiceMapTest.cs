using NUnit.Framework;
using Orion.Framework;
using System.IO;

namespace Orion.Tests.Framework
{
	[TestFixture]
	public class ServiceMapTest
	{
		[Test]
		public void TestServiceMap()
		{
			var serviceMap = new ServiceMap();

			using (var ms = new MemoryStream())
			using (var sw = new StreamReader(ms))
			{
				serviceMap.Save(ms);
				Assert.IsTrue(ms.ToArray().Length > 0);
				ms.Seek(0, SeekOrigin.Begin);

				File.WriteAllBytes("services.json", ms.ToArray());
			}
		}
	}
}
