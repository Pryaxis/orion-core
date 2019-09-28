﻿// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Orion.World.Tiles.Extensions;
using OTAPI.Tile;

namespace Orion.World.Tiles {
    internal sealed unsafe class TileToITileBridge : ITile {
        private readonly Tile* _tile;

        public ushort type {
            get => (ushort)_tile->BlockType;
            set => _tile->BlockType = (BlockType)value;
        }

        public byte wall {
            get => (byte)_tile->WallType;
            set => _tile->WallType = (WallType)value;
        }

        public byte liquid {
            get => _tile->LiquidAmount;
            set => _tile->LiquidAmount = value;
        }

        public short sTileHeader {
            get => _tile->_sTileHeader;
            set => _tile->_sTileHeader = value;
        }

        public byte bTileHeader {
            get => _tile->_bTileHeader;
            set => _tile->_bTileHeader = value;
        }

        // This is a no-op since the value isn't observable.
        [ExcludeFromCodeCoverage]
        public byte bTileHeader2 {
            get => 0;
            set { }
        }

        public byte bTileHeader3 {
            get => _tile->_bTileHeader2;
            set => _tile->_bTileHeader2 = value;
        }

        public short frameX {
            get => _tile->BlockFrameX;
            set => _tile->BlockFrameX = value;
        }

        public short frameY {
            get => _tile->BlockFrameY;
            set => _tile->BlockFrameY = value;
        }

        // TODO: write tests for this
        public int collisionType {
            get {
                if (!active()) return 0;
                if (halfBrick()) return 2;

                var slope = this.slope();
                if (slope > 0) return slope + 2;

                return Terraria.Main.tileSolid[type] && !Terraria.Main.tileSolid[type] ? 1 : -1;
            }
        }

        public TileToITileBridge(Tile* tile) {
            Debug.Assert(tile != null, "tile != null");

            _tile = tile;
        }

        [ExcludeFromCodeCoverage]
        [Pure]
        public object Clone() => MemberwiseClone();

        public void CopyFrom(ITile from) {
            if (from is TileToITileBridge bridge) {
                byte* toPtr = (byte*)_tile;
                byte* fromPtr = (byte*)bridge._tile;

                *(int*)toPtr = *(int*)fromPtr;
                *(int*)(toPtr + 4) = *(int*)(fromPtr + 4);
                *(int*)(toPtr + 8) = *(int*)(fromPtr + 8);
                return;
            }

            type = from.type;
            wall = from.wall;
            liquid = from.liquid;
            sTileHeader = from.sTileHeader;
            bTileHeader = from.bTileHeader;
            bTileHeader3 = from.bTileHeader3;
            frameX = from.frameX;
            frameY = from.frameY;
        }

        public void ClearEverything() {
            byte* ptr = (byte*)_tile;

            *(int*)ptr = 0;
            *(int*)(ptr + 4) = 0;
            *(int*)(ptr + 8) = 0;
        }

        public void ClearTile() {
            slope(0);
            halfBrick(false);
            active(false);
        }

        public void ResetToType(ushort newType) {
            byte* ptr = (byte*)_tile;

            *(int*)ptr = newType;
            *(int*)(ptr + 4) = 32;
            *(int*)(ptr + 8) = 0;
        }

        public void ClearMetadata() {
            byte* ptr = (byte*)_tile;

            *(ptr + 3) = 0;
            *(int*)(ptr + 4) = 0;
            *(int*)(ptr + 8) = 0;
        }

        [Pure]
        public bool isTheSameAs(ITile? compTile) {
            if (compTile is null) return false;
            if (sTileHeader != compTile.sTileHeader) return false;

            if (active()) {
                if (type != compTile.type) return false;

                if (((BlockType)type).AreFramesImportant() &&
                    (frameX != compTile.frameX || frameY != compTile.frameY)) {
                    return false;
                }
            }

            if (wall != compTile.wall || liquid != compTile.liquid) return false;

            if (compTile.liquid == 0) {
                if (wallColor() != compTile.wallColor()) return false;
                if (wire4() != compTile.wire4()) return false;
            } else if (bTileHeader != compTile.bTileHeader) {
                return false;
            }

            return true;
        }

        [Pure]
        public byte color() => (byte)_tile->BlockColor;

