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

using System;
using System.Diagnostics;
using Orion.Entities;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Packets;

namespace Orion.Players {
    internal sealed class OrionPlayer : OrionEntity<Terraria.Player>, IPlayer {
        private readonly OrionPlayerService _playerService;
        private readonly byte[] _sendBuffer = new byte[ushort.MaxValue];

        public override string Name {
            get => Wrapped.name;
            set => Wrapped.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        // We need to inject an `OrionPlayerService` so that we can raise a `PacketSendEvent`.
        public OrionPlayer(int playerIndex, Terraria.Player terrariaPlayer, OrionPlayerService playerService)
                : base(playerIndex, terrariaPlayer) {
            Debug.Assert(playerService != null);

            _playerService = playerService;
        }

        public OrionPlayer(Terraria.Player terrariaPlayer, OrionPlayerService playerService)
            : this(-1, terrariaPlayer, playerService) {
            Debug.Assert(playerService != null);

            _playerService = playerService;
        }

        public void SendPacket<TPacket>(ref TPacket packet) where TPacket : struct, IPacket {
            var terrariaClient = Terraria.Netplay.Clients[Index];
            if (!terrariaClient.IsConnected()) {
                return;
            }

            var evt = new PacketSendEvent<TPacket>(ref packet, this);
            _playerService.Kernel.Raise(evt, _playerService.Log);
            if (evt.IsCanceled()) {
                return;
            }

            // Write the packet payload. We need to use `evt.Packet` here, since the packet may have been modified.
            Span<byte> span = _sendBuffer;
            evt.Packet.WriteWithHeader(ref span, PacketContext.Server);

            var packetLength = (ushort)(_sendBuffer.Length - span.Length);
            terrariaClient.Socket?.AsyncSend(_sendBuffer, 0, packetLength, terrariaClient.ServerWriteCallBack);
        }
    }
}
