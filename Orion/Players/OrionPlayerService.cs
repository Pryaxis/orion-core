using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Orion.Framework;
using Orion.Players.Events;
using OTAPI;

namespace Orion.Players {
    /// <summary>
    /// Orion's implementation of <see cref="IPlayerService"/>.
    /// </summary>
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        private readonly IList<Terraria.Player> _terrariaPlayers;
        private readonly IList<OrionPlayer> _players;

        public override string Author => "Pryaxis";
        public override string Name => "Orion Player Service";

        public int Count => _players.Count - 1;

        public IPlayer this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                if (_players[index]?.WrappedPlayer != _terrariaPlayers[index]) {
                    _players[index] = new OrionPlayer(_terrariaPlayers[index]);
                }

                var player = _players[index];
                Debug.Assert(player != null, $"{nameof(player)} should not be null.");
                Debug.Assert(player.WrappedPlayer != null, $"{nameof(player.WrappedPlayer)} should not be null.");
                return player;
            }
        }

        public event EventHandler<GreetingPlayerEventArgs> GreetingPlayer;
        public event EventHandler<UpdatingPlayerEventArgs> UpdatingPlayer;
        public event EventHandler<UpdatedPlayerEventArgs> UpdatedPlayer;

        public OrionPlayerService() {
            _terrariaPlayers = Terraria.Main.player;
            _players = new OrionPlayer[_terrariaPlayers.Count];
            
            Hooks.Player.PreGreet = PreGreetHandler;
            Hooks.Player.PreUpdate = PreUpdateHandler;
            Hooks.Player.PostUpdate = PostUpdateHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            Hooks.Player.PreGreet = null;
            Hooks.Player.PreUpdate = null;
            Hooks.Player.PostUpdate = null;
        }

        public IEnumerator<IPlayer> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        private HookResult PreGreetHandler(ref int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new GreetingPlayerEventArgs(player);
            GreetingPlayer?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreUpdateHandler(Terraria.Player terrariaPlayer, ref int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new UpdatingPlayerEventArgs(player);
            UpdatingPlayer?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostUpdateHandler(Terraria.Player terrariaPlayer, int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new UpdatedPlayerEventArgs(player);
            UpdatedPlayer?.Invoke(this, args);
        }
    }
}
