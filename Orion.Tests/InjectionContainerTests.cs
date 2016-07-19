using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Orion.Framework;
using Orion.Interfaces;
using Orion.Services;
using Orion.Services.Implementations;
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

	[TestClass]
	public class InjectionContainerTests
	{
		protected ServiceMap serviceMap;
		protected IKernel container;

		[TestInitialize]
		public void Setup()
		{
		}

		[TestMethod]
		public void TestDefaultBindings()
		{
			serviceMap = new ServiceMap();
			container = new StandardKernel(new Framework.Injection.ServiceInjectionModule(serviceMap));

			Assert.AreEqual(container.Get<ITileService>().GetType(), typeof(TileService));
		}

		[TestMethod]
		public void TestServiceOverride()
		{
			serviceMap = new ServiceMap();
			container = new StandardKernel(new Framework.Injection.ServiceInjectionModule(serviceMap));

			serviceMap.OverrideService<ITileService, TestTileService>(container);

			Assert.AreEqual(container.Get<ITileService>().GetType(), typeof(TestTileService));
		}
	}
}
