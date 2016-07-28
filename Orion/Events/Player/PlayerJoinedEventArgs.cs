using System;
using Orion.Entities.Player;

namespace Orion.Events.Player
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerJoined"/> event.
	/// </summary>
	public class PlayerJoinedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the <see cref="IPlayer"/> instance that joined the server.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerJoinedEventArgs"/> class.
		/// </summary>
		/// <param name="player">The <see cref="IPlayer"/> instance that joined the server.</param>
		/// <exception cref="ArgumentNullException"><paramref name="player"/> was null.</exception>
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
