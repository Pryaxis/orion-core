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
		/// Gets the <see cref="IPlayer"/>.
		/// </summary>
		public IPlayer Player { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerJoiningEventArgs"/> class with the specified
		/// <see cref="IPlayer"/>.
		/// </summary>
		/// <param name="player">The <see cref="IPlayer"/>.</param>
		public PlayerJoiningEventArgs(IPlayer player)
		{
			Player = player;
		}
	}
}
