using System;

namespace Orion.Players.Events
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerJoined"/> event.
	/// </summary>
	public class PlayerJoinedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the player that joined the game.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerJoinedEventArgs"/> class.
		/// </summary>
		/// <param name="player">The player that joined the game.</param>
		/// <exception cref="ArgumentNullException"><paramref name="player"/> is null.</exception>
		public PlayerJoinedEventArgs(IPlayer player)
		{
			if (player == null)
			{
				throw new ArgumentNullException(nameof(player));
			}

			Player = player;
		}
	}
}
