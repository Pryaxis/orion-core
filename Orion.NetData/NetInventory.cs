using System;

namespace Orion.NetData
{
	public class NetInventory
	{
		/// <summary>
		/// 59 - Number of item slots available in the main inventory. Includes held item.
		/// </summary>
		public const int InventorySlots = 59;
		/// <summary>
		/// 20 - Number of item slots available for armor, accessories, and vanity
		/// </summary>
		public const int ArmorSlots = 20;
		/// <summary>
		/// 10 - Number of dye slots available for armor and accessories
		/// </summary>
		public const int DyeSlots = 10;
		/// <summary>
		/// 5 - Number of item slots available for miscellanious equipment
		/// </summary>
		public const int MiscEquipSlots = 5;
		/// <summary>
		/// 5 - Number of dye slots available for miscellanious equipment
		/// </summary>
		public const int MiscDyeSlots = 5;
		/// <summary>
		/// 40 - Number of slots in a piggy bank
		/// </summary>
		public const int PiggyBankSlots = 40;
		/// <summary>
		/// 40 - Number of slots in a Safe
		/// </summary>
		public const int SafeSlots = 40;
		/// <summary>
		/// 180 - Total inventory size
		/// </summary>
		public const int TotalSlots = InventorySlots + ArmorSlots + DyeSlots + MiscEquipSlots + MiscDyeSlots + PiggyBankSlots + SafeSlots + 1;


		public NetItem[] Inventory = new NetItem[InventorySlots];
		public NetItem[] Armor = new NetItem[ArmorSlots];
		public NetItem[] Dye = new NetItem[DyeSlots];
		public NetItem[] MiscEquips = new NetItem[MiscEquipSlots];
		public NetItem[] MiscDyes = new NetItem[MiscDyeSlots];
		public NetItem[] PiggyBank = new NetItem[PiggyBankSlots];
		public NetItem[] Safe = new NetItem[SafeSlots];
		public NetItem Trash;

		/// <summary>
		/// Returns the NetItem contained in the inventory at the given index.
		/// </summary>
		/// <param name="index">Must be a valid index in the range 0->(<see cref="TotalSlots"></see>-1)</param>
		/// <returns>A <see cref="NetItem"></see> object representing the item at the given index</returns>
		public NetItem this[int index]
		{
			get
			{
				if (index < 0 || index >= TotalSlots)
				{
					//TODO: Add to Strings.resx
					throw new IndexOutOfRangeException("Invalid index.");
				}

				if (index == TotalSlots - 1)
				{
					return Trash;
				}
				if (index >= TotalSlots - 1 - SafeSlots)
				{
					return Safe[index - (TotalSlots - 1 - SafeSlots)];
				}
				if (index >= TotalSlots - 1 - SafeSlots - PiggyBankSlots)
				{
					return PiggyBank[index - (TotalSlots - 1 - SafeSlots - PiggyBankSlots)];
				}
				if (index >= TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots)
				{
					return MiscDyes[index - (TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots)];
				}
				if (index >= TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots - MiscEquipSlots)
				{
					return MiscEquips[index - (TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots
						- MiscEquipSlots)];
				}
				if (index >= TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots - MiscEquipSlots - DyeSlots)
				{
					return Dye[index - (TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots
						- MiscEquipSlots - DyeSlots)];
				}
				if (index >= TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots - MiscEquipSlots - DyeSlots
					- ArmorSlots)
				{
					return Armor[index - (TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots 
						- MiscEquipSlots - DyeSlots - ArmorSlots)];
				}

				return Inventory[index];
			}
			internal set
			{
				if (index < 0 || index >= TotalSlots)
				{
					//TODO: Add to Strings.resx
					throw new IndexOutOfRangeException("Invalid index.");
				}

				if (index == TotalSlots - 1)
				{
					Trash = value;
				}
				else if (index >= TotalSlots - 1 - SafeSlots)
				{
					Safe[index - (TotalSlots - 1 - SafeSlots)] = value;
				}
				else if (index >= TotalSlots - 1 - SafeSlots - PiggyBankSlots)
				{
					PiggyBank[index - (TotalSlots - 1 - SafeSlots - PiggyBankSlots)] = value;
				}
				else if (index >= TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots)
				{
					MiscDyes[index - (TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots)] = value;
				}
				else if (index >= TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots - MiscEquipSlots)
				{
					MiscEquips[index - (TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots
						- MiscEquipSlots)] = value;
				}
				else if (index >= TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots - MiscEquipSlots 
					- DyeSlots)
				{
					Dye[index - (TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots - MiscEquipSlots 
						- DyeSlots)] = value;
				}
				else if (index >= TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots - MiscEquipSlots
					- DyeSlots - ArmorSlots)
				{
					Armor[index - (TotalSlots - 1 - SafeSlots - PiggyBankSlots - MiscDyeSlots - MiscEquipSlots 
						- DyeSlots - ArmorSlots)] = value;
				}
				else
				{
					Inventory[index] = value;
				}
			}
		}

		/// <summary>
		/// Constructs a <see cref="NetInventory"></see> from a <see cref="Terraria.Player"></see> object,
		/// using the player's inventory.
		/// </summary>
		/// <param name="player"><see cref="Terraria.Player"></see> object</param>
		public NetInventory(Terraria.Player player)
		{
			for (int i = 0; i < InventorySlots; i++)
			{
				Inventory[i] = player.inventory[i];
			}
			for (int i = 0; i < ArmorSlots; i++)
			{
				Armor[i] = player.armor[i];
			}
			for (int i = 0; i < DyeSlots; i++)
			{
				Dye[i] = player.dye[i];
			}
			for (int i = 0; i < MiscEquipSlots; i++)
			{
				MiscEquips[i] = player.miscEquips[i];
			}
			for (int i = 0; i < MiscDyeSlots; i++)
			{
				MiscDyes[i] = player.miscDyes[i];
			}
			for (int i = 0; i < PiggyBankSlots; i++)
			{
				PiggyBank[i] = player.bank.item[i];
			}
			for (int i = 0; i < SafeSlots; i++)
			{
				Safe[i] = player.bank2.item[i];
			}
			Trash = player.trashItem;
		}
	}
}