        public void color(byte color) => _tile->BlockColor = (PaintColor)color;

        [Pure]
        public bool active() => _tile->IsBlockActive;

        public void active(bool active) => _tile->IsBlockActive = active;

        [Pure]
        public bool inActive() => _tile->IsBlockActuated;

        public void inActive(bool inActive) => _tile->IsBlockActuated = inActive;

        [Pure]
        public bool wire() => _tile->HasRedWire;

        public void wire(bool wire) => _tile->HasRedWire = wire;

        [Pure]
        public bool wire2() => _tile->HasBlueWire;

        public void wire2(bool wire2) => _tile->HasBlueWire = wire2;

        [Pure]
        public bool wire3() => _tile->HasGreenWire;

        public void wire3(bool wire3) => _tile->HasGreenWire = wire3;

        [Pure]
        public bool halfBrick() => _tile->IsBlockHalved;

        public void halfBrick(bool halfBrick) => _tile->IsBlockHalved = halfBrick;

        [Pure]
        public bool actuator() => _tile->HasActuator;

        public void actuator(bool actuator) => _tile->HasActuator = actuator;

        [Pure]
        public byte slope() => (byte)_tile->Slope;

        public void slope(byte slope) => _tile->Slope = (Slope)slope;

        [Pure]
        public byte wallColor() => (byte)_tile->WallColor;

        public void wallColor(byte wallColor) => _tile->WallColor = (PaintColor)wallColor;

        [Pure]
        public bool lava() => _tile->IsLava;

        public void lava(bool lava) => _tile->IsLava = lava;

        [Pure]
        public bool honey() => _tile->IsHoney;

        public void honey(bool honey) => _tile->IsHoney = honey;

        [Pure]
        public byte liquidType() => (byte)_tile->LiquidType;

        public void liquidType(int liquidType) => _tile->LiquidType = (LiquidType)liquidType;

        [Pure]
        public bool wire4() => _tile->HasYellowWire;

        public void wire4(bool wire4) => _tile->HasYellowWire = wire4;

        [Pure]
        public bool checkingLiquid() => _tile->IsCheckingLiquid;

        public void checkingLiquid(bool checkingLiquid) => _tile->IsCheckingLiquid = checkingLiquid;

        [Pure]
        public bool skipLiquid() => _tile->ShouldSkipLiquid;

        public void skipLiquid(bool skipLiquid) => _tile->ShouldSkipLiquid = skipLiquid;

        [Pure]
        public bool nactive() => (sTileHeader & 96) == 32;

        [Pure]
        public bool topSlope() {
            var slope = this.slope();
            return slope == 1 || slope == 2;
        }

        [Pure]
        public bool bottomSlope() {
            var slope = this.slope();
            return slope == 3 || slope == 4;
        }

        [Pure]
        public bool leftSlope() {
            var slope = this.slope();
            return slope == 2 || slope == 4;
        }

        [Pure]
        public bool rightSlope() {
            var slope = this.slope();
            return slope == 1 || slope == 3;
        }

        [Pure]
        public bool HasSameSlope(ITile tile) => (sTileHeader & 29696) == (tile.sTileHeader & 29696);

        [Pure]
        public int blockType() {
            if (halfBrick()) return 1;

            var slope = this.slope();
            return slope > 0 ? slope + 1 : 0;
        }

        // These are no-ops since their values are not observable.
        [Pure, ExcludeFromCodeCoverage]
        public int wallFrameX() => 0;

        [ExcludeFromCodeCoverage]
        public void wallFrameX(int wallFrameX) { }

        [Pure, ExcludeFromCodeCoverage]
        public byte frameNumber() => 0;

        [ExcludeFromCodeCoverage]
        public void frameNumber(byte frameNumber) { }

        [Pure, ExcludeFromCodeCoverage]
        public byte wallFrameNumber() => 0;

        [ExcludeFromCodeCoverage]
        public void wallFrameNumber(byte wallFrameNumber) { }

        [Pure, ExcludeFromCodeCoverage]
        public int wallFrameY() => 0;

        [ExcludeFromCodeCoverage]
        public void wallFrameY(int wallFrameY) { }

        [Pure, ExcludeFromCodeCoverage]
        public Color actColor(Color oldColor) => default;
    }
}