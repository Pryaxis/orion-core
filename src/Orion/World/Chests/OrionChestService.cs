// Copyright (c) 2020 Pryaxis & Orion Contributors
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

using System.Collections.Generic;
using Orion.Collections;
using Orion.Framework;
using Serilog;

namespace Orion.World.Chests {
    [Binding("orion-chests", Author = "Pryaxis", Priority = BindingPriority.Lowest)]
    internal sealed class OrionChestService : OrionService, IChestService {
        public OrionChestService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            // Construct the `Chests` array.
            Chests = new WrappedReadOnlyList<OrionChest, Terraria.Chest?>(
                Terraria.Main.chest, (chestIndex, terrariaChest) => new OrionChest(chestIndex, terrariaChest));

            Kernel.RegisterHandlers(this, Log);
        }

        public IReadOnlyList<IChest> Chests { get; }

        public override void Dispose() {
            Kernel.DeregisterHandlers(this, Log);
        }

        // =============================================================================================================
        // Chest event publishers
        //
    }
}
