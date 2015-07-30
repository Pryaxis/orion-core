using System.IO;
using Terraria;
using TerrariaApi.Server;

namespace Orion.Net.Packets
{
	/// <summary>
	/// Player Info [4] packet. Sent by both ends (sync).
	/// </summary>
	public class PlayerInfo : TerrariaPacket
	{
		/// <summary>
		/// Player ID
		/// </summary>
		public byte Player { get; set; }
		/// <summary>
		/// Player SkinVariant
		/// </summary>
		public byte SkinVariant { get; set; }
		/// <summary>
		/// Player Hair ID
		/// </summary>
		public byte Hair { get; set; }
		/// <summary>
		/// Player Name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Player Hair Dye ID
		/// </summary>
		public byte HairDye { get; set; }
		/// <summary>
		/// Player's hidden visuals
		/// </summary>
		public BitsByte HideVisuals;
		/// <summary>
		/// Player's other hidden visuals (slots 9 & 10)
		/// </summary>
		public BitsByte HideOtherVisuals;
		/// <summary>
		/// Player's hidden miscellanious visuals
		/// </summary>
		public BitsByte HideMisc;
		/// <summary>
		/// Player's Hair color
		/// </summary>
		public Color HairColor { get; set; }
		/// <summary>
		/// Player's Skin color
		/// </summary>
		public Color SkinColor { get; set; }
		/// <summary>
		/// Player's Eye color
		/// </summary>
		public Color EyeColor { get; set; }
		/// <summary>
		/// Player's Shirt color
		/// </summary>
		public Color ShirtColor { get; set; }
		/// <summary>
		/// Player's Undershirt color
		/// </summary>
		public Color UndershirtColor { get; set; }
		/// <summary>
		/// Player's Pants color
		/// </summary>
		public Color PantsColor { get; set; }
		/// <summary>
		/// Player's Shoe color
		/// </summary>
		public Color ShoeColor { get; set; }
		/// <summary>
		/// Player's Difficulty. Default is 0 for softcore. 1 is medium-core. 2 is hardcore
		/// </summary>
		public byte Difficulty { get; set; }
		/// <summary>
		/// Whether or not the player has used a demon heart
		/// </summary>
		public bool HasExtraSlot { get; set; }

		/// <summary>
		/// Whether or not all data has been initialized and is ready to be sent.
		/// This must be true before calling SetNewData
		/// </summary>
		private bool _isReadyForSend;

		/// <summary>
		/// Creates a new Player Info packet by reading data from <paramref name="reader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> object with the data to be read.</param>
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
			HideVisuals = reader.ReadByte();
			HideOtherVisuals = reader.ReadByte();
			HideMisc = reader.ReadByte();
			HairColor = reader.ReadRGB();
			SkinColor = reader.ReadRGB();
			EyeColor = reader.ReadRGB();
			ShirtColor = reader.ReadRGB();
			UndershirtColor = reader.ReadRGB();
			PantsColor = reader.ReadRGB();
			ShoeColor = reader.ReadRGB();
			BitsByte extraInfo = reader.ReadByte();
			Difficulty = 0;
			if (extraInfo[0])
			{
				Difficulty = 1;
			}
			if (extraInfo[2])
			{
				Difficulty = 2;
			}

			HasExtraSlot = extraInfo[2];
		}

		/// <summary>
		/// Creates a new PlayerInfo packet and populates it with info from the player
		/// whose's index is <paramref name="playerID"/>.
		/// </summary>
		/// <param name="playerID">The player index.</param>
		/// <param name="playerName">The player name.</param>
		internal PlayerInfo(int playerID, string playerName)
			: base(PacketTypes.PlayerInfo)
		{
			if (playerID < 0 || playerID > Main.maxNetPlayers)
			{
				return;
			}

			Player = (byte)playerID;
			Player player = Main.player[Player];
			SkinVariant = (byte)player.skinVariant;
			Hair = (byte)player.hair;
			Name = playerName;
			HairDye = player.hairDye;
			HideVisuals = 0;
			for (int i = 0; i < 8; i++)
			{
				HideVisuals[i] = player.hideVisual[i];
			}
			HideOtherVisuals = 0;
			for (int i = 0; i < 2; i++)
			{
				HideOtherVisuals[i] = player.hideVisual[i + 8];
			}
			HideMisc = player.hideMisc;
			HairColor = player.hairColor;
			SkinColor = player.skinColor;
			EyeColor = player.eyeColor;
			ShirtColor = player.shirtColor;
			UndershirtColor = player.underShirtColor;
			PantsColor = player.pantsColor;
			ShoeColor = player.shoeColor;
			Difficulty = player.difficulty;
			HasExtraSlot = player.extraAccessory;

			_isReadyForSend = true;
		}

		/// <summary>
		/// Sets new Player Info data and updates <paramref name="e"/>.
		/// </summary>
		/// <param name="e">The <see cref="SendDataEventArgs"/> to set.</param>
		internal override void SetNewData(ref SendDataEventArgs e)
		{
			if (!_isReadyForSend)
			{
				return;
			}

			e.number = Player;
			e.text = Name;

			Player player = Main.player[Player];
			player.skinVariant = SkinVariant;
			player.hair = Hair;
			player.hairDye = HairDye;
			for (int i = 0; i < 8; i++)
			{
				player.hideVisual[i] = HideVisuals[i];
			}
			for (int i = 0; i < 2; i++)
			{
				player.hideVisual[i + 8] = HideOtherVisuals[i];
			}
			player.hideMisc = HideMisc;
			player.hairColor = HairColor;
			player.skinColor = SkinColor;
			player.eyeColor = EyeColor;
			player.shirtColor = ShirtColor;
			player.underShirtColor = UndershirtColor;
			player.pantsColor = PantsColor;
			player.shoeColor = ShoeColor;
			player.difficulty = Difficulty;
			player.extraAccessory = HasExtraSlot;
		}
	}
}
