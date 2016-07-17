using Orion.Interfaces;

namespace Orion.Events.Player
{
	/// <summary>
	/// Provides data for the <see cref="IPlayerService.PlayerJoining"/> event.
	/// </summary>
	public class PlayerJoiningEventArgs : HandledEntityEventArgs<Terraria.Player>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerJoiningEventArgs"/> class with the specified player.
		/// </summary>
		/// <param name="player">The player.</param>
		public PlayerJoiningEventArgs(Terraria.Player player) : base(player)
		{
		}
	}
}
