using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orion.Net;
using Orion;

namespace Unit_Tests
{
	[TestClass]
	public class UnitTest1 : OrionPlugin
	{
		public override string Author
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int Order
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override Version Version
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override void Initialize()
		{
			throw new NotImplementedException();
		}

		protected UnitTest1(Orion.Orion instance)
			: base(instance)
		{
		}

		[TestMethod]
		public void TestMethod1()
		{
			
		}

		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}
	}
}
