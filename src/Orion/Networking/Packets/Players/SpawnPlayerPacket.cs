using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to spawn a player.
    /// </summary>
    public sealed class SpawnPlayerPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate of the player's spawn position. A negative value results in the world spawn.
        /// </summary>
        public short PlayerSpawnX { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the player's spawn position. A negative value results in the world spawn.
        /// </summary>
        public short PlayerSpawnY { get; set; }

        private protected override PacketType Type => PacketType.SpawnPlayer;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerSpawnX = reader.ReadInt16();
            PlayerSpawnY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerSpawnX);
            writer.Write(PlayerSpawnY);
        }
    }
}
