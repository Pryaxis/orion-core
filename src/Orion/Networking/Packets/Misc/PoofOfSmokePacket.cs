using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent from the server to the client to show a poof of smoke.
    /// </summary>
    public sealed class PoofOfSmokePacket : Packet {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public HalfVector2 SmokePosition { get; set; }

        private protected override PacketType Type => PacketType.PoofOfSmoke;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[({SmokePosition})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            SmokePosition = new HalfVector2 {
                PackedValue = reader.ReadUInt32(),
            };
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(SmokePosition.PackedValue);
        }
    }
}
