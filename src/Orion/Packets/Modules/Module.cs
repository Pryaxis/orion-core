// Copyright (c) 2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Orion.Utils;

namespace Orion.Packets.Modules {
    /// <summary>
    /// Represents a module. This is sent in a <see cref="ModulePacket"/>.
    /// </summary>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Consumers are C#")]
    public abstract class Module : IDirtiable {
        private static readonly Func<Module>?[] _constructors = {
            /* 000 */ () => new LiquidsModule(),
            /* 001 */ () => new ChatModule()
        };

        private protected bool _isDirty;

        /// <inheritdoc/>
        public virtual bool IsDirty => _isDirty;

        /// <summary>
        /// Gets the module's type.
        /// </summary>
        public abstract ModuleType Type { get; }

        /// <summary>
        /// Reads and returns a module from the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to read the module from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/>.</exception>
        /// <returns>The resulting <see cref="Module"/> instance.</returns>
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope",
            Justification = "BinaryReader does not need to be disposed")]
        public static Module ReadFromStream(Stream stream, PacketContext context) {
            if (stream is null) {
                throw new ArgumentNullException(nameof(stream));
            }

            static Func<Module>? GetModuleConstructor(ushort moduleTypeId) =>
                moduleTypeId < _constructors.Length ? _constructors[moduleTypeId] : null;

            var reader = new BinaryReader(stream, Encoding.UTF8, true);
            var moduleConstructor =
                GetModuleConstructor(reader.ReadUInt16()) ?? throw new PacketException("Module type is invalid.");
            var module = moduleConstructor();
            module.ReadFromReader(reader, context);
            return module;
        }

        /// <inheritdoc/>
        public virtual void Clean() => _isDirty = false;

        /// <summary>
        /// Writes the module to the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to write the packet to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/>.</exception>
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope",
            Justification = "BinaryWriter does not need to be disposed")]
        public void WriteToStream(Stream stream, PacketContext context) {
            if (stream is null) {
                throw new ArgumentNullException(nameof(stream));
            }

            var writer = new BinaryWriter(stream, Encoding.UTF8, true);
            writer.Write((ushort)Type);
            WriteToWriter(writer, context);
        }

        private protected abstract void ReadFromReader(BinaryReader reader, PacketContext context);
        private protected abstract void WriteToWriter(BinaryWriter writer, PacketContext context);
    }
}
