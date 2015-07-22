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
		public readonly int InventorySlots = 59;
		/// <summary>
		/// 20 - Number of item slots available for armor, accessories, and vanity
		/// </summary>
		public readonly int ArmorSlots = 20;
		/// <summary>
		/// 10 - Number of dye slots available for armor and accessories
		/// </summary>
		public readonly int DyeSlots = 10;
		/// <summary>
		/// 5 - Number of item slots available for miscellanious equipment
		/// </summary>
		public readonly int MiscEquipSlots = 5;
		/// <summary>
		/// 5 - Number of dye slots available for miscellanious equipment
		/// </summary>
		public readonly int MiscDyeSlots = 5;
		/// <summary>
		/// 40 - Number of slots in a piggy bank
		/// </summary>
		public readonly int PiggyBankSlots = 40;
		/// <summary>
		/// 40 - Number of slots in a Safe
		/// </summary>
		public readonly int SafeSlots = 40;
		/// <summary>
		/// 180 - Total inventory size
		/// </summary>
		public int TotalSlots { get; private set; }

		/// <summary>
		/// Creates a new instance of the NetUtils class using the given Orion instance
		/// </summary>
		/// <param name="core"></param>
		public NetUtils(Orion core)
		{
			_core = core;
			TotalSlots = InventorySlots + ArmorSlots + DyeSlots + MiscEquipSlots + MiscDyeSlots
				+ PiggyBankSlots + SafeSlots + 1;
		}

		/// <summary>
		/// Sends a single packet to every player
		/// </summary>
		/// <param name="data"></param>
		public void SendPacketToEveryone(PacketData data)
		{
			SendPacketToEveryone(data.packet, data.text, data.num, data.num2, data.num3, data.num4, data.num5);
		}

		/// <summary>
		/// Sends a single packet to every player
		/// </summary>
		/// <param name="packet"></param>
		/// <param name="text"></param>
		/// <param name="num"></param>
		/// <param name="num2"></param>
		/// <param name="num3"></param>
		/// <param name="num4"></param>
		/// <param name="num5"></param>
		public void SendPacketToEveryone(PacketTypes packet, string text = "", int num = 0, float num2 = 0f, float num3 = 0f,
			float num4 = 0f, int num5 = 0)
		{
			NetMessage.SendData((int)packet, -1, -1, text, num, num2, num3, num4, num5);
		}

		/// <summary>
		/// Sends multiple packets to every player
		/// </summary>
		/// <param name="data"></param>
		public void SendPacketsToEveryone(params PacketData[] data)
		{
			foreach (PacketData d in data)
			{
				SendPacketToEveryone(d);
			}
		}

		/// <summary>
		/// Sends a single packet to one player
		/// </summary>
		/// <param name="data"></param>
		public void SendPacketToIndividual(PacketData data)
		{
			SendPacketToIndividual(data.packet, data.remoteClient,
				data.text, data.num, data.num2, data.num3, data.num4, data.num5);
		}

		/// <summary>
		/// Sends a single packet to one player
		/// </summary>
		/// <param name="packet"></param>
		/// <param name="index"></param>
		/// <param name="text"></param>
		/// <param name="num"></param>
		/// <param name="num2"></param>
		/// <param name="num3"></param>
		/// <param name="num4"></param>
		/// <param name="num5"></param>
		public void SendPacketToIndividual(PacketTypes packet, int index, string text = "", int num = 0,
			float num2 = 0f, float num3 = 0f, float num4 = 0f, int num5 = 0)
		{
			NetMessage.SendData((int)packet, index, -1, text, num, num2, num3, num4, num5);
		}

		/// <summary>
		/// Sends multiple packets to one player
		/// </summary>
		/// <param name="data"></param>
		public void SendPacketsToIndividuals(params PacketData[] data)
		{
			foreach (PacketData d in data)
			{
				SendPacketToIndividual(d);
			}
		}

		/// <summary>
		/// Sends a single packet from a player to a player
		/// </summary>
		/// <param name="data"></param>
		public void SendPacketFromIndividual(PacketData data)
		{
			SendPacketFromIndividual(data.packet, data.remoteClient, data.num, data.text, data.num2,
				data.num3, data.num4, data.num5);
		}

		/// <summary>
		/// Sends a single packet from a player to a player
		/// </summary>
		/// <param name="packet"></param>
		/// <param name="to"></param>
		/// <param name="from"></param>
		/// <param name="text"></param>
		/// <param name="num2"></param>
		/// <param name="num3"></param>
		/// <param name="num4"></param>
		/// <param name="num5"></param>
		public void SendPacketFromIndividual(PacketTypes packet, int to, int from, string text = "",
			float num2 = 0f, float num3 = 0f, float num4 = 0f, int num5 = 0)
		{
			NetMessage.SendData((int)packet, to, -1, text, from, num2, num3, num4, num5);
		}

		/// <summary>
		/// Sends multiple packets from a player to a player
		/// </summary>
		/// <param name="data"></param>
		public void SendPacketsFromIndividual(params PacketData[] data)
		{
			foreach (PacketData d in data)
			{
				SendPacketFromIndividual(d);
			}
		}
	}
}