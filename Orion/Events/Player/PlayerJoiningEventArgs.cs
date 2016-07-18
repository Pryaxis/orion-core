using System.ComponentModel;
using Orion.Interfaces;
using Orion.Services;

namespace Orion.Events.Player
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerJoining"/> event.
	/// </summary>
	public class PlayerJoiningEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the relevant player.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerJoiningEventArgs"/> class with the specified player.
		/// </summary>
		/// <param name="player">The player.</param>
		public PlayerJoiningEventArgs(IPlayer player)
		{
			Player = player;
		}
	}
}
