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
        private readonly IPlayer[] _players;

        /// <inheritdoc />
        public override string Author => "Pryaxis";

        /// <inheritdoc />
        public override string Name => "Orion Player Service";

        /// <inheritdoc />
        public int Count => Terraria.Main.player.Length;

        /// <inheritdoc />
        public IPlayer this[int index] {
            get {
                if (_players[index]?.WrappedEntity != Terraria.Main.player[index]) {
                    _players[index] = new OrionPlayer(Terraria.Main.player[index]);
                }

                var player = _players[index];
                Debug.Assert(player != null, $"{nameof(player)} should not be null.");
                Debug.Assert(player.WrappedEntity != null, 
                             $"{nameof(player.WrappedEntity)} should not be null.");
                return player;
            }
        }
        
        /// <inheritdoc />
        public event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

        /// <inheritdoc />
        public event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

        /// <inheritdoc />
        public event EventHandler<PlayerQuitEventArgs> PlayerQuit;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionPlayerService"/> class.
        /// </summary>
        public OrionPlayerService() {
            _players = new IPlayer[Terraria.Main.player.Length];

            Hooks.Player.PostGreet = PostGreetHandler;
            Hooks.Player.PreGreet = PreGreetHandler;
            Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        /// <inheritdoc />
        public IEnumerator<IPlayer> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void PostGreetHandler(int playerId) {
            Debug.Assert(playerId >= 0 && playerId < _players.Length,
                         $"{nameof(playerId)} should be a valid index.");

            var player = this[playerId];
            var joinedArgs = new PlayerJoinedEventArgs(player);
            PlayerJoined?.Invoke(this, joinedArgs);
        }

        private HookResult PreGreetHandler(ref int playerId) {
            Debug.Assert(playerId >= 0 && playerId < _players.Length,
                         $"{nameof(playerId)} should be a valid index.");

            var player = this[playerId];
            var joiningArgs = new PlayerJoiningEventArgs(player);
            PlayerJoining?.Invoke(this, joiningArgs);
            return joiningArgs.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreResetHandler(Terraria.RemoteClient remoteClient) {
            if (remoteClient.Socket == null) {
                return HookResult.Continue;
            }

            Debug.Assert(remoteClient.Id >= 0 && remoteClient.Id < _players.Length,
                         $"{nameof(remoteClient.Id)} should be a valid index.");

            var player = this[remoteClient.Id];
            var quitArgs = new PlayerQuitEventArgs(player);
            PlayerQuit?.Invoke(this, quitArgs);
            return HookResult.Continue;
        }
    }
}
