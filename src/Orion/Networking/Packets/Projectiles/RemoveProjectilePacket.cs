using System.IO;

namespace Orion.Networking.Packets.Projectiles {
    /// <summary>
    /// Packet sent to remove a projectile.
    /// </summary>
    public sealed class RemoveProjectilePacket : Packet {
        /// <summary>
        /// Gets or sets the projectile identity.
        /// </summary>
        public short ProjectileIdentity { get; set; }

        /// <summary>
        /// Gets or sets the projectile owner's player index.
        /// </summary>
        public byte ProjectileOwnerPlayerIndex { get; set; }

        private protected override PacketType Type => PacketType.RemoveProjectile;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ProjectileIdentity = reader.ReadInt16();
            ProjectileOwnerPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ProjectileIdentity);
            writer.Write(ProjectileOwnerPlayerIndex);
        }
    }
}
