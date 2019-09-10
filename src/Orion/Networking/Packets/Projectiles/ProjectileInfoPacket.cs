using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Orion.Projectiles;

namespace Orion.Networking.Packets.Projectiles {
    /// <summary>
    /// Packet sent to set a projectile's information.
    /// </summary>
    public sealed class ProjectileInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the projectile identity.
        /// </summary>
        public short ProjectileIdentity { get; set; }

        /// <summary>
        /// Gets or sets the projectile's position.
        /// </summary>
        public Vector2 ProjectilePosition { get; set; }

        /// <summary>
        /// Gets or sets the projectile's velocity.
        /// </summary>
        public Vector2 ProjectileVelocity { get; set; }

        /// <summary>
        /// Gets or sets the projectile's knockback.
        /// </summary>
        public float ProjectileKnockback { get; set; }

        /// <summary>
        /// Gets or sets the projectile's damage.
        /// </summary>
        public short ProjectileDamage { get; set; }

        /// <summary>
        /// Gets or sets the projectile owner's player index.
        /// </summary>
        public byte OwnerPlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the projectile type.
        /// </summary>
        public ProjectileType ProjectileType { get; set; }

        /// <summary>
        /// Gets the projectile's AI values.
        /// </summary>
        public float[] ProjectileAiValues { get; } = new float[2];

        /// <summary>
        /// Gets or sets the projectile's UUID.
        /// </summary>
        public short ProjectileUuid { get; set; } = -1;

        private protected override PacketType Type => PacketType.ProjectileInfo;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={ProjectileIdentity}, {ProjectileType} @ ({ProjectilePosition}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ProjectileIdentity = reader.ReadInt16();
            ProjectilePosition = reader.ReadVector2();
            ProjectileVelocity = reader.ReadVector2();
            ProjectileKnockback = reader.ReadSingle();
            ProjectileDamage = reader.ReadInt16();
            OwnerPlayerIndex = reader.ReadByte();
            ProjectileType = (ProjectileType)reader.ReadInt16();

            Terraria.BitsByte header = reader.ReadByte();
            if (header[0]) ProjectileAiValues[0] = reader.ReadSingle();
            if (header[1]) ProjectileAiValues[1] = reader.ReadSingle();
            if (header[2]) ProjectileUuid = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ProjectileIdentity);
            writer.Write(ProjectilePosition);
            writer.Write(ProjectileVelocity);
            writer.Write(ProjectileKnockback);
            writer.Write(ProjectileDamage);
            writer.Write(OwnerPlayerIndex);
            writer.Write((short)ProjectileType);

            Terraria.BitsByte header = 0;
            header[0] = ProjectileAiValues[0] != 0;
            header[1] = ProjectileAiValues[1] != 0;
            header[2] = ProjectileUuid >= 0;

            writer.Write(header);
            if (header[0]) writer.Write(ProjectileAiValues[0]);
            if (header[1]) writer.Write(ProjectileAiValues[1]);
            if (header[2]) writer.Write(ProjectileUuid);
        }
    }
}
