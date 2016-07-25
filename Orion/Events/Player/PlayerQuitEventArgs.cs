using System;
using Orion.Interfaces;

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
		/// <exception cref="ArgumentNullException"><paramref name="player"/> was null.</exception>
		public PlayerQuitEventArgs(IPlayer player)
		{
			if (player == null)
			{
				throw new ArgumentNullException(nameof(player));
			}

			Player = player;
		}
	}
}
