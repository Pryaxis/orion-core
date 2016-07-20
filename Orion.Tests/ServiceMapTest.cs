using System;
using Orion.Framework;
using System.IO;
using NUnit.Framework;

namespace Orion.Tests
{
	[TestFixture]
	public class ServiceMapTest
	{
        
		[Test]
		public void TestServiceMap()
		{
			ServiceMap serviceMap = new ServiceMap();

			using (MemoryStream ms = new MemoryStream())
			using (StreamReader sw = new StreamReader(ms))
			{
				serviceMap.Save(ms);
				Assert.IsTrue(ms.ToArray().Length > 0);
				ms.Seek(0, SeekOrigin.Begin);
                
				System.IO.File.WriteAllBytes("services.json", ms.ToArray());
			}
		}
	}
}
