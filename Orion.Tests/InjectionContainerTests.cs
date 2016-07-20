using System;
using Ninject;
using NUnit.Framework;
using Orion.Framework;
using Orion.Interfaces;
using Orion.Services;
using Orion.Services.Implementations;
using OTAPI.Tile;
using Terraria;
using NUnit.Framework;

namespace Orion.Tests
{
	public class TestTileService : ServiceBase, ITileService
	{
		public TestTileService(Orion orion) : base(orion)
		{
		}

		public ITile this[int x, int y]
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}
	}

	[TestFixture]
	public class InjectionContainerTests
	{
		protected ServiceMap serviceMap;
		protected IKernel container;


		[Test]
		public void TestDefaultBindings()
		{
			serviceMap = new ServiceMap();
			container = new StandardKernel(new Framework.Injection.ServiceInjectionModule(serviceMap));

			Assert.IsInstanceOf(typeof(TileService), container.Get<ITileService>());
		}

		[Test]
		public void TestServiceOverride()
		{
			serviceMap = new ServiceMap();
			container = new StandardKernel(new Framework.Injection.ServiceInjectionModule(serviceMap));

			serviceMap.OverrideService<ITileService, TestTileService>(container);

			Assert.IsInstanceOf(typeof(TestTileService), container.Get<ITileService>());
		}
	}
}
