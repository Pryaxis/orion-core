using Orion.Interfaces;

namespace Orion.Events.Player
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerQuit"/> event.
	/// </summary>
	public class PlayerQuitEventArgs : EntityEventArgs<Terraria.Player>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerQuitEventArgs"/> class with the specified player.
		/// </summary>
		/// <param name="player">The player.</param>
		public PlayerQuitEventArgs(Terraria.Player player) : base(player)
		{
		}
	}
}
