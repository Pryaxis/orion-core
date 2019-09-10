using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Represents a module. This is sent in a <see cref="ModulePacket"/>.
    /// </summary>
    public abstract class Module {
        private static readonly IDictionary<ModuleType, Func<Module>> ModuleConstructors =
            new Dictionary<ModuleType, Func<Module>> {
                [ModuleType.LiquidChanges] = () => new LiquidChangesModule(),
                [ModuleType.Chat] = () => new ChatModule(),
            };

        private protected abstract ModuleType Type { get; }

        /// <summary>
        /// Reads a module from the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to read the module from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <returns>The module.</returns>
        public static Module ReadFromStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using (var reader = new BinaryReader(stream, Encoding.UTF8, true)) {
                var moduleType = (ModuleType)reader.ReadUInt16();
                var module = ModuleConstructors[moduleType]();
                module.ReadFromReader(reader, context);
                return module;
            }
        }

        /// <summary>
        /// Writes the module to the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to write the packet to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public void WriteToStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true)) {
                writer.Write((ushort)Type);
                WriteToWriter(writer, context);
            }
        }

        private protected abstract void ReadFromReader(BinaryReader reader, PacketContext context);
        private protected abstract void WriteToWriter(BinaryWriter writer, PacketContext context);
    }
}
