using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to toggle a birthday party.
    /// </summary>
    public sealed class ToggleBirthdayPartyPacket : Packet {
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) { }
        private protected override void WriteToWriter(BinaryWriter writer) { }
    }
}
