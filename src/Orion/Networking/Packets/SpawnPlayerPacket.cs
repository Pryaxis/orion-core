using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to spawn a player.
    /// </summary>
    public sealed class SpawnPlayerPacket : TerrariaPacket {
        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.SpawnPlayer;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate of the spawn position.
        /// </summary>
        public short SpawnX { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the spawn position.
        /// </summary>
        public short SpawnY { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            SpawnX = reader.ReadInt16();
            SpawnY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(SpawnX);
            writer.Write(SpawnY);
        }
    }
}
