using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Represents a Terraria module.
    /// </summary>
    public abstract class Module {
        private static readonly IDictionary<ModuleType, Func<Module>> ModuleConstructors =
            new Dictionary<ModuleType, Func<Module>> {
            };
        
        [ExcludeFromCodeCoverage]
        internal static int HeaderLength => sizeof(ModuleType);

        /// <summary>
        /// Gets the module type.
        /// </summary>
        public abstract ModuleType ModuleType { get; }

        /// <summary>
        /// Reads a module from the given stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="moduleLength">The module length.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <returns>The module.</returns>
        public static Module ReadFromStream(Stream stream, ushort moduleLength) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var reader = new BinaryReader(stream, Encoding.UTF8, true)) {
                var position = stream.Position;
                var moduleType = (ModuleType)reader.ReadUInt16();
                var moduleCtor =
                    ModuleConstructors.TryGetValue(moduleType, out var f) ? f : () => new UnknownModule(moduleType);
                var module = moduleCtor();
                module.ReadFromReader(reader, moduleLength);
                
                Debug.Assert(stream.Position - position == moduleLength, "Module should be fully consumed.");

                return module;
            }
        }

        /// <summary>
        /// Writes the module to the given stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public void WriteToStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true)) {
                writer.Write((ushort)ModuleType);
                WriteToWriter(writer);
            }
        }

        private protected abstract void ReadFromReader(BinaryReader reader, ushort moduleLength);
        private protected abstract void WriteToWriter(BinaryWriter writer);
    }
}
