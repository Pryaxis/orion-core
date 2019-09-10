using System.Diagnostics.CodeAnalysis;
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
        /// Gets or sets the player spawn's X coordinate. A negative value results in the world spawn.
        /// </summary>
        public short PlayerSpawnX { get; set; }

        /// <summary>
        /// Gets or sets the player spawn's Y coordinate. A negative value results in the world spawn.
        /// </summary>
        public short PlayerSpawnY { get; set; }

        private protected override PacketType Type => PacketType.SpawnPlayer;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} @ ({PlayerSpawnX}, {PlayerSpawnY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerSpawnX = reader.ReadInt16();
            PlayerSpawnY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerSpawnX);
            writer.Write(PlayerSpawnY);
        }
    }
}
