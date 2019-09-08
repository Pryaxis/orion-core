using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to show a player dodge effect.
    /// </summary>
    public sealed class ShowPlayerDodgePacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's dodge type.
        /// </summary>
        public Type PlayerDodgeType {get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerDodgeType = (Type)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write((byte)PlayerDodgeType);
        }

        /// <summary>
        /// Specifies the dodge type.
        /// </summary>
        public enum Type {
            #pragma warning disable 1591
            None = 0,
            NinjaDodge = 1,
            ShadowDodge = 2,
            #pragma warning restore 1591
        }
    }
}
