using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Projectiles {
    /// <summary>
    /// Packet sent to the server to remove a portal.
    /// </summary>
    public sealed class RemovePortalPacket : Packet {
        /// <summary>
        /// Gets or sets the portal's projectile index.
        /// </summary>
        public short PortalProjectileIndex { get; set; }

        private protected override PacketType Type => PacketType.RemovePortal;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PortalProjectileIndex}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PortalProjectileIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PortalProjectileIndex);
        }
    }
}
