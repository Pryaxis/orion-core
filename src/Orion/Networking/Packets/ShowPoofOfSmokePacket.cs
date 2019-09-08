using System.IO;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to show a poof of smoke.
    /// </summary>
    public sealed class ShowPoofOfSmokePacket : Packet {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public HalfVector2 Position { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            Position = new HalfVector2 {
                PackedValue = reader.ReadUInt32(),
            };
        }

        private protected override void WriteToWriter(BinaryWriter writer) => writer.Write(Position.PackedValue);
    }
}
