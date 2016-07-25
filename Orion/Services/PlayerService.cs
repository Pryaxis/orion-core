using System;
using System.Collections.Generic;
using System.Linq;
using Orion.Core;
using Orion.Events.Player;
using Orion.Framework;
using Orion.Interfaces;
using OTAPI.Core;

namespace Orion.Services
{
	/// <summary>
	/// Manages <see cref="IPlayer"/>s with a backing array, retrieving information from the Terraria player array.
	/// </summary>
	[Service("Player Service", Author = "Nyx Studios")]
	public class PlayerService : ServiceBase, IPlayerService
	{
		/// <summary>
		/// A value indicating whether the service has been disposed. Used to ignore multiple
		/// <see cref="Dispose(bool)"/> calls.
		/// </summary>
		private bool _disposed;

		/// <summary>
		/// The backing array of <see cref="IPlayer"/>s. Lazily updated with items from the Terraria item array.
		/// </summary>
		private readonly IPlayer[] _players;

		/// <summary>
		/// Occurs when a <see cref="IPlayer"/> has joined the server.
		/// </summary>
		public event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

		/// <summary>
		/// Occurs when a <see cref="IPlayer"/> is joining the server.
		/// </summary>
		public event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

		/// <summary>
		/// Occurs when a <see cref="IPlayer"/> has quit the server.
		/// </summary>
		public event EventHandler<PlayerQuitEventArgs> PlayerQuit;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public PlayerService(Orion orion) : base(orion)
		{
			_players = new IPlayer[Terraria.Main.player.Length];
			Hooks.Net.RemoteClient.PreReset = InvokePlayerQuit;
			// TODO: change this to use net hooks, so we can have separate greeting hooks
			Hooks.Player.PreGreet = InvokePlayerJoin;
		}

		/// <summary>
		/// Finds all <see cref="IPlayer"/>s in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with, or null for none.</param>
		/// <returns>An enumerable collection of <see cref="IPlayer"/>s.</returns>
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

		/// <summary>
		/// Disposes the service and its unmanaged resources, optionally disposing its managed resources.
		/// </summary>
		/// <param name="disposing">
		/// true if called from a managed disposal, and *both* unmanaged and managed resources must be freed. false
		/// if called from a finalizer, and *only* unmanaged resources may be freed.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Hooks.Net.RemoteClient.PostReset = null;
					Hooks.Player.PreGreet = null;
				}
				_disposed = true;
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Invokes the <see cref="PlayerJoining"/> and <see cref="PlayerJoined"/> events.
		/// </summary>
		/// <param name="playerId">The ID of the player that is joining.</param>
		/// <returns>A value indicating whether to continue or cancel normal server code.</returns>
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

		/// <summary>
		/// Invokes the <see cref="PlayerQuit"/> event.
		/// </summary>
		/// <param name="remoteClient">The remote client that was reset due to leaving.</param>
		/// <returns>A value indicating to continue normal server code.</returns>
		private HookResult InvokePlayerQuit(Terraria.RemoteClient remoteClient)
		{
			if (remoteClient.Socket != null)
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
