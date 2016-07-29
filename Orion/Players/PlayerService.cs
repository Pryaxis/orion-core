using System;
using System.Collections.Generic;
using System.Linq;
using Orion.Framework;
using Orion.Players.Events;
using OTAPI.Core;

namespace Orion.Players
{
	/// <summary>
	/// Manages <see cref="IPlayer"/> instances.
	/// </summary>
	[Service("Player Service", Author = "Nyx Studios")]
	public class PlayerService : ServiceBase, IPlayerService
	{
		private readonly IPlayer[] _players;
		private bool _disposed;

		/// <inheritdoc/>
		public event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

		/// <inheritdoc/>
		public event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

		/// <inheritdoc/>
		public event EventHandler<PlayerQuitEventArgs> PlayerQuit;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public PlayerService(Orion orion) : base(orion)
		{
			_players = new IPlayer[Terraria.Main.player.Length];
			Hooks.Net.RemoteClient.PreReset = InvokePlayerQuit;
			Hooks.Player.PreGreet = InvokePlayerJoin;
			// TODO: change this to use net hooks, so we can have separate greeting hooks
		}

		/// <inheritdoc/>
		/// <remarks>
		/// The <see cref="IPlayer"/> instances are cached in an array. Calling this method multiple times will result
		/// in the same <see cref="IPlayer"/> instances as long as Terraria's player array remains unchanged.
		/// </remarks>
		public IEnumerable<IPlayer> Find(Predicate<IPlayer> predicate = null)
		{
			var players = new List<IPlayer>();
			for (int i = 0; i < _players.Length; i++)
			{
				if (_players[i]?.WrappedPlayer != Terraria.Main.player[i])
				{
					_players[i] = new Player(Terraria.Main.player[i]);
				}
				players.Add(_players[i]);
			}
			return players.Where(p => p.WrappedPlayer.active && (predicate?.Invoke(p) ?? true));
		}

		/// <inheritdoc/>
		protected override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Hooks.Net.RemoteClient.PreReset = null;
					Hooks.Player.PreGreet = null;
				}
				_disposed = true;
			}
			base.Dispose(disposing);
		}

		private HookResult InvokePlayerJoin(ref int playerId)
		{
			if (_players[playerId]?.WrappedPlayer != Terraria.Main.player[playerId])
			{
				_players[playerId] = new Player(Terraria.Main.player[playerId]);
			}
			IPlayer player = _players[playerId];
			var preArgs = new PlayerJoiningEventArgs(player);
			PlayerJoining?.Invoke(this, preArgs);
			if (preArgs.Handled)
			{
				return HookResult.Cancel;
			}

			var postArgs = new PlayerJoinedEventArgs(player);
			PlayerJoined?.Invoke(this, postArgs);
			return HookResult.Continue;
		}

		private HookResult InvokePlayerQuit(Terraria.RemoteClient remoteClient)
		{
			if (remoteClient.Socket == null)
			{
				return HookResult.Continue;
			}

			if (_players[remoteClient.Id]?.WrappedPlayer != Terraria.Main.player[remoteClient.Id])
			{
				_players[remoteClient.Id] = new Player(Terraria.Main.player[remoteClient.Id]);
			}
			IPlayer player = _players[remoteClient.Id];
			var args = new PlayerQuitEventArgs(player);
			PlayerQuit?.Invoke(this, args);
			return HookResult.Continue;
		}
	}
}
