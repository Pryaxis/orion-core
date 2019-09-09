using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the client to the server to toggle the birthday party status.
    /// </summary>
    public sealed class ToggleBirthdayPartyPacket : Packet {
        private protected override PacketType Type => PacketType.ToggleBirthdayParty;
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) { }
        private protected override void WriteToWriter(BinaryWriter writer) { }
    }
}
