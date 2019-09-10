using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Projectiles {
    /// <summary>
    /// Packet sent to the client to shoot from a cannon.
    /// </summary>
    public sealed class CannonShotPacket : Packet {
        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        public float Knockback { get; set; }

        /// <summary>
        /// Gets or sets the cannon tile's X coordinate.
        /// </summary>
        public short CannonTileX { get; set; }

        /// <summary>
        /// Gets or sets the cannon tile's Y coordinate.
        /// </summary>
        public short CannonTileY { get; set; }

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        public short Angle { get; set; }

        /// <summary>
        /// Gets or sets the ammo type.
        /// </summary>
        // TODO: implement an enum for this.
        public short AmmoType { get; set; }

        /// <summary>
        /// Gets or sets the shooter's player index.
        /// </summary>
        public byte ShooterPlayerIndex { get; set; }

        private protected override PacketType Type => PacketType.CannonShot;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={ShooterPlayerIndex}, {AmmoType} @ ({CannonTileX}, {CannonTileY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            Damage = reader.ReadInt16();
            Knockback = reader.ReadSingle();
            CannonTileX = reader.ReadInt16();
            CannonTileY = reader.ReadInt16();
            Angle = reader.ReadInt16();
            AmmoType = reader.ReadInt16();
            ShooterPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(Damage);
            writer.Write(Knockback);
            writer.Write(CannonTileX);
            writer.Write(CannonTileY);
            writer.Write(Angle);
            writer.Write(AmmoType);
            writer.Write(ShooterPlayerIndex);
        }
    }
}
