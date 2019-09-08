using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to toggle the state of a door.
    /// </summary>
    public sealed class ToggleDoorPacket : Packet {
        /// <summary>
        /// Gets or sets the toggle type.
        /// </summary>
        public Type ToggleType { get; set; }

        /// <summary>
        /// Gets or sets the door's X coordinate.
        /// </summary>
        public short X { get; set; }

        /// <summary>
        /// Gets or sets the door's Y coordinate.
        /// </summary>
        public short Y { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the direction of the toggle.
        /// </summary>
        public bool Direction { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ToggleType = (Type)reader.ReadByte();
            X = reader.ReadInt16();
            Y = reader.ReadInt16();
            Direction = reader.ReadByte() == 1;
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write((byte)ToggleType);
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Direction);
        }

        /// <summary>
        /// Specifies the toggle type.
        /// </summary>
        public enum Type : byte {
#pragma warning disable 1591
            OpenDoor = 0,
            CloseDoor,
            OpenTrapdoor,
            CloseTrapdoor,
            OpenTallGate,
            CloseTallGate,
#pragma warning restore 1591
        }
    }
}
