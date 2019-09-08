using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a player's item animation.
    /// </summary>
    public sealed class UpdatePlayerItemAnimationPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's item rotation.
        /// </summary>
        public float PlayerItemRotation { get; set; }

        /// <summary>
        /// Gets or sets the player's item animation.
        /// </summary>
        public short PlayerItemAnimation { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerItemRotation = reader.ReadSingle();
            PlayerItemAnimation = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerItemRotation);
            writer.Write(PlayerItemAnimation);
        }
    }
}
