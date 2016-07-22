using System;
using Ninject;
using NUnit.Framework;
using Orion.Framework;
using Orion.Framework.Injection;
using Orion.Interfaces;
using Orion.Services;
using OTAPI.Tile;

namespace Orion.Tests
{
	public class TestTileService : ServiceBase, ITileService
	{
		public TestTileService(Orion orion) : base(orion)
		{
		}
		
		public ITile this[int x, int y]
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}
	}

	[TestFixture]
	public class InjectionContainerTests
	{
		[Test]
		public void TestDefaultBindings()
		{
			var serviceMap = new ServiceMap();
			var container = new StandardKernel(new ServiceInjectionModule(serviceMap));

			Assert.IsInstanceOf(typeof(TileService), container.Get<ITileService>());
		}

		[Test]
		public void TestServiceOverride()
		{
			var serviceMap = new ServiceMap();
			var container = new StandardKernel(new ServiceInjectionModule(serviceMap));

			serviceMap.OverrideService<ITileService, TestTileService>(container);

			Assert.IsInstanceOf(typeof(TestTileService), container.Get<ITileService>());
		}
	}
}
