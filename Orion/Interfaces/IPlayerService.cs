using System;
using System.Collections.Generic;
using Orion.Events.Player;
using Orion.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a mechanism for dealing with <see cref="IPlayer"/>s.
	/// </summary>
	public interface IPlayerService : IService
	{
		/// <summary>
		/// Occurs when a <see cref="IPlayer"/> has joined the server.
		/// </summary>
		event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

		/// <summary>
		/// Occurs when a <see cref="IPlayer"/> is joining the server.
		/// </summary>
		event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

		/// <summary>
		/// Occurs when a <see cref="IPlayer"/> has quit the server.
		/// </summary>
		event EventHandler<PlayerQuitEventArgs> PlayerQuit;

		/// <summary>
		/// Finds all <see cref="IPlayer"/>s in the world matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of <see cref="IPlayer"/>s matching the predicate.</returns>
		IEnumerable<IPlayer> Find(Predicate<IPlayer> predicate);

		/// <summary>
		/// Gets all <see cref="IPlayer"/>s in the world.
		/// </summary>
		/// <returns>An enumerable collection of <see cref="IPlayer"/>s.</returns>
		IEnumerable<IPlayer> GetAll();
	}
}
