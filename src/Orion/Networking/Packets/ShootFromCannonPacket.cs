using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to shoot from a cannon.
    /// </summary>
    public sealed class ShootFromCannonPacket : Packet {
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
        /// Gets or sets the ammo.
        /// </summary>
        public short Ammo { get; set; }

        /// <summary>
        /// Gets or sets the shooter's player index.
        /// </summary>
        public byte ShooterPlayerIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            Damage = reader.ReadInt16();
            Knockback = reader.ReadSingle();
            CannonTileX = reader.ReadInt16();
            CannonTileY = reader.ReadInt16();
            Angle = reader.ReadInt16();
            Ammo = reader.ReadInt16();
            ShooterPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Damage);
            writer.Write(Knockback);
            writer.Write(CannonTileX);
            writer.Write(CannonTileY);
            writer.Write(Angle);
            writer.Write(Ammo);
            writer.Write(ShooterPlayerIndex);
        }
    }
}
