using System.IO;
using Terraria;

namespace Orion.Net.Packets
{
	public class PlayerInfo : TerrariaPacket
	{
		/// <summary>
		/// Player ID this packet
		/// </summary>
		public byte Player { get; set; }
		public byte SkinVariant { get; set; }
		public byte Hair { get; set; }
		public string Name { get; set; }
		public byte HairDye { get; set; }
		public BitsByte HideVisuals { get; set; }
		public BitsByte HideOtherVisuals { get; set; }
		public byte HideMisc { get; set; }
		public Color HairColor { get; set; }
		public Color SkinColor { get; set; }
		public Color EyeColor { get; set; }
		public Color ShirtColor { get; set; }
		public Color UndershirtColor { get; set; }
		public Color PantsColor { get; set; }
		public Color ShoeColor { get; set; }
		public byte Difficulty { get; set; }

		internal PlayerInfo(BinaryReader reader) 
			: base(reader)
		{
			Player = reader.ReadByte();
			SkinVariant = reader.ReadByte();
			Hair = reader.ReadByte();
			if (Hair > 134)
			{
				Hair = 0;
			}
			Name = reader.ReadString();
			HairDye = reader.ReadByte();
		}

		internal PlayerInfo(byte id)
			: base(id)
		{
		}

		internal PlayerInfo(PacketTypes id) 
			: base(id)
		{
		}
	}
}
