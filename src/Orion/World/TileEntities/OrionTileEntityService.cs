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
using System.Diagnostics;
using Orion.Utils;
using Terraria;
using TerrariaChest = Terraria.Chest;
using TerrariaTargetDummy = Terraria.GameContent.Tile_Entities.TETrainingDummy;
using TerrariaItem = Terraria.Item;
using TerrariaItemFrame = Terraria.GameContent.Tile_Entities.TEItemFrame;
using TerrariaLogicSensor = Terraria.GameContent.Tile_Entities.TELogicSensor;
using TerrariaSign = Terraria.Sign;
using TerrariaTileEntity = Terraria.DataStructures.TileEntity;

namespace Orion.World.TileEntities {
    internal sealed class OrionTileEntityService : OrionService, ITileEntityService {
        public IReadOnlyArray<IChest?> Chests { get; }
        public IReadOnlyArray<ISign?> Signs { get; }

        public OrionTileEntityService() {
            Debug.Assert(Main.chest != null, "Main.chest != null");
            Debug.Assert(Main.sign != null, "Main.sign != null");

            Chests = new WrappedNullableReadOnlyArray<OrionChest, TerrariaChest>(
                Main.chest,
                (chestIndex, terrariaChest) => new OrionChest(chestIndex, terrariaChest));
            Signs = new WrappedNullableReadOnlyArray<OrionSign, TerrariaSign>(
                Main.sign,
                (signIndex, terrariaSign) => new OrionSign(signIndex, terrariaSign));
        }

        public ITileEntity? AddTileEntity(TileEntityType tileEntityType, int x, int y) {
            if (GetTileEntity(x, y) != null) return null;

            static ITileEntity? AddChest(int x, int y) {
                for (var i = 0; i < Main.chest.Length; ++i) {
                    ref var terrariaChest = ref Main.chest[i];
                    if (terrariaChest == null) {
                        terrariaChest = new TerrariaChest {
                            x = x,
                            y = y
                        };
                        for (var j = 0; j < TerrariaChest.maxItems; ++j) {
                            terrariaChest.item[j] = new TerrariaItem();
                        }

                        return new OrionChest(i, terrariaChest);
                    }
                }

                return null;
            }

            static ITileEntity? AddSign(int x, int y) {
                for (var i = 0; i < Main.sign.Length; ++i) {
                    ref var terrariaSign = ref Main.sign[i];
                    if (terrariaSign == null) {
                        terrariaSign = new TerrariaSign {
                            x = x,
                            y = y,
                            text = ""
                        };

                        return new OrionSign(i, terrariaSign);
                    }
                }

                return null;
            }

            static ITileEntity AddTargetDummy(int x, int y) {
                var targetDummyIndex = TerrariaTargetDummy.Place(x, y);
                return new OrionTargetDummy((TerrariaTargetDummy)TerrariaTileEntity.ByID[targetDummyIndex]);
            }

            static ITileEntity AddItemFrame(int x, int y) {
                var targetDummyIndex = TerrariaItemFrame.Place(x, y);
                return new OrionItemFrame((TerrariaItemFrame)TerrariaTileEntity.ByID[targetDummyIndex]);
            }

            static ITileEntity AddLogicSensor(int x, int y) {
                var targetDummyIndex = TerrariaLogicSensor.Place(x, y);
                return new OrionLogicSensor((TerrariaLogicSensor)TerrariaTileEntity.ByID[targetDummyIndex]);
            }

            return tileEntityType switch {
                TileEntityType.Chest => AddChest(x, y),
                TileEntityType.Sign => AddSign(x, y),
                TileEntityType.TargetDummy => AddTargetDummy(x, y),
                TileEntityType.ItemFrame => AddItemFrame(x, y),
                TileEntityType.LogicSensor => AddLogicSensor(x, y),
                _ => throw new ArgumentException("Tile entity type is invalid.", nameof(tileEntityType))
            };
        }

        public ITileEntity? GetTileEntity(int x, int y) {
            throw new NotImplementedException();
        }

        public bool RemoveTileEntity(ITileEntity tileEntity) {
            throw new NotImplementedException();
        }
    }
}
