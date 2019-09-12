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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Utils;
using Orion.World.TileEntities;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Represents a tile entity that is transmitted over the network.
    /// </summary>
    public abstract class NetworkTileEntity : AnnotatableObject, ITileEntity {
        private static readonly Dictionary<TileEntityType, Func<NetworkTileEntity>> TileEntityConstructors =
            new Dictionary<TileEntityType, Func<NetworkTileEntity>> {
                [TileEntityType.TargetDummy] = () => new NetworkTargetDummy(),
                [TileEntityType.ItemFrame] = () => new NetworkItemFrame(),
                [TileEntityType.LogicSensor] = () => new NetworkLogicSensor()
            };

        /// <inheritdoc />
        public int Index { get; set; }

        /// <inheritdoc />
        public int X { get; set; }

        /// <inheritdoc />
        public int Y { get; set; }

        private protected abstract TileEntityType Type { get; }

        internal static NetworkTileEntity FromReader(BinaryReader reader, bool shouldIncludeIndex) {
            var type = (TileEntityType)reader.ReadByte();
            var tileEntity = TileEntityConstructors[type]();
            tileEntity.ReadFromReader(reader, shouldIncludeIndex);
            return tileEntity;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type} @ ({X}, {Y})";

        private void ReadFromReader(BinaryReader reader, bool shouldIncludeIndex) {
            if (shouldIncludeIndex) {
                Index = reader.ReadInt32();
            }

            X = reader.ReadInt16();
            Y = reader.ReadInt16();
            ReadFromReaderImpl(reader);
        }

        internal void WriteToWriter(BinaryWriter writer, bool shouldIncludeIndex) {
            writer.Write((byte)Type);
            if (shouldIncludeIndex) {
                writer.Write(Index);
            }

            writer.Write((short)X);
            writer.Write((short)Y);
            WriteToWriterImpl(writer);
        }

        private protected abstract void ReadFromReaderImpl(BinaryReader reader);
        private protected abstract void WriteToWriterImpl(BinaryWriter writer);
    }
}
