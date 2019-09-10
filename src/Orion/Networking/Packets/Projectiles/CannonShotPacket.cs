using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Projectiles {
    /// <summary>
    /// Packet sent from the server to the client to shoot from a cannon.
    /// </summary>
    public sealed class CannonShotPacket : Packet {
        /// <summary>
        /// Gets or sets the shot's damage.
        /// </summary>
        public short ShotDamage { get; set; }

        /// <summary>
        /// Gets or sets the shot's knockback.
        /// </summary>
        public float ShotKnockback { get; set; }

        /// <summary>
        /// Gets or sets the cannon tile's X coordinate.
        /// </summary>
        public short CannonTileX { get; set; }

        /// <summary>
        /// Gets or sets the cannon tile's Y coordinate.
        /// </summary>
        public short CannonTileY { get; set; }

        /// <summary>
        /// Gets or sets the shot's angle.
        /// </summary>
        public short ShotAngle { get; set; }

        /// <summary>
        /// Gets or sets the shot's ammo type.
        /// </summary>
        // TODO: implement an enum for this.
        public short ShotAmmoType { get; set; }

        /// <summary>
        /// Gets or sets the shooter's player index.
        /// </summary>
        public byte ShooterPlayerIndex { get; set; }

        private protected override PacketType Type => PacketType.CannonShot;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={ShooterPlayerIndex}, {ShotAmmoType} @ ({CannonTileX}, {CannonTileY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ShotDamage = reader.ReadInt16();
            ShotKnockback = reader.ReadSingle();
            CannonTileX = reader.ReadInt16();
            CannonTileY = reader.ReadInt16();
            ShotAngle = reader.ReadInt16();
            ShotAmmoType = reader.ReadInt16();
            ShooterPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ShotDamage);
            writer.Write(ShotKnockback);
            writer.Write(CannonTileX);
            writer.Write(CannonTileY);
            writer.Write(ShotAngle);
            writer.Write(ShotAmmoType);
            writer.Write(ShooterPlayerIndex);
        }
    }
}
