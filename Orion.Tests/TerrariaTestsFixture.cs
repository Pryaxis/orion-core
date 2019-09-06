using System;
using Xunit;

namespace Orion.Tests {
    public class TerrariaTestsFixture : IDisposable {
        private readonly Terraria.Main _main;

        public TerrariaTestsFixture() {
            _main = new Terraria.Main();

            _main.Initialize();
        }

        public void Dispose() {
            _main.Dispose();
        }
    }

    [CollectionDefinition("TerrariaTestsCollection")]
    public class WorldTestsCollection : ICollectionFixture<TerrariaTestsFixture> { }
}
