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
using Orion.Events;
using Orion.Networking.Packets;
using Orion.Utils;
using Orion.World.TileEntities;

namespace Orion.Networking {
    /// <summary>
    /// Represents a tile entity that is transmitted over the network.
    /// </summary>
    public abstract class NetworkTileEntity : AnnotatableObject, ITileEntity, IDirtiable {
        private static readonly Dictionary<TileEntityType, Func<NetworkTileEntity>> Constructors =
            new Dictionary<TileEntityType, Func<NetworkTileEntity>> {
                [TileEntityType.Chest] = () => new NetChest(),
                [TileEntityType.Sign] = () => new NetSign(),
                [TileEntityType.TargetDummy] = () => new NetworkTargetDummy(),
                [TileEntityType.ItemFrame] = () => new NetworkItemFrame(),
                [TileEntityType.LogicSensor] = () => new NetworkLogicSensor()
            };

        private int _index;
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

        internal static NetworkTileEntity FromReader(BinaryReader reader, bool shouldIncludeIndex, TileEntityType typeHint = null) {
            var tileEntityType = typeHint ?? TileEntityType.FromId(reader.ReadByte()) ??
                                 throw new PacketException("Tile entity type is invalid.");
            var tileEntity = Constructors[tileEntityType]();
            tileEntity.ReadFromReader(reader, shouldIncludeIndex);
            return tileEntity;
        }

        /// <inheritdoc />
        public void Clean() {
            IsDirty = false;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type} @ ({X}, {Y})";

        private void ReadFromReader(BinaryReader reader, bool shouldIncludeIndex) {
            if (shouldIncludeIndex) {
                // Chests and signs have an Int16 index.
                Index = Type.Id == byte.MaxValue ? reader.ReadInt16() : reader.ReadInt32();
            }

            X = reader.ReadInt16();
            Y = reader.ReadInt16();
            ReadFromReaderImpl(reader);
        }

        internal void WriteToWriter(BinaryWriter writer, bool shouldIncludeIndex) {
            // Chests and signs don't store type and have an Int16 index.
            if (Type.Id == byte.MaxValue) {
                if (shouldIncludeIndex) {
                    writer.Write((short)Index);
                }
            } else {
                writer.Write(Type.Id);
                if (shouldIncludeIndex) {
                    writer.Write(Index);
                }
            }

            writer.Write((short)X);
            writer.Write((short)Y);
            WriteToWriterImpl(writer);
        }

        private protected abstract void ReadFromReaderImpl(BinaryReader reader);
        private protected abstract void WriteToWriterImpl(BinaryWriter writer);
    }
}
