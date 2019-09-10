using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Modules;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Tests.Networking.Packets.Modules {
    public class LiquidChangesModuleTests {
        public static readonly byte[] LiquidChangesBytes = {13, 0, 82, 0, 0, 1, 0, 100, 0, 0, 1, 255, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(LiquidChangesBytes)) {
                var packet = (ModulePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.Module.Should().BeOfType<LiquidChangesModule>();
                packet.Module.As<LiquidChangesModule>().LiquidChanges.Should().BeEquivalentTo(
                    new LiquidChange(256, 100, 255, LiquidType.Water));
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(LiquidChangesBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Client);

                stream2.ToArray().Should().BeEquivalentTo(LiquidChangesBytes);
            }
        }
    }
}
