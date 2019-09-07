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

    /*
     * These tests cannot run in parallel, since they interact heavily with Terraria's static state. So we have to run
     * these in series. However, all of the other tests can be run simultaneously, at least.
     */
    [CollectionDefinition("TerrariaTestsCollection", DisableParallelization = true)]
    public class TerrariaTestsCollection : ICollectionFixture<TerrariaTestsFixture> { }
}
