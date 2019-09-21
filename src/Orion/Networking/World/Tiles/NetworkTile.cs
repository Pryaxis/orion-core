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
using Orion.Events;
using Orion.Utils;
using Orion.World.Tiles;

namespace Orion.Networking.World.Tiles {
    /// <summary>
    /// Represents a tile that is transmitted over the network.
    /// </summary>
    public sealed class NetworkTile : Tile, IDirtiable {
        private BlockType _blockType = BlockType.Dirt;
        private WallType _wallType = WallType.None;
        private byte _liquidAmount;
        private short _tileHeader;
        private byte _tileHeader2;
        private byte _tileHeader3;
        private byte _tileHeader4;
        private short _blockFrameX;
        private short _blockFrameY;

        /// <inheritdoc />
        public override BlockType BlockType {
            get => _blockType;
            set {
                _blockType = value ?? throw new ArgumentNullException(nameof(value));
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override WallType WallType {
            get => _wallType;
            set {
                _wallType = value ?? throw new ArgumentNullException(nameof(value));
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override byte LiquidAmount {
            get => _liquidAmount;
            set {
                _liquidAmount = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override short TileHeader {
            get => _tileHeader;
            set {
                _tileHeader = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override byte TileHeader2 {
            get => _tileHeader2;
            set {
                _tileHeader2 = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override byte TileHeader3 {
            get => _tileHeader3;
            set {
                _tileHeader3 = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override byte TileHeader4 {
            get => _tileHeader4;
            set {
                _tileHeader4 = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override short BlockFrameX {
            get => _blockFrameX;
            set {
                _blockFrameX = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override short BlockFrameY {
            get => _blockFrameY;
            set {
                _blockFrameY = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public bool IsDirty { get; private set; }

        /// <inheritdoc />
        public void Clean() {
            IsDirty = false;
        }
    }
}
