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

using System;
using System.Diagnostics.CodeAnalysis;
using Orion.Entities;
using Orion.Events;
using Orion.Events.Networking;

namespace Orion.Networking.Impl {
    internal sealed class NetworkService : OrionService, INetworkService {
        private readonly Lazy<IPlayerService> _playerService;

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";
        public EventHandlerCollection<PacketReceiveEventArgs> PacketReceive { get; set; }
        public EventHandlerCollection<PacketSendEventArgs> PacketSend { get; set; }

        public NetworkService(Lazy<IPlayerService> playerService) {
            _playerService = playerService;
        }
    }
}
