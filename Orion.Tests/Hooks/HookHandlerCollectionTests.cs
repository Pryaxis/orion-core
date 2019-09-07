using System;
using FluentAssertions;
using Orion.Hooks;
using Xunit;

namespace Orion.Tests.Hooks {
    public class HookHandlerCollectionTests {
        [Fact]
        public void Plus_IsCorrect() {
            HookHandlerCollection<TestEventArgs> collection = null;
            collection += TestHandler;
            var args = new TestEventArgs();

            collection.Invoke(this, args);

            args.Value.Should().Be(100);
        }

        [Fact]
        public void Plus_Priority_IsCorrect() {
            HookHandlerCollection<TestEventArgs> collection = null;
            collection += TestHandler2;
            collection += TestHandler;
            var args = new TestEventArgs();

            collection.Invoke(this, args);

            args.Value.Should().Be(200);
        }

        [Fact]
        public void Plus_NullHandler_ThrowsArgumentNullException() {
            HookHandlerCollection<TestEventArgs> collection = null;
            Func<HookHandlerCollection<TestEventArgs>> func = () => collection += null;

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Minus_IsCorrect() {
            HookHandlerCollection<TestEventArgs> collection = null;
            collection += TestHandler2;
            collection += TestHandler;
            collection -= TestHandler2;
            var args = new TestEventArgs();

            collection.Invoke(this, args);

            args.Value.Should().Be(100);
        }

        [Fact]
        public void Minus_NullHandler_ThrowsArgumentNullException() {
            HookHandlerCollection<TestEventArgs> collection = null;
            Func<HookHandlerCollection<TestEventArgs>> func = () => collection -= null;

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Minus_InvalidHandler_ThrowsArgumentException() {
            HookHandlerCollection<TestEventArgs> collection = null;
            Func<HookHandlerCollection<TestEventArgs>> func = () => collection -= TestHandler;

            func.Should().Throw<ArgumentException>();
        }

        [HookHandler(HookPriority.Lowest)]
        private static void TestHandler(object sender, TestEventArgs args) {
            args.Value = 100;
        }

        [HookHandler(HookPriority.Highest)]
        private static void TestHandler2(object sender, TestEventArgs args) {
            args.Value = 200;
        }

        private class TestEventArgs : EventArgs {
            public int Value { get; set; }
        }
    }
}
