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
using Serilog;
using Main = Terraria.Main;
using TerrariaChest = Terraria.Chest;
using TerrariaItem = Terraria.Item;
using TerrariaItemFrame = Terraria.GameContent.Tile_Entities.TEItemFrame;
using TerrariaLogicSensor = Terraria.GameContent.Tile_Entities.TELogicSensor;
using TerrariaSign = Terraria.Sign;
using TerrariaTargetDummy = Terraria.GameContent.Tile_Entities.TETrainingDummy;
using TerrariaTileEntity = Terraria.DataStructures.TileEntity;

namespace Orion.World.TileEntities {
    [Service("orion-tile-entities")]
    internal sealed class OrionTileEntityService : OrionService, ITileEntityService {
        public IReadOnlyArray<IChest?> Chests { get; }
        public IReadOnlyArray<ISign?> Signs { get; }

        public OrionTileEntityService(ILogger log) : base(log) {
            Debug.Assert(log != null, "log should not be null");
            Debug.Assert(Main.chest != null, "Terraria chests should not be null");
            Debug.Assert(Main.sign != null, "Terraria signs should not be null");

            Chests = new WrappedReadOnlyArray<OrionChest, TerrariaChest?>(
                Main.chest, (chestIndex, terrariaChest) => new OrionChest(chestIndex, terrariaChest));
            Signs = new WrappedReadOnlyArray<OrionSign, TerrariaSign?>(
                Main.sign, (signIndex, terrariaSign) => new OrionSign(signIndex, terrariaSign));
        }

        public ITileEntity? AddTileEntity(TileEntityType tileEntityType, int x, int y) {
            if (GetTileEntity(x, y) != null) {
                return null;
            }

            static IChest? AddChest(int x, int y) {
                for (var i = 0; i < Main.chest.Length; ++i) {
                    ref TerrariaChest? terrariaChest = ref Main.chest[i];
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

            static ISign? AddSign(int x, int y) {
                for (var i = 0; i < Main.sign.Length; ++i) {
                    ref TerrariaSign? terrariaSign = ref Main.sign[i];
                    if (terrariaSign == null) {
                        terrariaSign = new TerrariaSign {
                            x = x,
                            y = y,
                            text = string.Empty
                        };

                        return new OrionSign(i, terrariaSign);
                    }
                }

                return null;
            }

            static ITargetDummy AddTargetDummy(int x, int y) {
                var targetDummyIndex = TerrariaTargetDummy.Place(x, y);
                return new OrionTargetDummy((TerrariaTargetDummy)TerrariaTileEntity.ByID[targetDummyIndex]);
            }

            static IItemFrame AddItemFrame(int x, int y) {
                var targetDummyIndex = TerrariaItemFrame.Place(x, y);
                return new OrionItemFrame((TerrariaItemFrame)TerrariaTileEntity.ByID[targetDummyIndex]);
            }

            static ILogicSensor AddLogicSensor(int x, int y) {
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
            static IChest? GetChest(int x, int y) {
                for (var i = 0; i < Main.chest.Length; ++i) {
                    var terrariaChest = Main.chest[i];
                    if (terrariaChest != null && terrariaChest.x == x && terrariaChest.y == y) {
                        return new OrionChest(i, terrariaChest);
                    }
                }

                return null;
            }

            static ISign? GetSign(int x, int y) {
                for (var i = 0; i < Main.sign.Length; ++i) {
                    var terrariaSign = Main.sign[i];
                    if (terrariaSign != null && terrariaSign.x == x && terrariaSign.y == y) {
                        return new OrionSign(i, terrariaSign);
                    }
                }

                return null;
            }

            static ITileEntity? GetTerrariaTileEntity(int x, int y) {
                if (!TerrariaTileEntity.ByPosition.TryGetValue(
                        new Terraria.DataStructures.Point16(x, y), out var terrariaTileEntity)) {
                    return null;
                }

                return terrariaTileEntity switch {
                    TerrariaTargetDummy terrariaTargetDummy => new OrionTargetDummy(terrariaTargetDummy),
                    TerrariaItemFrame terrariaItemFrame => new OrionItemFrame(terrariaItemFrame),
                    TerrariaLogicSensor terrariaLogicSensor => new OrionLogicSensor(terrariaLogicSensor),
                    _ => throw new InvalidOperationException("Tile entity is invalid.")
                };
            }

            return GetChest(x, y) ?? GetSign(x, y) ?? GetTerrariaTileEntity(x, y);
        }

        public bool RemoveTileEntity(ITileEntity tileEntity) {
            if (tileEntity is null) {
                throw new ArgumentNullException(nameof(tileEntity));
            }

            static bool RemoveChest(IChest chest) {
                ref TerrariaChest? terrariaChest = ref Main.chest[chest.Index];
                if (terrariaChest != null && terrariaChest.x == chest.X && terrariaChest.y == chest.Y) {
                    terrariaChest = null;
                    return true;
                }

                return false;
            }

            static bool RemoveSign(ISign sign) {
                ref TerrariaSign? terrariaSign = ref Main.sign[sign.Index];
                if (terrariaSign != null && terrariaSign.x == sign.X && terrariaSign.y == sign.Y) {
                    terrariaSign = null;
                    return true;
                }

                return false;
            }

            static bool RemoveTerrariaTileEntity(ITileEntity tileEntity) {
                var position = new Terraria.DataStructures.Point16(tileEntity.X, tileEntity.Y);

                // Use the & operator here instead of && since both expressions should always be evaluated.
                return TerrariaTileEntity.ByID.Remove(tileEntity.Index) &
                    TerrariaTileEntity.ByPosition.Remove(position);
            }

            return tileEntity switch {
                IChest chest => RemoveChest(chest),
                ISign sign => RemoveSign(sign),
                _ => RemoveTerrariaTileEntity(tileEntity)
            };
        }
    }
}
