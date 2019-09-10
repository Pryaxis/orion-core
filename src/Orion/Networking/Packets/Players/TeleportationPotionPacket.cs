using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to use a teleportation potion.
    /// </summary>
    public sealed class TeleportationPotionPacket : Packet {
        private protected override PacketType Type => PacketType.TeleportationPotion;
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) { }
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) { }
    }
}
