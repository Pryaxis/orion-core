using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orion.Framework;
using System.IO;

namespace Orion.Tests
{
	[TestClass]
	public class ServiceMapTest
	{

		[TestInitialize]
		public void Setup()
		{
		}

		[TestMethod]
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
