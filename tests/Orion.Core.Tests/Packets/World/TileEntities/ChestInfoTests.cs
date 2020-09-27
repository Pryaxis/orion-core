using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World.TileEntities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class ChestInfoTests
    {
        private readonly byte[] _bytes = { 20, 0, 33, 5, 0, 150, 0, 200, 0, 10, 84, 101, 115, 116, 32, 67, 104, 101, 115, 116 };

        [Fact]
        public void ChestIndex_Set_Get()
        {
            var packet = new ChestInfo();

            packet.ChestIndex = 10;

            Assert.Equal(10, packet.ChestIndex);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new ChestInfo();

            packet.X = 25;

            Assert.Equal(25, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new ChestInfo();

            packet.Y = 25;

            Assert.Equal(25, packet.Y);
        }

        [Fact]
        public void Name_GetNullValue()
        {
            var packet = new ChestInfo();

            Assert.Equal(string.Empty, packet.Name);
        }

        [Fact]
        public void Name_SetNullValue_ThrowsArgumentNullException()
        {
            var packet = new ChestInfo();

            Assert.Throws<ArgumentNullException>(() => packet.Name = null!);
        }

        [Fact]
        public void Name_Set_Get()
        {
            var packet = new ChestInfo();

            packet.Name = "Chest";

            Assert.Equal("Chest", packet.Name);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ChestInfo>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.ChestIndex);
            Assert.Equal(150, packet.X);
            Assert.Equal(200, packet.Y);
            Assert.Equal("Test Chest", packet.Name);
        }
    }
}
