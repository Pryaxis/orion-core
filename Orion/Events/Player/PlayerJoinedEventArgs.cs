using System;
using Orion.Interfaces;

namespace Orion.Events.Player
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerJoined"/> event.
	/// </summary>
	public class PlayerJoinedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the <see cref="IPlayer"/> that joined the server.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerJoinedEventArgs"/> class.
		/// </summary>
		/// <param name="player">The <see cref="IPlayer"/> that joined the server.</param>
		public PlayerJoinedEventArgs(IPlayer player)
		{
			Player = player;
		}
	}
}
