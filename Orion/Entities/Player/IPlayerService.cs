using System;
using System.Collections.Generic;
using Orion.Events.Player;
using Orion.Framework;

namespace Orion.Entities.Player
{
	/// <summary>
	/// Provides a mechanism for managing <see cref="IPlayer"/> instances.
	/// </summary>
	public interface IPlayerService : IService
	{
		/// <summary>
		/// Occurs after a <see cref="IPlayer"/> instance joined the server.
		/// </summary>
		event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

		/// <summary>
		/// Occurs when a <see cref="IPlayer"/> instance is joining the server.
		/// </summary>
		event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

		/// <summary>
		/// Occurs after a <see cref="IPlayer"/> instance quit the server.
		/// </summary>
		event EventHandler<PlayerQuitEventArgs> PlayerQuit;

		/// <summary>
		/// Finds all <see cref="IPlayer"/> instances in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of <see cref="IPlayer"/> instances.</returns>
		IEnumerable<IPlayer> Find(Predicate<IPlayer> predicate = null);
	}
}
