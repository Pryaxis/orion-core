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

using System.Diagnostics;
using Orion.Utils;

namespace Orion.World.TileEntities {
    // OrionTileEntity is generic so that we only use a single wrapped field instead of two wrapped fields per entity.
    internal abstract class OrionTileEntity<TTerrariaTileEntity> : AnnotatableObject, ITileEntity
        where TTerrariaTileEntity : Terraria.DataStructures.TileEntity {
        public TileEntityType Type => (TileEntityType)Wrapped.type;

        public int Index => Wrapped.ID;

        public int X {
            get => Wrapped.Position.X;
            set => Wrapped.Position = new Terraria.DataStructures.Point16(value, Y);
        }

        public int Y {
            get => Wrapped.Position.Y;
            set => Wrapped.Position = new Terraria.DataStructures.Point16(X, value);
        }

        // internal for testing.
        internal TTerrariaTileEntity Wrapped { get; }

        private protected OrionTileEntity(TTerrariaTileEntity terrariaTileEntity) {
            Debug.Assert(terrariaTileEntity != null, "terrariaTileEntity != null");

            Wrapped = terrariaTileEntity;
        }
    }
}
