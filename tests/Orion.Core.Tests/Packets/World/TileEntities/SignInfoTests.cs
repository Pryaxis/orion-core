using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World.TileEntities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class SignInfoTests
    {
        private readonly byte[] _bytes = { 21, 0, 47, 5, 100, 0, 100, 0, 11, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 250, 0 };

        [Fact]
        public void SignIndex_Set_Get()
        {
            var packet = new SignInfo();

            packet.SignIndex = 1;

            Assert.Equal(1, packet.SignIndex);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new SignInfo();

            packet.X = 2;

            Assert.Equal(2, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new SignInfo();

            packet.Y = 3;

            Assert.Equal(3, packet.Y);
        }

        [Fact]
        public void Text_GetNullValue()
        {
            var packet = new SignInfo();

            Assert.Equal(string.Empty, packet.Text);
        }

        [Fact]
        public void Text_NullValue_ThrowsArgumentNullException()
        {
            var packet = new SignInfo();

            Assert.Throws<ArgumentNullException>(() => packet.Text = null!);
        }

        [Fact]
        public void Text_Set_Get()
        {
            var packet = new SignInfo();

            packet.Text = "test";

            Assert.Equal("test", packet.Text);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsTombstone_Set_Get(bool value)
        {
            var packet = new SignInfo();

            packet.IsTombstone = value;

            Assert.Equal(value, packet.IsTombstone);
        }

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new SignInfo();

            packet.PlayerIndex = 4;

            Assert.Equal(4, packet.PlayerIndex);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<SignInfo>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.SignIndex);
            Assert.Equal(100, packet.X);
            Assert.Equal(100, packet.Y);
            Assert.Equal("ABCDEFGHIJK", packet.Text);
            Assert.Equal(250, packet.PlayerIndex);
        }
    }
}
