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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Packets.World.Tiles;
using Orion.Utils;

namespace Orion.Packets.Modules {
    /// <summary>
    /// Module sent for liquids.
    /// </summary>
    public sealed class LiquidsModule : Module {
        private readonly DirtiableList<NetworkLiquid> _liquids = new DirtiableList<NetworkLiquid>();

        /// <inheritdoc/>
        public override bool IsDirty => base.IsDirty || _liquids.IsDirty;

        /// <inheritdoc/>
        public override ModuleType Type => ModuleType.Liquids;

        /// <summary>
        /// Gets the liquids.
        /// </summary>
        public IList<NetworkLiquid> Liquids => _liquids;

        /// <inheritdoc/>
        public override void Clean() {
            base.Clean();
            _liquids.Clean();
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            var numberOfLiquidChanges = reader.ReadUInt16();
            for (var i = 0; i < numberOfLiquidChanges; ++i) {
                _liquids._list.Add(NetworkLiquid.ReadFromReader(reader, true));
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((ushort)_liquids.Count);
            foreach (var liquid in _liquids) {
                liquid.WriteToWriter(writer, true);
            }
        }
    }
}
