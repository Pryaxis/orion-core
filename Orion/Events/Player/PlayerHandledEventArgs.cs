using System.ComponentModel;

namespace Orion.Events.Player
{
	public class PlayerHandledEventArgs : HandledEventArgs
	{
		public Terraria.Player Player { get; }

		public PlayerHandledEventArgs(Terraria.Player player)
		{
			Player = player;
		}
	}
}
