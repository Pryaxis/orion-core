using System;
using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to provide a world section.
    /// </summary>
    public sealed class UpdateWorldSectionPacket : TerrariaPacket {
        private byte[] _worldSection = new byte[0];

        /// <inheritdoc />
        public override bool IsSentToClient => true;
        
        /// <inheritdoc />
        public override bool IsSentToServer => false;
        
        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.UpdateWorldSection;

        /// <summary>
        /// Gets or sets the world section.
        /// </summary>
        public byte[] WorldSection {
            get => _worldSection;
            set => _worldSection = value ?? throw new ArgumentNullException(nameof(value));
        }

        // TODO: provide more descriptive information about the world section.
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            _worldSection = reader.ReadBytes(packetLength - HeaderLength);
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(WorldSection);
        }
    }
}
