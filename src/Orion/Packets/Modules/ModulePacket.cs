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
using System.Diagnostics.Contracts;
using System.IO;

namespace Orion.Packets.Modules {
    /// <summary>
    /// Packet sent in the form of a module.
    /// </summary>
    // TODO: check nullability possibilities
    public sealed class ModulePacket : Packet {
        private Module? _module;

        /// <inheritdoc/>
        public override bool IsDirty => base.IsDirty || Module?.IsDirty == true;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.Module;

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public Module? Module {
            get => _module;
            set {
                _module = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        public override void Clean() {
            base.Clean();
            Module?.Clean();
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Module}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _module = Module.ReadFromStream(reader.BaseStream, context);

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            // Satisfy the contract with a null module by writing an invalid module type.
            if (_module is null) {
                writer.Write(ushort.MaxValue);
            } else {
                _module.WriteToStream(writer.BaseStream, context);
            }
        }
    }
}
