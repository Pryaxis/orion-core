using Xunit;

namespace Orion.Core.Packets.World.Tiles
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class ObjectPlaceTests
    {
        private readonly byte[] _bytes = { 14, 0, 79, 1, 0, 2, 0, 3, 0, 4, 0, 5, 6, 1 };

        [Fact]
        public void X_Set_Get()
        {
            var packet = new ObjectPlace();

            packet.X = 1;

            Assert.Equal(1, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new ObjectPlace();

            packet.Y = 1;

            Assert.Equal(1, packet.Y);
        }

        [Fact]
        public void TileType_Set_Get()
        {
            var packet = new ObjectPlace();

            packet.TileType = 1;

            Assert.Equal(1, packet.TileType);
        }

        [Fact]
        public void Style_Set_Get()
        {
            var packet = new ObjectPlace();

            packet.Style = 1;

            Assert.Equal(1, packet.Style);
        }

        [Fact]
        public void AlternateStyle_Set_Get()
        {
            var packet = new ObjectPlace();

            packet.AlternateStyle = 1;

            Assert.Equal(1, packet.AlternateStyle);
        }

        [Fact]
        public void RandomStyleModifier_Set_Get()
        {
            var packet = new ObjectPlace();

            packet.RandomStyleModifier = 1;

            Assert.Equal(1, packet.RandomStyleModifier);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsRightDirection_Set_Get(bool value)
        {
            var packet = new ObjectPlace();

            packet.IsRightDirection = value;

            Assert.Equal(value, packet.IsRightDirection);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ObjectPlace>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.X);
            Assert.Equal(2, packet.Y);
            Assert.Equal(3, packet.TileType);
            Assert.Equal(4, packet.Style);
            Assert.Equal(5, packet.AlternateStyle);
            Assert.Equal(6, packet.RandomStyleModifier);
            Assert.True(packet.IsRightDirection);
        }
    }
}
