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
using Orion.Collections;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Events.World.Signs;
using Orion.Framework;
using Orion.Packets.World.Signs;
using Serilog;

namespace Orion.World.Signs {
    [Binding("orion-signs", Author = "Pryaxis", Priority = BindingPriority.Lowest)]
    internal sealed class OrionSignService : OrionService, ISignService {
        public OrionSignService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            // Construct the `Signs` array.
            Signs = new WrappedReadOnlyList<OrionSign, Terraria.Sign?>(
                Terraria.Main.sign, (signIndex, terrariaSign) => new OrionSign(signIndex, terrariaSign));

            Kernel.RegisterHandlers(this, Log);
        }

        public IReadOnlyList<ISign> Signs { get; }

        public override void Dispose() {
            Kernel.DeregisterHandlers(this, Log);
        }

        // =============================================================================================================
        // Sign event publishers
        //

        [EventHandler("orion-signs", Priority = EventPriority.Lowest)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Implicitly called")]
        private void OnSignReadPacket(PacketReceiveEvent<SignReadPacket> evt) {
            var player = evt.Sender;
            ref var packet = ref evt.Packet;
            var evt2 = new SignReadEvent(player, packet.X, packet.Y);
            Kernel.Raise(evt2, Log);
            evt.CancellationReason = evt2.CancellationReason;
        }
    }
}
