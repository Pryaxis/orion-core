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
		/// Gets the relevant <see cref="IPlayer"/>.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerQuitEventArgs"/> class with the specified
		/// <see cref="IPlayer"/>.
		/// </summary>
		/// <param name="player">The <see cref="IPlayer"/>.</param>
		public PlayerQuitEventArgs(IPlayer player)
		{
			Player = player;
		}
	}
}
