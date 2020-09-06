using System;
using System.Collections.Generic;
using System.Text;
using ReLogic.Content;
using Xunit;

namespace Orion.Core.Packets.World.TileEntities
{
    public sealed class ChestModifyTests
    {
        private readonly byte[] _bytes = { 12, 0, 34, 1, 0, 4, 0, 8, 3, 0, 50, 0 };

        [Theory]
        [InlineData(ChestModification.PlaceChest)]
        [InlineData(ChestModification.DestroyChest)]
        [InlineData(ChestModification.PlaceContainer)]
        [InlineData(ChestModification.DestroyContainer)]
        public void Modification_Set_Get(ChestModification modification)
        {
            var packet = new ChestModify();

            packet.Modification = modification;

            Assert.Equal(modification, packet.Modification);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new ChestModify();

            packet.X = 50;

            Assert.Equal(50, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new ChestModify();

            packet.Y = 50;

            Assert.Equal(50, packet.Y);
        }

        [Fact]
        public void Style_Set_Get()
        {
            var packet = new ChestModify();

            packet.Style = 1234;

            Assert.Equal(1234, packet.Style);
        }

        [Fact]
        public void ChestIndex_Set_Get()
        {
            var packet = new ChestModify();

            packet.ChestIndex = 10;

            Assert.Equal(10, packet.ChestIndex);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ChestModify>(_bytes, PacketContext.Server);

            Assert.Equal(ChestModification.DestroyChest, packet.Modification);
            Assert.Equal(1024, packet.X);
            Assert.Equal(2048, packet.Y);
            Assert.Equal(3, packet.Style);
            Assert.Equal(50, packet.ChestIndex);
        }
    }
}
