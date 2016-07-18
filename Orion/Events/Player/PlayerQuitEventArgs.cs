using System;
using Orion.Interfaces;
using Orion.Services;

namespace Orion.Events.Player
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerQuit"/> event.
	/// </summary>
	public class PlayerQuitEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the relevant player.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerQuitEventArgs"/> class with the specified player.
		/// </summary>
		/// <param name="player">The player.</param>
		public PlayerQuitEventArgs(IPlayer player)
		{
			Player = player;
		}
	}
}
