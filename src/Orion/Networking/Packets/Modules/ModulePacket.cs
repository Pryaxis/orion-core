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

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) =>
            Module = Module.ReadFromStream(reader.BaseStream, (ushort)(packetLength - HeaderLength));

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) => Module.WriteToStream(writer.BaseStream);
    }
}
