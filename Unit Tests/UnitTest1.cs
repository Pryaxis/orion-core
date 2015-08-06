using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orion.Regions;

namespace Unit_Tests
{
	[TestClass]
	public class WindingNumberTest
	{
		PointCollection pc;

		[TestMethod]
		public void TestMethod1()
		{
			pc = new PointCollection();
			pc.AddPoint(0, 0);
			pc.AddPoint(0, 10);
			pc.AddPoint(10, 10);
			pc.AddPoint(10, 20);
			pc.AddPoint(15, 15);
			pc.AddPoint(30, 30);
			pc.AddPoint(30, 20);
			pc.AddPoint(20, 10);
			pc.AddPoint(15, 5);
			pc.AddPoint(20, 0);

			Console.WriteLine(pc.IsInArea(5, 5));
			Console.WriteLine(pc.IsInArea(10, 10));
			Console.WriteLine(pc.IsInArea(25, 25));
			Console.WriteLine(pc.IsInArea(15, 16));
			Console.WriteLine(pc.IsInArea(20, 0));

			//Expected   |    Received
			//true            true
			//true            true
			//true            true
			//false           false
			//true            true
		}
	}
}
