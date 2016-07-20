using System;
using System.Collections.Generic;
using Orion.Events.Player;
using Orion.Framework;

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
		/// Occurs when a player has joined the server.
		/// </summary>
		event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

		/// <summary>
		/// Occurs when a player is joining the server.
		/// </summary>
		event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

		/// <summary>
		/// Occurs when a player has quit the server.
		/// </summary>
		event EventHandler<PlayerQuitEventArgs> PlayerQuit;

		/// <summary>
		/// Finds all players matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of players matching the predicate.</returns>
		IEnumerable<IPlayer> Find(Predicate<IPlayer> predicate);

		/// <summary>
		/// Gets all players.
		/// </summary>
		/// <returns>An enumerable collection of players.</returns>
		IEnumerable<IPlayer> GetAll();
	}
}
