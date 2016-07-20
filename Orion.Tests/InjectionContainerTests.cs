using System;
using Ninject;
using Orion.Framework;
using Orion.Interfaces;
using Orion.Services;
using Terraria;
using NUnit.Framework;

namespace Orion.Tests
{
	public class TestTileService : ServiceBase, ITileService
	{
		public TestTileService(Orion orion) : base(orion)
		{
		}

		public Tile this[int x, int y]
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

			Assert.AreEqual(container.Get<ITileService>().GetType(), typeof(TileService));
		}

		[Test]
		public void TestServiceOverride()
		{
			serviceMap = new ServiceMap();
			container = new StandardKernel(new Framework.Injection.ServiceInjectionModule(serviceMap));

			serviceMap.OverrideService<ITileService, TestTileService>(container);

			Assert.AreEqual(container.Get<ITileService>().GetType(), typeof(TestTileService));
		}
	}
}
