using System.IO;
using Terraria;

namespace Orion.Net.Packets
{
	/// <summary>
	/// Inventory Slot [5] packet. Sent by both ends (sync).
	/// </summary>
	public class InventorySlot : TerrariaPacket
	{
		public byte Player { get; set; }
		public byte SlotID { get; set; }
		public short Stack { get; set; }
		public byte Prefix { get; set; }
		public short NetID { get; set; }

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
		}
	}
}
