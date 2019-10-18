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
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using Orion.Utils;
using Orion.World.TileEntities;

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Represents a tile entity that is transmitted over the network.
    /// </summary>
    public abstract class NetworkTileEntity : IDirtiable {
        private static readonly Dictionary<TileEntityType, Func<NetworkTileEntity>> _constructors =
            new Dictionary<TileEntityType, Func<NetworkTileEntity>> {
                [TileEntityType.TargetDummy] = () => new NetworkTargetDummy(),
                [TileEntityType.ItemFrame] = () => new NetworkItemFrame(),
                [TileEntityType.LogicSensor] = () => new NetworkLogicSensor(),
                [TileEntityType.Chest] = () => new NetworkChest(),
                [TileEntityType.Sign] = () => new NetworkSign()
            };

        private int _index;
        private int _x;
        private int _y;

        /// <inheritdoc cref="ITileEntity.Type"/>
        public abstract TileEntityType Type { get; }

        /// <inheritdoc cref="ITileEntity.Index"/>
        public int Index {
            get => _index;
            set {
                _index = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc cref="ITileEntity.X"/>
        public int X {
            get => _x;
            set {
                _x = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc cref="ITileEntity.Y"/>
        public int Y {
            get => _y;
            set {
                _y = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc/>
        public bool IsDirty { get; private protected set; }

        // Prevent outside inheritance.
        private protected NetworkTileEntity() { }

        internal static NetworkTileEntity ReadFromReader(
                BinaryReader reader, int? indexHint = null, TileEntityType? typeHint = null) {
            Debug.Assert(reader != null, "reader should not be null");

            // The type hint allows us to reuse code for NetworkChests and NetworkSigns.
            var tileEntityType = typeHint ?? (TileEntityType)reader.ReadByte();
            if (!_constructors.TryGetValue(tileEntityType, out var tileEntityConstructor)) {
                // Not localized because this string is developer-facing.
                throw new PacketException("Tile entity type is invalid.");
            }

            var tileEntity = tileEntityConstructor();
            tileEntity.ReadFromReaderImpl(reader, indexHint);
            return tileEntity;
        }

        /// <inheritdoc/>
        public void Clean() => IsDirty = false;

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type} @ ({X}, {Y})";

        private void ReadFromReaderImpl(BinaryReader reader, int? indexHint = null) {
            _index = indexHint ?? ((short)Type > byte.MaxValue ? reader.ReadInt16() : reader.ReadInt32());
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            ReadFromReaderImpl(reader);
        }

        internal void WriteToWriter(BinaryWriter writer, bool shouldIncludeIndex) {
            // NetworkChests and NetworkSigns don't store type and have an Int16 index.
            if ((short)Type > byte.MaxValue) {
                if (shouldIncludeIndex) {
                    writer.Write((short)_index);
                }
            } else {
                writer.Write((byte)Type);
                if (shouldIncludeIndex) {
                    writer.Write(_index);
                }
            }

            writer.Write((short)_x);
            writer.Write((short)_y);
            WriteToWriterImpl(writer);
        }

        private protected abstract void ReadFromReaderImpl(BinaryReader reader);
        private protected abstract void WriteToWriterImpl(BinaryWriter writer);
    }
}
