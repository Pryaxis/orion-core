// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

        internal override PacketType Type => PacketType.Module;

        /// <inheritdoc />
        public override string ToString() => $"{Type}[{Module}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            Module = Module.ReadFromStream(reader.BaseStream, context);
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            Module.WriteToStream(writer.BaseStream, context);
        }
    }
}
