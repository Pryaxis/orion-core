using Terraria;
using TerrariaApi.Server;

namespace Orion.Net.Packets
{
	/// <summary>
	/// WorldInfo packet
	/// </summary>
	public class WorldInfo : TerrariaPacket
	{
		public int Time { get; set; }
		public bool DayTime { get; set; }
		public bool BloodMoon { get; set; }
		public bool Eclipse { get; set; }
		public byte MoonPhase { get; set; }
		public short MaxTilesX { get; set; }
		public short MaxTilesY { get; set; }
		public short SpawnTileX { get; set; }
		public short SpawnTileY { get; set; }
		public short WorldSurface { get; set; }
		public short RockLayer { get; set; }
		public int WorldID { get; set; }
		public string WorldName { get; set; }
		public byte MoonType { get; set; }
		public byte WorldGenTreeBg { get; set; }
		public byte WorldGenCorruptBg { get; set; }
		public byte WorldGenJungleBg { get; set; }
		public byte WorldGenSnowBg { get; set; }
		public byte WorldGenHallowBg { get; set; }
		public byte WorldGenCrimsonBg { get; set; }
		public byte WorldGenDesertBg { get; set; }
		public byte WorldGenOceanBg { get; set; }
		public byte IceBackStyle { get; set; }
		public byte JungleBackStyle { get; set; }
		public byte HellBackStyle { get; set; }
		public float WindSpeed { get; set; }
		public byte NumberOfClouds { get; set; }
		public int[] TreeX { get; set; }
		public byte[] TreeStyle { get; set; }
		public int[] CaveBackX { get; set; }
		public byte[] CaveBackStyle { get; set; }
		public float Rain { get; set; }
		public bool ShadowOrbSmashed { get; set; }
		public bool WorldIsHardMode { get; set; }
		public bool DownedEyeOfCthulhu { get; set; }
		public bool DownedEaterOfWorlds { get; set; }
		public bool DownedSkeletron { get; set; }
		public bool DownedClowns { get; set; }
		public bool ServerSideCharacter { get; set; }
		public bool DownedPlantera { get; set; }
		public bool DownedTwins { get; set; }
		public bool DownedDestroyer { get; set; }
		public bool DownedSkeletronPrime { get; set; }
		public bool DownedAnyMechBosses { get; set; }
		public bool DownedQueenBee { get; set; }
		public bool DownedSlimeKing { get; set; }
		public bool DownedFishron { get; set; }
		public bool DownedMartians { get; set; }
		public bool DownedAncientCultist { get; set; }
		public bool DownedMoonLord { get; set; }
		public bool DownedHalloweenKing { get; set; }
		public bool DownedHalloweenTree { get; set; }
		public bool DownedChristmasQueen { get; set; }
		public bool DownedChristmasSantank { get; set; }
		public bool DownedChristmasTree { get; set; }
		public bool DownedGolem { get; set; }
		public float CloudBgActive { get; set; }
		public bool WorldIsCrimson { get; set; }
		public bool CurrentlyPumpkinMoon { get; set; }
		public bool CurrentlySnowMoon { get; set; }
		public bool WorldIsExpertMode { get; set; }
		public bool TimeIsFastForwarded { get; set; }
		public bool CurrentlySlimeRain { get; set; }
		public sbyte CurrentInvasionType { get; set; }
		public ulong SteamLobbyID { get; set; }

		public WorldInfo(byte id)
			: base(id)
		{
			Time = (int)Main.time;
			DayTime = Main.dayTime;

			BloodMoon = Main.bloodMoon;
			Eclipse = Main.eclipse;

			MoonPhase = (byte)Main.moonPhase;
			MoonType = (byte)Main.moonType;

			MaxTilesX = (short)Main.maxTilesX;
			MaxTilesY = (short)Main.maxTilesY;

			SpawnTileX = (short)Main.spawnTileX;
			SpawnTileY = (short)Main.spawnTileY;

			WorldSurface = (short)Main.worldSurface;
			RockLayer = (short)Main.rockLayer;

			WorldID = Main.worldID;
			WorldName = Main.worldName;

			WorldGenTreeBg = (byte)WorldGen.treeBG;
			WorldGenCorruptBg = (byte)WorldGen.corruptBG;
			WorldGenJungleBg = (byte)WorldGen.jungleBG;
			WorldGenSnowBg = (byte)WorldGen.snowBG;
			WorldGenHallowBg = (byte)WorldGen.hallowBG;
			WorldGenCrimsonBg = (byte)WorldGen.crimsonBG;
			WorldGenDesertBg = (byte)WorldGen.desertBG;
			WorldGenOceanBg = (byte)WorldGen.oceanBG;

			IceBackStyle = (byte)Main.iceBackStyle;
			JungleBackStyle = (byte)Main.jungleBackStyle;
			HellBackStyle = (byte)Main.hellBackStyle;

			WindSpeed = Main.windSpeedSet;
			NumberOfClouds = (byte)Main.numClouds;

			for (int i = 0; i < 3; i++)
			{
				TreeX[i] = Main.treeX[i];
			}
			for (int i = 0; i < 4; i++)
			{
				TreeStyle[i] = (byte)Main.treeStyle[i];
			}
			for (int i = 0; i < 3; i++)
			{
				CaveBackX[i] = Main.caveBackX[i];
			}
			for (int i = 0; i < 4; i++)
			{
				CaveBackStyle[i] = (byte)Main.caveBackStyle[i];
			}

			Rain = Main.maxRaining;
			ShadowOrbSmashed = WorldGen.shadowOrbSmashed;

			DownedEyeOfCthulhu = NPC.downedBoss1;
			DownedEaterOfWorlds = NPC.downedBoss2;
			DownedSkeletron = NPC.downedBoss3;
			DownedPlantera = NPC.downedPlantBoss;
			DownedGolem = NPC.downedGolemBoss;
			DownedDestroyer = NPC.downedMechBoss1;
			DownedTwins = NPC.downedMechBoss2;
			DownedSkeletronPrime = NPC.downedMechBoss3;
			DownedAnyMechBosses = NPC.downedMechBossAny;
			DownedSlimeKing = NPC.downedSlimeKing;
			DownedQueenBee = NPC.downedQueenBee;
			DownedFishron = NPC.downedFishron;
			DownedMartians = NPC.downedMartians;
			DownedAncientCultist = NPC.downedAncientCultist;
			DownedMoonLord = NPC.downedMoonlord;
			DownedHalloweenKing = NPC.downedHalloweenKing;
			DownedHalloweenTree = NPC.downedHalloweenTree;
			DownedChristmasQueen = NPC.downedChristmasIceQueen;
			DownedChristmasSantank = NPC.downedChristmasSantank;
			DownedChristmasTree = NPC.downedChristmasTree;
			DownedClowns = NPC.downedClown;

			ServerSideCharacter = Main.ServerSideCharacter;
			CloudBgActive = Main.cloudBGActive;

			WorldIsCrimson = WorldGen.crimson;
			WorldIsExpertMode = Main.expertMode;
			WorldIsHardMode = Main.hardMode;

			CurrentlyPumpkinMoon = Main.pumpkinMoon;
			CurrentlySnowMoon = Main.snowMoon;
			CurrentInvasionType = (sbyte)Main.invasionType;

			SteamLobbyID = Main.LobbyId;
		}

