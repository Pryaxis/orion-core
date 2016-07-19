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
		/// Gets the <see cref="IPlayer"/> that quit the server.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerQuitEventArgs"/> class.
		/// </summary>
		/// <param name="player">The <see cref="IPlayer"/> that quit the server.</param>
		public PlayerQuitEventArgs(IPlayer player)
		{
			Player = player;
		}
	}
}
