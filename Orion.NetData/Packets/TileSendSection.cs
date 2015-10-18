using Orion.Framework.Events;
using OTA.Memory;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Tile_Entities;

namespace Orion.NetData.Packets
{
	/// <summary>
	/// Not fully implemented. Packet 10
	/// </summary>
	public class TileSendSection : TerrariaPacketBase
	{
		public int XStart { get; set; }
		public int YStart { get; set; }
		public short Width { get; set; }
		public short Height { get; set; }
		public List<MemTile> Tiles { get; set; } = new List<MemTile>();
		public short ChestCount { get; set; } = 0;
		public List<Chest> Chests { get; set; } = new List<Chest>();
		public short SignCount { get; set; } = 0;
		public List<Sign> Signs { get; set; } = new List<Sign>();
		public short TileEntityCount { get; set; } = 0;
		public List<TileEntity> TileEntities { get; set; } = new List<TileEntity>();

		internal TileSendSection(NetSendDataEventArgs e) : base(PacketTypes.TileSendSection)
		{
			XStart = e.Number;
			YStart = (int)e.Number2;
			Width = (short)e.Number3;
			Height = (short)e.Number4;

			for (int i = YStart; i < YStart + Height; i++)
			{
				for (int j = XStart; j < XStart + Width; j++)
				{
					MemTile t = Main.tile[i, j];

					if (t != null && t.active())
					{
						if ((t.type == 21 && t.frameX % 36 == 0 && t.frameY % 36 == 0)
							|| (t.type == 88 && t.frameX % 54 == 0 && t.frameY % 36 == 0))
						{
							int chestId = Chest.FindChest(j, i);
							if (chestId != -1)
							{
								Chests.Add(Main.chest[ChestCount]);
								ChestCount++;
							}
						}
						else if ((t.type == 85 && t.frameX % 36 == 0 && t.frameY % 36 == 0)
							|| (t.type == 55 && t.frameX % 36 == 0 && t.frameY % 36 == 0))
						{
							int signId = Sign.ReadSign(j, i, true);
							if (signId != -1)
							{
								Signs.Add(Main.sign[SignCount]);
								SignCount++;
							}
						}
						else if (t.type == 378 && t.frameX % 36 == 0 && t.frameY == 0)
						{
							int entityId = TETrainingDummy.Find(j, i);
							if (entityId != -1)
							{
								TileEntities.Add(TileEntity.ByID[entityId]);
							}
						}
						else if (t.type == 395 && t.frameX % 36 == 0 && t.frameY == 0)
						{
							int entityId = TEItemFrame.Find(j, i);
							if (entityId != -1)
							{
								TileEntities.Add(TileEntity.ByID[entityId]);
							}
						}
						else
						{
							Tiles.Add(t);
						}
                    }
				}
			}
		}
	}
}