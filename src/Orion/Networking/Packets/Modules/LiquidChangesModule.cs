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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Module sent for liquid changes.
    /// </summary>
    public class LiquidChangesModule : Module {
        /// <summary>
        /// Gets the liquid changes.
        /// </summary>
        public IList<LiquidChange> LiquidChanges { get; } = new List<LiquidChange>();

        private protected override ModuleType Type => ModuleType.LiquidChanges;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(ModuleType.LiquidChanges)}[...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            var numberOfLiquidChanges = reader.ReadUInt16();
            for (var i = 0; i < numberOfLiquidChanges; ++i) {
                var tileXY = reader.ReadInt32();
                var tileX = (short)((tileXY >> 16) & ushort.MaxValue);
                var tileY = (short)(tileXY & ushort.MaxValue);
                LiquidChanges.Add(new LiquidChange(tileX, tileY, reader.ReadByte(), (LiquidType)reader.ReadByte()));
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((ushort)LiquidChanges.Count);
            foreach (var liquidChange in LiquidChanges) {
                var tileXY = ((ushort)liquidChange.TileX << 16) | (ushort)liquidChange.TileY;
                writer.Write(tileXY);
                writer.Write(liquidChange.LiquidAmount);
                writer.Write((byte)liquidChange.LiquidType);
            }
        }
    }
}
