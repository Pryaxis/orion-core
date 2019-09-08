using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to perform a teleportation potion.
    /// </summary>
    public sealed class PerformTeleportationPotionPacket : Packet {
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) { }
        private protected override void WriteToWriter(BinaryWriter writer) { }
    }
}
