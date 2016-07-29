using System;
using System.ComponentModel;

namespace Orion.Players.Events
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerJoining"/> event.
	/// </summary>
	public class PlayerJoiningEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the <see cref="IPlayer"/> instance that is joining the server.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerJoiningEventArgs"/> class.
		/// </summary>
		/// <param name="player">The <see cref="IPlayer"/> instance that is joining the server.</param>
		/// <exception cref="ArgumentNullException"><paramref name="player"/> was null.</exception>
		public PlayerJoiningEventArgs(IPlayer player)
		{
			if (player == null)
			{
				throw new ArgumentNullException(nameof(player));
			}

			Player = player;
		}
	}
}
