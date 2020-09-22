using System;
using Microsoft.Xna.Framework;
using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.World
{
    public sealed class PoofOfSmokeTests
    {
        private readonly byte[] _bytes = { 7, 0, 106, 0, 64, 0, 64 };

        [Fact]
        public void Position_Set_Get()
        {
            var packet = new PoofOfSmoke();

            packet.Position = new PackedVector2f(1.23F, 4.56F);

            var tolerance = 0.01;
            var (x, y) = new Vector2f(1.23F, 4.56F) - packet.Position.Vector2;
            Assert.True(Math.Abs(x) < tolerance);
            Assert.True(Math.Abs(y) < tolerance);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PoofOfSmoke>(_bytes, PacketContext.Server);

            Assert.Equal(BitConverter.ToUInt32(_bytes[3..]), packet.Position.PackedValue);
            Assert.Equal(new Vector2f(2, 2), packet.Position.Vector2);
        }
    }
}
