using System;
using FluentAssertions;
using Xunit;

namespace Orion.Tests {
    public class OrionKernelTests {
        /*
         * It is difficult to test the plugin loading mechanism programmatically...
         */

        [Fact]
        public void QueuePluginsFromPath_NullAssemblyPath_ThrowsArgumentNullException() {
            using (var kernel = new OrionKernel()) {
                // ReSharper disable once AccessToDisposedClosure
                Action action = () => kernel.QueuePluginsFromPath(null);

                action.Should().Throw<ArgumentNullException>();
            }
        }

        [Fact]
        public void UnloadPlugin_NullPlugin_ThrowsArgumentNullException() {
            using (var kernel = new OrionKernel()) {
                // ReSharper disable once AccessToDisposedClosure
                Action action = () => kernel.UnloadPlugin(null);

                action.Should().Throw<ArgumentNullException>();
            }
        }
    }
}
