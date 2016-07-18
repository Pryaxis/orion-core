using System;
using Orion.Interfaces;
using Orion.Services;

namespace Orion.Events.Player
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerJoined"/> event.
	/// </summary>
	public class PlayerJoinedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the relevant player.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerJoinedEventArgs"/> class with the specified player.
		/// </summary>
		/// <param name="player">The player.</param>
		public PlayerJoinedEventArgs(IPlayer player)
		{
			Player = player;
		}
	}
}
