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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Orion.Collections;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Events.World.Chests;
using Orion.Framework;
using Orion.Packets.World.Chests;
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

        private IChest? FindChest(int x, int y) => Chests.FirstOrDefault(s => s.IsActive && s.X == x && s.Y == y);

        // =============================================================================================================
        // Chest event publishers
        //

        [EventHandler("orion-chests", Priority = EventPriority.Lowest)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Implicitly used")]
        private void OnChestOpenPacket(PacketReceiveEvent<ChestOpenPacket> evt) {
            ref var packet = ref evt.Packet;
            var chest = FindChest(packet.X, packet.Y);
            if (chest is null) {
                return;
            }

            var player = evt.Sender;
            var evt2 = new ChestOpenEvent(chest, player);
            Kernel.Raise(evt2, Log);
            evt.CancellationReason = evt2.CancellationReason;
        }
    }
}
