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
	/// Implements the <see cref="IPlayerService"/> functionality.
	/// </summary>
	[Service("Player Service", Author = "Nyx Studios")]
	public class PlayerService : ServiceBase, IPlayerService
	{
		private bool _disposed;
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
			Hooks.Net.RemoteClient.PostReset = InvokePlayerQuit;
			Hooks.Player.PreGreet = InvokePlayerJoin;
		}

		/// <summary>
		/// Finds all <see cref="IPlayer"/>s in the world matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of <see cref="IPlayer"/>s matching the predicate.</returns>
		public IEnumerable<IPlayer> Find(Predicate<IPlayer> predicate)
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
			return players.Where(p => p.WrappedPlayer.active && predicate(p));
		}

		/// <summary>
		/// Gets all <see cref="IPlayer"/>s in the world.
		/// </summary>
		/// <returns>An enumerable collection of <see cref="IPlayer"/>s.</returns>
		public IEnumerable<IPlayer> GetAll()
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
			return players.Where(p => p.WrappedPlayer.active);
		}

		/// <summary>
		/// Disposes the service and its unmanaged resources, if any, optionally disposing its managed resources, if
		/// any.
		/// </summary>
		/// <param name="disposing">
		/// true to dispose managed and unmanaged resources, false to only dispose unmanaged resources.
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

		private HookResult InvokePlayerJoin(ref int playerId)
		{
			if (_players[playerId]?.WrappedPlayer != Terraria.Main.player[playerId])
			{
				_players[playerId] = new Player(Terraria.Main.player[playerId]);
			}
			var player = _players[playerId];
			var preArgs = new PlayerJoiningEventArgs(player);
			PlayerJoining?.Invoke(this, preArgs);
			if (preArgs.Handled)
			{
				return HookResult.Cancel;
			}
			else
			{
				var postArgs = new PlayerJoinedEventArgs(player);
				PlayerJoined?.Invoke(this, postArgs);
				return HookResult.Continue;
			}
		}

		private void InvokePlayerQuit(Terraria.RemoteClient remoteClient)
		{
			if (_players[remoteClient.Id]?.WrappedPlayer != Terraria.Main.player[remoteClient.Id])
			{
				_players[remoteClient.Id] = new Player(Terraria.Main.player[remoteClient.Id]);
			}
			var player = _players[remoteClient.Id];
			var args = new PlayerQuitEventArgs(player);
			PlayerQuit?.Invoke(this, args);
		}
	}
}
