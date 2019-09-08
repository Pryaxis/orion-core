using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to perform an action.
    /// </summary>
    public sealed class PerformActionPacket : Packet {
        /// <summary>
        /// Gets or sets the player or NPC index.
        /// </summary>
        public byte PlayerOrNpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the action type.
        /// </summary>
        public Type ActionType { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerOrNpcIndex = reader.ReadByte();
            ActionType = (Type)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerOrNpcIndex);
            writer.Write((byte)ActionType);
        }

        /// <summary>
        /// Specifies the type of action.
        /// </summary>
        public enum Type : byte {
            #pragma warning disable 1591
            None = 0,
            SpawnSkeletron = 1,
            PlaySound = 2,
            UseSundial = 3,
            CreateBigMimicSmoke = 4,
            #pragma warning restore 1591
        }
    }
}
