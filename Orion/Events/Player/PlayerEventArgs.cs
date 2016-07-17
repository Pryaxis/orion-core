namespace Orion.Events.Player
{
	public class PlayerEventArgs
	{
		public Terraria.Player Player { get; }

		public PlayerEventArgs(Terraria.Player player)
		{
			Player = player;
		}
	}
}
