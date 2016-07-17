using Orion.Events.Player;
using Orion.Framework;
using System;
using System.Collections.Generic;

namespace Orion.Interfaces
{
	/// <summary>
	/// Service definition: IPlayerService
	///
	/// Provides a mechanism for dealing with players.
	/// </summary>
	public interface IPlayerService : IService
	{
		/// <summary>
		/// Occurs when a player is joining the server.
		/// </summary>
		event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

		/// <summary>
		/// Occurs when a player has joined the server.
		/// </summary>
		event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

		/// <summary>
		/// Occurs when a player has quit the server.
		/// </summary>
		event EventHandler<PlayerQuitEventArgs> PlayerQuit;

		/// <summary>
		/// Gets the player count.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Finds all players that match the specified predicate.
		/// </summary>
		/// <returns>
		/// An enumerable collection of players that match the specified predicate.
		/// </returns>
		IEnumerable<Terraria.Player> FindPlayers(Predicate<Terraria.Player> predicate);
	}
}
