using Orion.Net;

namespace Orion.SSC
{
	public class SSCharacter : OrionPlayer
	{
		public NetInventory Inventory { get; private set; }

		public NetItem this[int index]
		{
			get { return Inventory[index]; }
		}

		public SSCharacter(Terraria.Player player, Orion core) : base(player)
		{
			Inventory = new NetInventory(player);
		}

		public override void EditItemSlot(int index, Terraria.Item item)
		{
			Inventory[index] = item;
		}

		public override void EditItemSlot(int index, NetItem item)
		{
			Inventory[index] = item;
		}

		public override void SaveInventory()
		{
			//TODO: Save inventory to DB
		}
	}
}
