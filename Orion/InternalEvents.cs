using Orion.Net.Packets;

namespace Orion
{
	internal class InternalEventHandler
	{
		private Orion _core;

		internal InternalEventHandler(Orion core)
		{
			_core = core;
		}

		internal void PlayerSlotReceived(InventorySlot packet)
		{
			if (Terraria.Main.ServerSideCharacter)
			{
				//Eg
				//(_core.OnlinePlayers[packet.Player] as SSCharacter).EditItemSlot(packet.SlotID, new NetItem(...));
			}
		}
	}
}
