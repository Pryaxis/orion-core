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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Networking.Tiles;
using Orion.Utils;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Module sent for liquids.
    /// </summary>
    public class LiquidsModule : Module {
        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || Liquids.IsDirty;

        /// <inheritdoc />
        public override ModuleType Type => ModuleType.LiquidChanges;

        /// <summary>
        /// Gets the liquids.
        /// </summary>
        public DirtiableList<NetworkLiquid> Liquids { get; } = new DirtiableList<NetworkLiquid>();

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            Liquids.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(ModuleType.LiquidChanges)}[...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            var numberOfLiquidChanges = reader.ReadUInt16();
            for (var i = 0; i < numberOfLiquidChanges; ++i) {
                Liquids.Add(NetworkLiquid.ReadFromStream(reader.BaseStream, true));
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((ushort)Liquids.Count);
            foreach (var liquid in Liquids) {
                liquid.WriteToStream(writer.BaseStream, true);
            }
        }
    }
}
