using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update the time.
    /// </summary>
    public sealed class UpdateTimePacket : Packet {
        /// <summary>
        /// Gets or sets a value indicating whether it is daytime.
        /// </summary>
        public bool IsDaytime { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets the sun's Y position.
        /// </summary>
        public short SunY { get; set; }

        /// <summary>
        /// Gets or sets the moon's Y position.
        /// </summary>
        public short MoonY { get; set; }

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            IsDaytime = reader.ReadByte() == 1;
            Time = reader.ReadInt32();
            SunY = reader.ReadInt16();
            MoonY = reader.ReadInt16();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(IsDaytime);
            writer.Write(Time);
            writer.Write(SunY);
            writer.Write(MoonY);
        }
    }
}
