using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Orion.Projectiles;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a projectile.
    /// </summary>
    public sealed class UpdateProjectilePacket : Packet {
        /// <summary>
        /// Gets or sets the projectile identity.
        /// </summary>
        public short ProjectileIdentity { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        public float Knockback { get; set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the owner's player index.
        /// </summary>
        public byte OwnerPlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the projectile type.
        /// </summary>
        public ProjectileType ProjectileType { get; set; }

        /// <summary>
        /// Gets the projectile's AI values.
        /// </summary>
        public float[] AiValues { get; } = new float[2];

        /// <summary>
        /// Gets or sets the projectile's UUID.
        /// </summary>
        public short ProjectileUuid { get; set; } = -1;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ProjectileIdentity = reader.ReadInt16();
            Position = reader.ReadVector2();
            Velocity = reader.ReadVector2();
            Knockback = reader.ReadSingle();
            Damage = reader.ReadInt16();
            OwnerPlayerIndex = reader.ReadByte();
            ProjectileType = (ProjectileType)reader.ReadInt16();

            Terraria.BitsByte header = reader.ReadByte();
            if (header[0]) AiValues[0] = reader.ReadSingle();
            if (header[1]) AiValues[1] = reader.ReadSingle();
            if (header[2]) ProjectileUuid = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ProjectileIdentity);
            writer.Write(Position);
            writer.Write(Velocity);
            writer.Write(Knockback);
            writer.Write(Damage);
            writer.Write(OwnerPlayerIndex);
            writer.Write((short)ProjectileType);

            Terraria.BitsByte header = 0;
            header[0] = AiValues[0] != 0;
            header[1] = AiValues[1] != 0;
            header[2] = ProjectileUuid >= 0;

            writer.Write(header);
            if (header[0]) writer.Write(AiValues[0]);
            if (header[1]) writer.Write(AiValues[1]);
            if (header[2]) writer.Write(ProjectileUuid);
        }
    }
}
