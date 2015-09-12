using System;
using System.IO;
using Terraria;

namespace Orion.Net.Packets
{
	/// <summary>
	/// Inventory Slot [5] packet. Sent by both ends (sync).
	/// </summary>
	public class InventorySlot : TerrariaPacket
	{
		/// <summary>
		/// Player ID
		/// </summary>
		public byte Player { get; set; }
		/// <summary>
		/// Item slot index
		/// </summary>
		public byte SlotID { get; set; }
		/// <summary>
		/// Item stack
		/// </summary>
		public short Stack { get; set; }
		/// <summary>
		/// Item prefix
		/// </summary>
		public byte Prefix { get; set; }
		/// <summary>
		/// Item ID
		/// </summary>
		public short NetID { get; set; }
		/// <summary>
		/// True if item is favorited
		/// </summary>
		[Obsolete("Currently not sent by Terraria. This value will always be false.")]
		public bool Favorited { get; set; }

		/// <summary>
		/// Creates a new Inventory Slot packet by reading data from <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> object with the data to be read.</param>
		internal InventorySlot(BinaryReader reader)
			: base(reader)
		{
			Player = reader.ReadByte();
			SlotID = reader.ReadByte();
			Stack = reader.ReadInt16();
			Prefix = reader.ReadByte();
			NetID = reader.ReadInt16();
			Favorited = false;
		}

		/// <summary>
		/// Creates a new Inventory Slot packet.
		/// </summary>
		/// <param name="player">The player index.</param>
		/// <param name="slot">The slot index.</param>
		internal InventorySlot(int player, int slot)
			: base(PacketTypes.PlayerSlot)
		{
			Player = (byte)player;
			SlotID = (byte)slot;
			Player ply = Main.player[Player];

			//TODO: Figure out if we actually need the item
			Item item;
			if (SlotID == NetUtils.TotalSlots - 1) //179
			{
				item = ply.trashItem;
			}
			else if (SlotID >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots) //139
			{
				item = ply.bank2.item[SlotID - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots)];
			}
			else if (SlotID >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots) //99
			{
				item = ply.bank.item[SlotID - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots)];
			}
			else if (SlotID >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
				- NetUtils.MiscDyeSlots) //94
			{
				item = ply.miscDyes[SlotID - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
						- NetUtils.MiscDyeSlots)];
			}
			else if (SlotID >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
				- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots) //89
			{
				item = ply.miscEquips[SlotID - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
						- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots)];
			}
			else if (SlotID >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
				- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots - NetUtils.DyeSlots) //79
			{
				item = ply.dye[SlotID - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
						- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots - NetUtils.DyeSlots)];
			}
			else if (SlotID >= NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
				- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots - NetUtils.DyeSlots - NetUtils.ArmorSlots) //59
			{
				item = ply.armor[SlotID - (NetUtils.TotalSlots - 1 - NetUtils.SafeSlots - NetUtils.PiggyBankSlots
						- NetUtils.MiscDyeSlots - NetUtils.MiscEquipSlots - NetUtils.DyeSlots - NetUtils.ArmorSlots)];
			}
			else //0
			{
				item = ply.inventory[SlotID];
			}

			Prefix = item.prefix;
			Stack = (short)item.stack;
			Favorited = item.favorited;
		}
	}
}
