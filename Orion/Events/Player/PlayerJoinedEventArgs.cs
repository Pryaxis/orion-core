using Orion.Interfaces;

namespace Orion.Events.Player
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerJoined"/> event.
	/// </summary>
	public class PlayerJoinedEventArgs : EntityEventArgs<Terraria.Player>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerJoinedEventArgs"/> class with the specified player.
		/// </summary>
		/// <param name="player">The player.</param>
		public PlayerJoinedEventArgs(Terraria.Player player) : base(player)
		{
		}
	}
}
