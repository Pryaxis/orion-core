using System;
using System.Collections.Generic;
using Orion.Framework;
using Orion.Players.Events;

namespace Orion.Players
{
	/// <summary>
	/// Provides a mechanism for managing <see cref="IPlayer"/> instances.
	/// </summary>
	public interface IPlayerService : ISharedService
	{
		/// <summary>
		/// Occurs after a <see cref="IPlayer"/> instance has joined the game.
		/// </summary>
		event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

		/// <summary>
		/// Occurs when a <see cref="IPlayer"/> instance is joining the game.
		/// </summary>
		event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

		/// <summary>
		/// Occurs after a <see cref="IPlayer"/> instance has quit the game.
		/// </summary>
		event EventHandler<PlayerQuitEventArgs> PlayerQuit;

		/// <summary>
		/// Returns all <see cref="IPlayer"/> instances in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of <see cref="IPlayer"/> instances.</returns>
		IEnumerable<IPlayer> FindPlayers(Predicate<IPlayer> predicate = null);
	}
}
