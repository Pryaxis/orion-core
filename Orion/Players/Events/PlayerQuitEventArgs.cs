using System;

namespace Orion.Players.Events
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerQuit"/> event.
	/// </summary>
	public class PlayerQuitEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the player that quit the game.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerQuitEventArgs"/> class.
		/// </summary>
		/// <param name="player">The player that quit the game.</param>
		/// <exception cref="ArgumentNullException"><paramref name="player"/> is null.</exception>
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
