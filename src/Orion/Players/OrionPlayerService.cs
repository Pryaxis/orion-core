using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Orion.Hooks;
using Orion.Players.Events;

namespace Orion.Players {
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        private readonly IList<Terraria.Player> _terrariaPlayers;
        private readonly IList<OrionPlayer> _players;

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";
        [ExcludeFromCodeCoverage] public override string Name => "Orion Player Service";
        
        /*
         * We need to subtract 1 from the count. This is because Terraria actually has an extra slot which is reserved
         * as a failure index.
         */
        public int Count => _players.Count - 1;

        public IPlayer this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

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
            _terrariaPlayers = Terraria.Main.player;
            _players = new OrionPlayer[_terrariaPlayers.Count];
            
            OTAPI.Hooks.Player.PreGreet = PreGreetHandler;
            OTAPI.Hooks.Player.PreUpdate = PreUpdateHandler;
            OTAPI.Hooks.Player.PostUpdate = PostUpdateHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) {
                return;
            }

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


        private OTAPI.HookResult PreGreetHandler(ref int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new GreetingPlayerEventArgs(player);
            GreetingPlayer?.Invoke(this, args);

            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult PreUpdateHandler(Terraria.Player terrariaPlayer, ref int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new UpdatingPlayerEventArgs(player);
            UpdatingPlayer?.Invoke(this, args);

            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private void PostUpdateHandler(Terraria.Player terrariaPlayer, int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new UpdatedPlayerEventArgs(player);
            UpdatedPlayer?.Invoke(this, args);
        }
    }
}
