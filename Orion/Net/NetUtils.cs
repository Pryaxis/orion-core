using Terraria;

namespace Orion.Net
{
	public class NetUtils
	{
		/// <summary>
		/// Orion instance
		/// </summary>
		private Orion _core;

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

		/// <summary>
		/// Creates a new instance of the NetUtils class using the given Orion instance
		/// </summary>
		/// <param name="core"></param>
		public NetUtils(Orion core)
		{
			_core = core;
		}
	}
}