namespace Orion.Events.Player
{
	public class PlayerQuitEventArgs : PlayerEventArgs
	{
		public PlayerQuitEventArgs(Terraria.Player player) : base(player)
		{
		}
	}
}
