using System;
using System.Collections.Generic;
using Orion.Events.Player;
using Orion.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a mechanism for managing <see cref="IPlayer"/>s.
	/// </summary>
	public interface IPlayerService : IService
	{
		/// <summary>
		/// Occurs after a <see cref="IPlayer"/> joined the server.
		/// </summary>
		event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

		/// <summary>
		/// Occurs when a <see cref="IPlayer"/> is joining the server.
		/// </summary>
		event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

		/// <summary>
		/// Occurs after a <see cref="IPlayer"/> quit the server.
		/// </summary>
		event EventHandler<PlayerQuitEventArgs> PlayerQuit;

		/// <summary>
		/// Finds all <see cref="IPlayer"/>s in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of <see cref="IPlayer"/>s.</returns>
		IEnumerable<IPlayer> Find(Predicate<IPlayer> predicate = null);
	}
}