		public WorldInfo(PacketTypes id)
			: this((byte)id)
		{

		}

		internal override void SetNewData(ref SendDataEventArgs e)
		{
			Main.time = Time;
			Main.dayTime = DayTime;

			Main.bloodMoon = BloodMoon;
			Main.eclipse = Eclipse;

			Main.moonPhase = MoonPhase;
			Main.moonType = MoonType;

			Main.maxTilesX = MaxTilesX;
			Main.maxTilesY = MaxTilesY;

			Main.spawnTileX = SpawnTileX;
			Main.spawnTileY = SpawnTileY;

			Main.worldSurface = WorldSurface;
			Main.rockLayer = RockLayer;

			Main.worldID = WorldID;
			Main.worldName = WorldName;

			WorldGen.treeBG = WorldGenTreeBg;
			WorldGen.corruptBG = WorldGenCorruptBg;
			WorldGen.jungleBG = WorldGenJungleBg;
			WorldGen.snowBG = WorldGenSnowBg;
			WorldGen.hallowBG = WorldGenHallowBg;
			WorldGen.crimsonBG = WorldGenCrimsonBg;
			WorldGen.desertBG = WorldGenDesertBg;
			WorldGen.oceanBG = WorldGenOceanBg;

			Main.iceBackStyle = IceBackStyle;
			Main.jungleBackStyle = JungleBackStyle;
			Main.hellBackStyle = HellBackStyle;

			Main.windSpeedSet = WindSpeed;
			Main.numClouds = NumberOfClouds;

			for (int i = 0; i < 3; i++)
			{
				Main.treeX[i] = TreeX[i];
			}
			for (int i = 0; i < 4; i++)
			{
				Main.treeStyle[i] = TreeStyle[i];
			}
			for (int i = 0; i < 3; i++)
			{
				Main.caveBackX[i] = CaveBackX[i];
			}
			for (int i = 0; i < 4; i++)
			{
				Main.caveBackStyle[i] = CaveBackStyle[i];
			}

			Main.maxRaining = Rain;
			WorldGen.shadowOrbSmashed = ShadowOrbSmashed;

			NPC.downedBoss1 = DownedEyeOfCthulhu;
			NPC.downedBoss2 = DownedEaterOfWorlds;
			NPC.downedBoss3 = DownedSkeletron;
			NPC.downedPlantBoss = DownedPlantera;
			NPC.downedGolemBoss = DownedGolem;
			NPC.downedMechBoss1 = DownedDestroyer;
			NPC.downedMechBoss2 = DownedTwins;
			NPC.downedMechBoss3 = DownedSkeletronPrime;
			NPC.downedMechBossAny = DownedAnyMechBosses;
			NPC.downedSlimeKing = DownedSlimeKing;
			NPC.downedQueenBee = DownedQueenBee;
			NPC.downedFishron = DownedFishron;
			NPC.downedMartians = DownedMartians;
			NPC.downedAncientCultist = DownedAncientCultist;
			NPC.downedMoonlord = DownedMoonLord;
			NPC.downedHalloweenKing = DownedHalloweenKing;
			NPC.downedHalloweenTree = DownedHalloweenTree;
			NPC.downedChristmasIceQueen = DownedChristmasQueen;
			NPC.downedChristmasSantank = DownedChristmasSantank;
			NPC.downedClown = DownedClowns;

			Main.ServerSideCharacter = ServerSideCharacter;
			Main.cloudBGActive = CloudBgActive;

			WorldGen.crimson = WorldIsCrimson;
			Main.expertMode = WorldIsExpertMode;
			Main.hardMode = WorldIsHardMode;

			Main.pumpkinMoon = CurrentlyPumpkinMoon;
			Main.snowMoon = CurrentlySnowMoon;
			Main.invasionType = CurrentInvasionType;
			Main.LobbyId = SteamLobbyID;
		}
	}
}
