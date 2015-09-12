using System;

namespace Orion.Net
{
	public class NetInventory
	{
		public NetItem[] Inventory = new NetItem[NetUtils.InventorySlots];
		public NetItem[] Armor = new NetItem[NetUtils.ArmorSlots];
		public NetItem[] Dye = new NetItem[NetUtils.DyeSlots];
		public NetItem[] MiscEquips = new NetItem[NetUtils.MiscEquipSlots];
		public NetItem[] MiscDyes = new NetItem[NetUtils.MiscDyeSlots];
		public NetItem[] PiggyBank = new NetItem[NetUtils.PiggyBankSlots];
		public NetItem[] Safe = new NetItem[NetUtils.SafeSlots];
		public NetItem Trash;

		/// <summary>
		/// Returns the NetItem contained in the inventory at the given index.
		/// </summary>
		/// <param name="index">Must be a valid index in the range 0->(<see cref="NetUtils.TotalSlots"></see>-1)</param>
		/// <returns>A <see cref="NetItem"></see> object representing the item at the given index</returns>
		public NetItem this[int index]
		{
			get
			{
				if (index < 0 || index >= NetUtils.TotalSlots)
				{
					//TODO: Add to Strings.resx
					throw new IndexOutOfRangeException("Invalid index.");
				}

				if (index == NetUtils.TotalSlots - 1)
				{
					return Trash;
				}
				if (index >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots)
				{
					return Safe[index - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots)];
				}
				if (index >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots)
				{
					return PiggyBank[index - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots)];
				}
				if (index >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
				- NetUtils.MiscDyeSlots)
				{
					return MiscDyes[index - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
						- NetUtils.MiscDyeSlots)];
				}
				if (index >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
				- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots)
				{
					return MiscEquips[index - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
						- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots)];
				}
				if (index >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
				- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots - NetUtils.DyeSlots)
				{
					return Dye[index - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
						- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots - NetUtils.DyeSlots)];
				}
				if (index >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
				- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots - NetUtils.DyeSlots - NetUtils.ArmorSlots)
				{
					return Armor[index - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
						- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots - NetUtils.DyeSlots - NetUtils.ArmorSlots)];
				}

				return Inventory[index];
			}
		}

		/// <summary>
		/// Constructs a <see cref="NetInventory"></see> from a <see cref="Terraria.Player"></see> object,
		/// using the player's inventory.
		/// </summary>
		/// <param name="player"><see cref="Terraria.Player"></see> object</param>
		public NetInventory(Terraria.Player player)
		{
			for (int i = 0; i < NetUtils.InventorySlots; i++)
			{
				Inventory[i] = player.inventory[i];
			}
			for (int i = 0; i < NetUtils.ArmorSlots; i++)
			{
				Armor[i] = player.armor[i];
			}
			for (int i = 0; i < NetUtils.DyeSlots; i++)
			{
				Dye[i] = player.dye[i];
			}
			for (int i = 0; i < NetUtils.MiscEquipSlots; i++)
			{
				MiscEquips[i] = player.miscEquips[i];
			}
			for (int i = 0; i < NetUtils.MiscDyeSlots; i++)
			{
				MiscDyes[i] = player.miscDyes[i];
			}
			for (int i = 0; i < NetUtils.PiggyBankSlots; i++)
			{
				PiggyBank[i] = player.bank.item[i];
			}
			for (int i = 0; i < NetUtils.SafeSlots; i++)
			{
				Safe[i] = player.bank2.item[i];
			}
			Trash = player.trashItem;
		}
	}
}
