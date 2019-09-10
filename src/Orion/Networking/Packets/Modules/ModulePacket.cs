using System;
using System.IO;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Packet sent in the form of a module.
    /// </summary>
    public sealed class ModulePacket : Packet {
        private Module _module;

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public Module Module {
            get => _module;
            set => _module = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.Module;

        /// <inheritdoc />
        public override string ToString() => $"{nameof(PacketType.Module)}[{Module}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            Module = Module.ReadFromStream(reader.BaseStream, context);
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            Module.WriteToStream(writer.BaseStream, context);
        }
    }
}
