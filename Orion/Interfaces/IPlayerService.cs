using Orion.Events.Player;
using System;

namespace Orion.Interfaces
{
	/// <summary>
	/// Service definition: IPlayerService
	///
	/// Provides a mechanism for dealing with players.
	/// </summary>
	public interface IPlayerService : IEntityService<Terraria.Player>
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
	}
}
