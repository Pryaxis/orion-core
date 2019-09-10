using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to end the Old One's Army event.
    /// </summary>
    public sealed class EndOldOnesArmyPacket : Packet {
        private protected override PacketType Type => PacketType.EndOldOnesArmy;
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) { }
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) { }
    }
}
