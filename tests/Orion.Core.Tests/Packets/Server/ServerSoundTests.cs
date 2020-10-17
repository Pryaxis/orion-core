using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.Server
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public sealed class ServerSoundTests
    {
        private readonly byte[] _bytes = { 26, 0, 132, 164, 112, 157, 63, 133, 235, 145, 64, 2, 0, 7, 3, 0, 0, 0, 0, 0, 128, 64, 0, 0, 160, 64 };

        [Fact]
        public void Position_Set_Get()
        {
            var packet = new ServerSound();

            packet.Position = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Position);
        }

        [Fact]
        public void SoundIndex_Set_Get()
        {
            var packet = new ServerSound();

            packet.SoundIndex = 1;

            Assert.Equal(1, packet.SoundIndex);
        }

        [Fact]
        public void Style_Set_Get()
        {
            var packet = new ServerSound();

            packet.Style = 1;

            Assert.Equal(1, packet.Style);
        }

        [Fact]
        public void Volume_Set_Get()
        {
            var packet = new ServerSound();

            packet.Volume = 1F;

            Assert.Equal(1F, packet.Volume);
        }

        [Fact]
        public void PitchOffset_Set_Get()
        {
            var packet = new ServerSound();

            packet.PitchOffset = 1F;

            Assert.Equal(1F, packet.PitchOffset);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ServerSound>(_bytes, PacketContext.Server);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Position);
            Assert.Equal(2, packet.SoundIndex);
            Assert.Equal(3, packet.Style!.Value);
            Assert.Equal(4, packet.Volume!.Value);
            Assert.Equal(5, packet.PitchOffset!.Value);
        }
    }
}
