using System;
using System.Collections.Generic;
using Orion.Framework;
using Orion.Players.Events;

namespace Orion.Players
{
	/// <summary>
	/// Provides a mechanism for managing players.
	/// </summary>
	public interface IPlayerService : ISharedService
	{
		/// <summary>
		/// Occurs after a player has joined the game.
		/// </summary>
		event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

		/// <summary>
		/// Occurs when a player is joining the game.
		/// </summary>
		event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

		/// <summary>
		/// Occurs after a player has quit the game.
		/// </summary>
		event EventHandler<PlayerQuitEventArgs> PlayerQuit;

		/// <summary>
		/// Returns all players in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of players.</returns>
		IEnumerable<IPlayer> FindPlayers(Predicate<IPlayer> predicate = null);
	}
}
