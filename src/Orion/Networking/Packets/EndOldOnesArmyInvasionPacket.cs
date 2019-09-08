using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to end the Old One's Army event.
    /// </summary>
    public sealed class EndOldOnesArmyInvasionPacket : Packet {
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) { }
        private protected override void WriteToWriter(BinaryWriter writer) { }
    }
}
