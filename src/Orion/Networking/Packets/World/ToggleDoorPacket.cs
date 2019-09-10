using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to toggle the state of a door.
    /// </summary>
    public sealed class ToggleDoorPacket : Packet {
        /// <summary>
        /// Gets or sets the door action.
        /// </summary>
        public ToggleDoorAction DoorAction { get; set; }

        /// <summary>
        /// Gets or sets the door's X coordinate.
        /// </summary>
        public short DoorX { get; set; }

        /// <summary>
        /// Gets or sets the door's Y coordinate.
        /// </summary>
        public short DoorY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the direction of the toggle.
        /// </summary>
        public bool ToggleDirection { get; set; }

        private protected override PacketType Type => PacketType.ToggleDoor;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{DoorAction} @ ({DoorX}, {DoorY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            DoorAction = (ToggleDoorAction)reader.ReadByte();
            DoorX = reader.ReadInt16();
            DoorY = reader.ReadInt16();
            ToggleDirection = reader.ReadByte() == 1;
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)DoorAction);
            writer.Write(DoorX);
            writer.Write(DoorY);
            writer.Write(ToggleDirection);
        }
    }
}
