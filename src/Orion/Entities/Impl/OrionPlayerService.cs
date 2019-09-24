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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Orion.Events;
using Orion.Events.Entities;

namespace Orion.Entities.Impl {
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        [NotNull, ItemNotNull] private readonly IList<Terraria.Player> _terrariaPlayers;
        [NotNull, ItemCanBeNull] private readonly IList<OrionPlayer> _players;

        // Subtract 1 from the count. This is because Terraria has an extra slot.
        public int Count => _players.Count - 1;

        [NotNull]
        public IPlayer this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                if (_players[index]?.Wrapped != _terrariaPlayers[index]) {
                    _players[index] = new OrionPlayer(_terrariaPlayers[index]);
                }

                Debug.Assert(_players[index] != null, "_players[index] != null");
                return _players[index];
            }
        }

        public EventHandlerCollection<PlayerConnectEventArgs> PlayerConnect { get; set; }
        public EventHandlerCollection<PlayerDataEventArgs> PlayerData { get; set; }
        public EventHandlerCollection<PlayerInventorySlotEventArgs> PlayerInventorySlot { get; set; }
        public EventHandlerCollection<PlayerJoinEventArgs> PlayerJoin { get; set; }
        public EventHandlerCollection<PlayerDisconnectedEventArgs> PlayerDisconnected { get; set; }

        public OrionPlayerService() {
            Debug.Assert(Terraria.Main.player != null, "Terraria.Main.player != null");
            Debug.Assert(Terraria.Main.player.All(p => p != null), "Terraria.Main.player.All(p => p != null)");

            _terrariaPlayers = Terraria.Main.player;
            _players = new OrionPlayer[_terrariaPlayers.Count];
        }

        public IEnumerator<IPlayer> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
