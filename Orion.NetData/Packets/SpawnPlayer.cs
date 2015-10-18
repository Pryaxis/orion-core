using System.IO;
using Orion.Framework.Events;
using Terraria;

namespace Orion.NetData.Packets
{
	public class SpawnPlayer : TerrariaPacketBase
	{
		public byte PlayerID { get; set; }
		public short SpawnX { get; set; }
		public short SpawnY { get; set; }

		internal SpawnPlayer(BinaryReader reader) : base(reader)
		{
			PlayerID = reader.ReadByte();
			SpawnX = reader.ReadInt16();
			SpawnY = reader.ReadInt16();
		}

		internal override void SetNewData(ref NetSendDataEventArgs e)
		{
			e.Number = PlayerID;
			Main.player[PlayerID].SpawnX = SpawnX;
			Main.player[PlayerID].SpawnY = SpawnY;
		}
	}
}
