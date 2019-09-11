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

using System.Diagnostics;
using Orion.Utils;
using TDS = Terraria.DataStructures;

namespace Orion.World.TileEntities {
    internal abstract class OrionTileEntity<TTileEntity> : AnnotatableObject, ITileEntity
        where TTileEntity : TDS.TileEntity {
        public int Index => Wrapped.ID;

        public int X {
            get => Wrapped.Position.X;
            set => Wrapped.Position = new TDS.Point16(value, Y);
        }

        public int Y {
            get => Wrapped.Position.Y;
            set => Wrapped.Position = new TDS.Point16(X, value);
        }

        internal TTileEntity Wrapped { get; }

        protected OrionTileEntity(TTileEntity terrariaTileEntity) {
            Debug.Assert(terrariaTileEntity != null, $"{nameof(terrariaTileEntity)} should not be null.");

            Wrapped = terrariaTileEntity;
        }
    }
}
