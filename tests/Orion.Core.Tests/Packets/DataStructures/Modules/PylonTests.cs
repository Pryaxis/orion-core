using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.DataStructures.Modules
{
    public sealed class PylonTests
    {
        private readonly byte[] _bytes = { 8, 0, 2, 3, 0, 4, 0, 5 };

        [Theory]
        [InlineData(PylonAction.AddPylon)]
        [InlineData(PylonAction.RemovePylon)]
        [InlineData(PylonAction.Teleport)]
        public void Action_Set_Get(PylonAction action)
        {
            var module = new Pylon();

            module.Action = action;

            Assert.Equal(action, module.Action);
        }

        [Fact]
        public void X_Set_Get()
        {
            var module = new Pylon();

            module.X = 1024;

            Assert.Equal(1024, module.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var module = new Pylon();

            module.Y = 2048;

            Assert.Equal(2048, module.Y);
        }

        [Theory]
        [InlineData(PylonType.Beach)]
        [InlineData(PylonType.Desert)]
        [InlineData(PylonType.GlowingMushroom)]
        public void Type_Set_Get(PylonType type)
        {
            var module = new Pylon();

            module.Type = type;

            Assert.Equal(type, module.Type);
        }

        [Fact]
        public void Read()
        {
            var module = TestUtils.ReadModule<Pylon>(_bytes, PacketContext.Server);

            Assert.Equal(PylonAction.Teleport, module.Action);
            Assert.Equal(3, module.X);
            Assert.Equal(4, module.Y);
            Assert.Equal(PylonType.Desert, module.Type);
        }
    }
}
