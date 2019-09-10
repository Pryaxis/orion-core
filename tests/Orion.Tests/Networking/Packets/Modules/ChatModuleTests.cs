using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Modules;
using Xunit;

namespace Orion.Tests.Networking.Packets.Modules {
    public class ChatModuleTests {
        public static readonly byte[] ChatBytes = {
            23, 0, 82, 1, 0, 3, 83, 97, 121, 13, 47, 99, 111, 109, 109, 97, 110, 100, 32, 116, 101, 115, 116,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ChatBytes)) {
                var packet = (ModulePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.Module.Should().BeOfType<ChatModule>();
                packet.Module.As<ChatModule>().ClientChatCommand.Should().Be("Say");
                packet.Module.As<ChatModule>().ClientChatText.Should().Be("/command test");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ChatBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Client);

                stream2.ToArray().Should().BeEquivalentTo(ChatBytes);
            }
        }
    }
}
