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

        public override string Author => "Pryaxis";
        public override string Name => "Orion Player Service";

        public int Count => Terraria.Main.maxPlayers;

        public IPlayer this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                if (_players[index]?.WrappedPlayer != Terraria.Main.player[index]) {
                    _players[index] = new OrionPlayer(Terraria.Main.player[index]);
                }

                var player = _players[index];
                Debug.Assert(player != null, $"{nameof(player)} should not be null.");
                Debug.Assert(player.WrappedPlayer != null, 
                             $"{nameof(player.WrappedPlayer)} should not be null.");
                return player;
            }
        }

        public event EventHandler<PlayerJoiningEventArgs> PlayerJoining;
        public event EventHandler<PlayerJoinedEventArgs> PlayerJoined;
        public event EventHandler<PlayerUpdatingEventArgs> PlayerUpdating;
        public event EventHandler<PlayerUpdatedEventArgs> PlayerUpdated;
        public event EventHandler<PlayerQuitEventArgs> PlayerQuit;

        public OrionPlayerService() {
            _players = new IPlayer[Terraria.Main.maxPlayers];
            
            Hooks.Player.PreGreet = PreGreetHandler;
            Hooks.Player.PostGreet = PostGreetHandler;
            Hooks.Player.PreUpdate = PreUpdateHandler;
            Hooks.Player.PostUpdate = PostUpdateHandler;
            Hooks.Net.RemoteClient.PreReset = PreResetHandler;
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
            var args = new PlayerJoiningEventArgs(player);
            PlayerJoining?.Invoke(this, args);
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostGreetHandler(int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new PlayerJoinedEventArgs(player);
            PlayerJoined?.Invoke(this, args);
        }

        private HookResult PreUpdateHandler(Terraria.Player terrariaPlayer, ref int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new PlayerUpdatingEventArgs(player);
            PlayerUpdating?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostUpdateHandler(Terraria.Player terrariaPlayer, int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Count, $"{nameof(playerIndex)} should be a valid index.");

            var player = this[playerIndex];
            var args = new PlayerUpdatedEventArgs(player);
            PlayerUpdated?.Invoke(this, args);
        }

        private HookResult PreResetHandler(Terraria.RemoteClient remoteClient) {
            if (remoteClient.Socket == null) {
                return HookResult.Continue;
            }

            Debug.Assert(remoteClient.Id >= 0 && remoteClient.Id < Count, $"{nameof(remoteClient.Id)} should be a valid index.");

            var player = this[remoteClient.Id];
            var args = new PlayerQuitEventArgs(player);
            PlayerQuit?.Invoke(this, args);
            return HookResult.Continue;
        }
    }
}
