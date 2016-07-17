namespace Orion.Events.Player
{
	public class PlayerJoinedEventArgs : PlayerEventArgs
	{
		public PlayerJoinedEventArgs(Terraria.Player player) : base(player)
		{
		}
	}
}
