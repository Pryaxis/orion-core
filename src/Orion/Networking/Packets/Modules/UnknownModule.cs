using System;
using System.IO;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Used as a fail-safe for any module that failed to be read.
    /// </summary>
    public sealed class UnknownModule : Module {
        private byte[] _payload = new byte[0];

        /// <inheritdoc />
        public override ModuleType ModuleType { get; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public byte[] Payload {
            get => _payload;
            set => _payload = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownModule"/> class with the given module type.
        /// </summary>
        /// <param name="moduleType">The module type.</param>
        public UnknownModule(ModuleType moduleType) {
            ModuleType = moduleType;
        }

        private protected override void ReadFromReader(BinaryReader reader, ushort moduleLength) =>
            _payload = reader.ReadBytes(moduleLength - HeaderLength);

        private protected override void WriteToWriter(BinaryWriter writer) => writer.Write(Payload);
    }
}
