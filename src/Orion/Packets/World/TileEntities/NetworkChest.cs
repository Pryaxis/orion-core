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
using System.IO;
using Orion.World.TileEntities;

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Represents a chest that is transmitted over the network.
    /// </summary>
    public sealed class NetworkChest : NetworkTileEntity {
        private string _name = string.Empty;

        /// <inheritdoc/>
        public override TileEntityType Type => TileEntityType.Chest;

        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        /// <value>The chest's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Name {
            get => _name;
            set {
                _name = value ?? throw new ArgumentNullException(nameof(value));
                IsDirty = true;
            }
        }

        private protected override void ReadFromReaderImpl(BinaryReader reader) => _name = reader.ReadString();
        private protected override void WriteToWriterImpl(BinaryWriter writer) => writer.Write(_name);
    }
}
