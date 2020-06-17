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
using Orion.Core.Collections;
using Orion.Core.Events;
using Orion.Core.Events.Packets;
using Orion.Core.Events.World.Signs;
using Orion.Core.Framework;
using Orion.Core.Packets.World.Signs;
using Serilog;

namespace Orion.Core.World.Signs
{
    [Binding("orion-signs", Author = "Pryaxis", Priority = BindingPriority.Lowest)]
    internal sealed class OrionSignService : OrionService, ISignService
    {
        public OrionSignService(OrionKernel kernel, ILogger log) : base(kernel, log)
        {
            // Construct the `Signs` array.
            Signs = new WrappedReadOnlyList<OrionSign, Terraria.Sign?>(
                Terraria.Main.sign, (signIndex, terrariaSign) => new OrionSign(signIndex, terrariaSign));

            Kernel.Events.RegisterHandlers(this, Log);
        }

        public IReadOnlyList<ISign> Signs { get; }

        public override void Dispose()
        {
            Kernel.Events.DeregisterHandlers(this, Log);
        }

        private ISign? FindSign(int x, int y) => Signs.FirstOrDefault(s => s.IsActive && s.X == x && s.Y == y);

        // =============================================================================================================
        // Sign event publishers
        //

        [EventHandler("orion-signs", Priority = EventPriority.Lowest)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Implicitly used")]
        private void OnSignReadPacket(PacketReceiveEvent<SignReadPacket> evt)
        {
            ref var packet = ref evt.Packet;
            var sign = FindSign(packet.X, packet.Y);
            if (sign is null)
            {
                return;
            }

            ForwardEvent(evt, new SignReadEvent(sign, evt.Sender));
        }

        // Forwards `evt` as `newEvt`.
        private void ForwardEvent<TEvent>(Event evt, TEvent newEvt) where TEvent : Event
        {
            Kernel.Events.Raise(newEvt, Log);
            if (newEvt.IsCanceled)
            {
                evt.Cancel(newEvt.CancellationReason);
            }
        }
    }
}
