using System.IO;

using Terraria;

namespace Orion.Net.Packets
{
	/// <summary>
	/// Inventory Slot packet
	/// </summary>
	public class InventorySlot : TerrariaPacket
	{
		public byte Player { get; set; }
		public byte SlotID { get; set; }
		public short Stack { get; set; }
		public byte Prefix { get; set; }
		public short NetID { get; set; }

		/// <summary>
		/// Used when the packet is received
		/// </summary>
		/// <param name="reader"></param>
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
		/// Used when the packet is sent
		/// </summary>
		/// <param name="id"></param>
		/// <param name="player"></param>
		/// <param name="slot"></param>
		/// <param name="utils"></param>
		internal InventorySlot(byte id, int player, float slot, NetUtils utils)
			: base(id)
		{
			Player = (byte)player;
			SlotID = (byte)slot;
			Player ply = Main.player[Player];
			Item item;
			if (SlotID == utils.TotalSlots - 1) //179
			{
				item = ply.trashItem;
			}
			else if (SlotID >= utils.TotalSlots - 1 - utils.SafeSlots) //139
			{
				item = ply.bank2.item[SlotID - (utils.TotalSlots - 1 - utils.SafeSlots)];
			}
			else if (SlotID >= utils.TotalSlots - 1 - utils.SafeSlots - utils.PiggyBankSlots) //99
			{
				item = ply.bank.item[SlotID - (utils.TotalSlots - 1 - utils.SafeSlots - utils.PiggyBankSlots)];
			}
			else if (SlotID >= utils.TotalSlots - 1 - utils.SafeSlots - utils.PiggyBankSlots
				- utils.MiscDyeSlots) //94
			{
				item = ply.miscDyes[SlotID - (utils.TotalSlots - 1 - utils.SafeSlots - utils.PiggyBankSlots
					- utils.MiscDyeSlots)];
			}
			else if (SlotID >= utils.TotalSlots - 1 - utils.SafeSlots - utils.PiggyBankSlots
				- utils.MiscDyeSlots - utils.MiscEquipSlots) //89
			{
				item = ply.miscEquips[SlotID - (utils.TotalSlots - 1 - utils.SafeSlots - utils.PiggyBankSlots
					   - utils.MiscDyeSlots - utils.MiscEquipSlots)];
			}
			else if (SlotID >= utils.TotalSlots - 1 - utils.SafeSlots - utils.PiggyBankSlots
				- utils.MiscDyeSlots - utils.MiscEquipSlots - utils.DyeSlots) //79
			{
				item = ply.dye[SlotID - (utils.TotalSlots - 1 - utils.SafeSlots - utils.PiggyBankSlots
					   - utils.MiscDyeSlots - utils.MiscEquipSlots - utils.DyeSlots)];
			}
			else if (SlotID >= utils.TotalSlots - 1 - utils.SafeSlots - utils.PiggyBankSlots
				- utils.MiscDyeSlots - utils.MiscEquipSlots - utils.DyeSlots - utils.ArmorSlots) //59
			{
				item = ply.armor[SlotID - (utils.TotalSlots - 1 - utils.SafeSlots - utils.PiggyBankSlots
					   - utils.MiscDyeSlots - utils.MiscEquipSlots - utils.DyeSlots - utils.ArmorSlots)];
			}
			else //0
			{
				item = ply.inventory[SlotID];
			}
		}

		/// <summary>
		/// Used when the packet is sent
		/// </summary>
		/// <param name="id"></param>
		/// <param name="player"></param>
		/// <param name="slot"></param>
		/// <param name="utils"></param>
		internal InventorySlot(PacketTypes id, int player, float slot, NetUtils utils)
			: this((byte)id, player, slot, utils)
		{

		}
	}
}
