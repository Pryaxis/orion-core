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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using Orion.Entities;
using Orion.Events;
using Orion.Events.Players;
using Orion.Packets;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    internal sealed class OrionPlayer : OrionEntity<TerrariaPlayer>, IPlayer {
        private readonly IPlayerService _playerService;

        public override string Name {
            get => Wrapped.name;
            set => Wrapped.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public PlayerTeam Team {
            get => (PlayerTeam)Wrapped.team;
            set => Wrapped.team = (int)value;
        }

        public IPlayerStats Stats { get; }
        public IPlayerInventory Inventory { get; }

        // We need to inject IPlayerService so that we can trigger its PacketSend event.
        public OrionPlayer(IPlayerService playerService, TerrariaPlayer terrariaPlayer)
            : this(playerService, -1, terrariaPlayer) { }

        public OrionPlayer(IPlayerService playerService, int playerIndex, TerrariaPlayer terrariaPlayer)
                : base(playerIndex, terrariaPlayer) {
            Debug.Assert(playerService != null, "player service should not be null");

            _playerService = playerService;

            Stats = new OrionPlayerStats(terrariaPlayer);
            Inventory = new OrionPlayerInventory(terrariaPlayer);
        }

        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => Name;

        public void SendPacket(Packet packet) {
            if (packet is null) {
                throw new ArgumentNullException(nameof(packet));
            }

            var terrariaClient = Terraria.Netplay.Clients[Index];
            if (!terrariaClient.IsConnected()) {
                return;
            }

            var args = new PacketSendEvent(this, packet);
            _playerService.PacketSend.Invoke(this, args);
            if (args.IsCanceled()) {
                return;
            }

            // TODO: consider MemoryStream allocation vs reusing buffer here
            var stream = new MemoryStream();
            args.Packet.WriteToStream(stream, PacketContext.Server);
            terrariaClient.Socket?.AsyncSend(
                stream.ToArray(), 0, (int)stream.Length, terrariaClient.ServerWriteCallBack);
        }
    }
}
