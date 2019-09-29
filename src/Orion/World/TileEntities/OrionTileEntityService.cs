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

namespace Orion.World.TileEntities {
    internal sealed class OrionTileEntityService : OrionService, ITileEntityService {
        public IReadOnlyArray<IChest?> Chests { get; }
        public IReadOnlyArray<ISign?> Signs { get; }

        public OrionTileEntityService() {
            Debug.Assert(Terraria.Main.chest != null, "Terraria.Main.chest != null");
            Debug.Assert(Terraria.Main.sign != null, "Terraria.Main.sign != null");

            Chests = new WrappedNullableReadOnlyArray<OrionChest, Terraria.Chest>(
                Terraria.Main.chest,
                (chestIndex, terrariaChest) => new OrionChest(chestIndex, terrariaChest));
            Signs = new WrappedNullableReadOnlyArray<OrionSign, Terraria.Sign>(
                Terraria.Main.sign,
                (signIndex, terrariaSign) => new OrionSign(signIndex, terrariaSign));
        }

        public ITileEntity AddTileEntity(TileEntityType tileEntityType, int x, int y) {
            throw new NotImplementedException();
        }

        public ITileEntity GetTileEntity(int x, int y) {
            throw new NotImplementedException();
        }

        public bool RemoveTileEntity(ITileEntity tileEntity) {
            throw new NotImplementedException();
        }
    }
}
