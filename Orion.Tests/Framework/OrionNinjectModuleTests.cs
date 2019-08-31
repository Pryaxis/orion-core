namespace Orion.Tests.Framework {
    using System;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using Ninject;
    using Orion.Framework;
    using Xunit;

    public class OrionNinjectModuleTests : IDisposable {
        private static string PluginDirectory => "test";

        private readonly StandardKernel _kernel;

        public OrionNinjectModuleTests() {
            Directory.CreateDirectory(PluginDirectory);
            _kernel = new StandardKernel(new OrionNinjectModule(PluginDirectory));
        }

        public void Dispose() {
            _kernel.Dispose();
        }

        [Fact]
        public void ServicesAreSingleton() {
            var service1 = _kernel.Get<IMockService>();
            var service2 = _kernel.Get<IMockService>();
            
            service1.Should().BeOfType<MockService>();
            service1.Should().BeSameAs(service2);
        }

        [Fact]
        public void InstancedServicesAreInstanced() {
            var service1 = _kernel.Get<IMockInstanceService>();
            var service2 = _kernel.Get<IMockInstanceService>();
            
            service1.Should().BeOfType<MockInstanceService>();
            service1.Should().NotBeSameAs(service2);
        }

        [Fact]
        public void OverrideServicesDoOverride() {
            var services = _kernel.GetAll<IMockOverrideService>().ToList();

            services.Should().ContainSingle();
            services[0].Should().NotBeOfType<MockOverrideService>();
            services[0].Should().BeOfType<MockOverrideServiceV2>();
        }


        public interface IMockService : IService {
        }

        public interface IMockInstanceService : IService {
        }

        public interface IMockOverrideService : IService {
        }

        public class MockService : OrionService, IMockService {
            public override string Author => "TestAuthor";
            public override string Name => "TestName";
        }

        [InstancedService]
        public class MockInstanceService : OrionService, IMockInstanceService {
            public override string Author => "TestAuthor";
            public override string Name => "TestName";
        }

        public class MockOverrideService : OrionService, IMockOverrideService {
            public override string Author => "TestAuthor";
            public override string Name => "TestName";
        }

        [OverrideService]
        public class MockOverrideServiceV2 : OrionService, IMockOverrideService {
            public override string Author => "TestAuthor";
            public override string Name => "TestName";
        }
    }
}
