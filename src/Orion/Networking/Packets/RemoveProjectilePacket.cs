using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to remove a projectile.
    /// </summary>
    public sealed class RemoveProjectilePacket : Packet {
        /// <summary>
        /// Gets or sets the projectile identity.
        /// </summary>
        public short ProjectileIdentity { get; set; }

        /// <summary>
        /// Gets or sets the owner player index.
        /// </summary>
        public byte OwnerPlayerIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ProjectileIdentity = reader.ReadInt16();
            OwnerPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ProjectileIdentity);
            writer.Write(OwnerPlayerIndex);
        }
    }
}
