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
using JetBrains.Annotations;
using Orion.Utils;
using Orion.World.TileEntities;

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Represents a tile entity that is transmitted over the network.
    /// </summary>
    [PublicAPI]
    public abstract class NetworkTileEntity : AnnotatableObject, ITileEntity, IDirtiable {
        [NotNull] private static readonly Dictionary<TileEntityType, Func<NetworkTileEntity>> Constructors =
            new Dictionary<TileEntityType, Func<NetworkTileEntity>> {
                [TileEntityType.TargetDummy] = () => new NetworkTargetDummy(),
                [TileEntityType.ItemFrame] = () => new NetworkItemFrame(),
                [TileEntityType.LogicSensor] = () => new NetworkLogicSensor(),
                [TileEntityType.Chest] = () => new NetworkChest(),
                [TileEntityType.Sign] = () => new NetworkSign()
            };

        internal int _index;
        private int _x;
        private int _y;

        /// <inheritdoc />
        public abstract TileEntityType Type { get; }

        /// <inheritdoc />
        public int Index {
            get => _index;
            set {
                _index = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public int X {
            get => _x;
            set {
                _x = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public int Y {
            get => _y;
            set {
                _y = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public bool IsDirty { get; private protected set; }

        // Prevent outside inheritance.
        private protected NetworkTileEntity() { }

        [NotNull]
        internal static NetworkTileEntity ReadFromReader([NotNull] BinaryReader reader, bool shouldIncludeIndex,
                                                         [CanBeNull] TileEntityType? typeHint = null) {
            // The type hint allows us to reuse code for NetworkChests and NetworkSigns.
            var tileEntityType = typeHint ?? (TileEntityType)reader.ReadByte();
            if (!Constructors.TryGetValue(tileEntityType, out var tileEntityConstructor)) {
                throw new PacketException("Tile entity type is invalid.");
            }

            var tileEntity = tileEntityConstructor();
            tileEntity.ReadFromReaderImpl(reader, shouldIncludeIndex);
            return tileEntity;
        }

        /// <inheritdoc />
        public void Clean() {
            IsDirty = false;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type} @ ({X}, {Y})";

        private void ReadFromReaderImpl([NotNull] BinaryReader reader, bool shouldIncludeIndex) {
            if (shouldIncludeIndex) {
                // NetworkChests and NetworkSigns have an Int16 index.
                _index = (short)Type > byte.MaxValue ? reader.ReadInt16() : reader.ReadInt32();
            }

            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            ReadFromReaderImpl(reader);
        }

        internal void WriteToWriter([NotNull] BinaryWriter writer, bool shouldIncludeIndex) {
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

        private protected abstract void ReadFromReaderImpl([NotNull] BinaryReader reader);
        private protected abstract void WriteToWriterImpl([NotNull] BinaryWriter writer);
    }
}
