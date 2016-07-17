namespace Orion.Events.Player
{
	public class PlayerJoiningEventArgs : PlayerHandledEventArgs
	{
		public PlayerJoiningEventArgs(Terraria.Player player) : base(player)
		{
		}
	}
}
