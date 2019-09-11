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
using Orion.Hooks;
using Orion.Players.Events;
using OTAPI;
using Terraria;

namespace Orion.Players {
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        private readonly IList<Player> _terrariaPlayers;
        private readonly IList<OrionPlayer> _players;

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        // We need to subtract 1 from the count. This is because Terraria actually has an extra slot which is reserved
        // as a failure index.
        public int Count => _players.Count - 1;

        public IPlayer this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                if (_players[index]?.Wrapped != _terrariaPlayers[index]) {
                    _players[index] = new OrionPlayer(_terrariaPlayers[index]);
                }

                var player = _players[index];
                Debug.Assert(player != null, $"{nameof(player)} should not be null.");
                Debug.Assert(player.Wrapped != null, $"{nameof(player.Wrapped)} should not be null.");
                return player;
            }
        }

        public HookHandlerCollection<GreetingPlayerEventArgs> GreetingPlayer { get; set; }
        public HookHandlerCollection<UpdatingPlayerEventArgs> UpdatingPlayer { get; set; }
        public HookHandlerCollection<UpdatedPlayerEventArgs> UpdatedPlayer { get; set; }

        public OrionPlayerService() {
            _terrariaPlayers = Main.player;
            _players = new OrionPlayer[_terrariaPlayers.Count];

            OTAPI.Hooks.Player.PreGreet = PreGreetHandler;
            OTAPI.Hooks.Player.PreUpdate = PreUpdateHandler;
            OTAPI.Hooks.Player.PostUpdate = PostUpdateHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) return;

            OTAPI.Hooks.Player.PreGreet = null;
            OTAPI.Hooks.Player.PreUpdate = null;
            OTAPI.Hooks.Player.PostUpdate = null;
        }

        public IEnumerator<IPlayer> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        private HookResult PreGreetHandler(ref int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new GreetingPlayerEventArgs(player);
            GreetingPlayer?.Invoke(this, args);
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreUpdateHandler(Player terrariaPlayer, ref int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new UpdatingPlayerEventArgs(player);
            UpdatingPlayer?.Invoke(this, args);
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostUpdateHandler(Player terrariaPlayer, int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new UpdatedPlayerEventArgs(player);
            UpdatedPlayer?.Invoke(this, args);
        }
    }
}
