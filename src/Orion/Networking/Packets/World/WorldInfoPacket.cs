using System;
using System.IO;
using Orion.Networking.Packets.Connections;
using Orion.World;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to set world information. This is sent in response to a
    /// <see cref="FinishConnectingPacket"/> and is also sent periodically.
    /// </summary>
    public sealed class WorldInfoPacket : Packet {
        private string _worldName = "";

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is daytime.
        /// </summary>
        public bool IsDaytime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is a blood moon.
        /// </summary>
        public bool IsBloodMoon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is an eclipse.
        /// </summary>
        public bool IsEclipse { get; set; }

        /// <summary>
        /// Gets or sets the moon phase.
        /// </summary>
        public byte MoonPhase { get; set; }

        /// <summary>
        /// Gets or sets the world width.
        /// </summary>
        public short WorldWidth { get; set; }

        /// <summary>
        /// Gets or sets the world height.
        /// </summary>
        public short WorldHeight { get; set; }

        /// <summary>
        /// Gets or sets the spawn X coordinate.
        /// </summary>
        public short SpawnX { get; set; }

        /// <summary>
        /// Gets or sets the spawn Y coordinate.
        /// </summary>
        public short SpawnY { get; set; }

        /// <summary>
        /// Gets or sets the surface Y coordinate.
        /// </summary>
        public short SurfaceY { get; set; }

        /// <summary>
        /// Gets or sets the rock layer Y coordinate.
        /// </summary>
        public short RockLayerY { get; set; }

        /// <summary>
        /// Gets or sets the world ID.
        /// </summary>
        public int WorldId { get; set; }

        /// <summary>
        /// Gets or sets the world name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string WorldName {
            get => _worldName;
            set => _worldName = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the world GUID.
        /// </summary>
        public Guid WorldGuid { get; set; }

        /// <summary>
        /// Gets or sets the world generator version.
        /// </summary>
        public ulong WorldGeneratorVersion { get; set; }

        /// <summary>
        /// Gets or sets the moon type.
        /// </summary>
        public byte MoonType { get; set; }

        /// <summary>
        /// Gets or sets the tree background style.
        /// </summary>
        public byte TreeBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the corruption background style.
        /// </summary>
        public byte CorruptionBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the jungle background style.
        /// </summary>
        public byte JungleBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the snow background style.
        /// </summary>
        public byte SnowBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the hallow background style.
        /// </summary>
        public byte HallowBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the crimson background style.
        /// </summary>
        public byte CrimsonBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the desert background style.
        /// </summary>
        public byte DesertBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the ocean background style.
        /// </summary>
        public byte OceanBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the ice cave background style.
        /// </summary>
        public byte IceCaveBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the underground jungle background style.
        /// </summary>
        public byte UndergroundJungleBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the hell background style.
        /// </summary>
        public byte HellBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the wind speed.
        /// </summary>
        public float WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the number of clouds.
        /// </summary>
        public byte NumberOfClouds { get; set; }

        /// <summary>
        /// Get the tree style boundaries.
        /// </summary>
        public int[] TreeStyleBoundaries { get; private set; } = new int[3];

        /// <summary>
        /// Gets the tree styles.
        /// </summary>
        public byte[] TreeStyles { get; private set; } = new byte[4];

        /// <summary>
        /// Gets the cave background style boundaries.
        /// </summary>
        public int[] CaveBackgroundStyleBoundaries { get; private set; } = new int[3];

        /// <summary>
        /// Gets the cave background styles.
        /// </summary>
        public byte[] CaveBackgroundStyles { get; private set; } = new byte[4];

        /// <summary>
        /// Gets or sets the rain.
        /// </summary>
        public float Rain { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a shadow orb has been smashed.
        /// </summary>
		public bool HasSmashedShadowOrb { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Eye of Cthulhu has been defeated.
        /// </summary>
        public bool HasDefeatedEyeOfCthulhu { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the "evil" boss (Eater of Worlds, or Brain of Cthulhu) has been
        /// defeated.
        /// </summary>
        public bool HasDefeatedEvilBoss { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Skeletron has been defeated.
        /// </summary>
        public bool HasDefeatedSkeletron { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is in hard mode.
        /// </summary>
        public bool IsHardMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a clown has been defeated.
        /// </summary>
        public bool HasDefeatedClown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world has server-side characters.
        /// </summary>
        public bool IsServerSideCharacter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Plantera has been defeated.
        /// </summary>
        public bool HasDefeatedPlantera { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Destroyer has been defeated.
        /// </summary>
        public bool HasDefeatedDestroyer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Twins have been defeated.
        /// </summary>
        public bool HasDefeatedTwins { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Skeletron Prime has been defeated.
        /// </summary>
        public bool HasDefeatedSkeletronPrime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a mechanical boss has been defeated.
        /// </summary>
        public bool HasDefeatedMechanicalBoss { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the cloud background is active.
        /// </summary>
        public bool IsCloudBackgroundActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is crimson.
        /// </summary>
        public bool IsCrimson { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is a pumpkin moon.
        /// </summary>
        public bool IsPumpkinMoon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is a frost moon.
        /// </summary>
        public bool IsFrostMoon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is in expert mode.
        /// </summary>
        public bool IsExpertMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether time is being fast-forwarded.
        /// </summary>
        public bool IsFastForwardingTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is a slime rain.
        /// </summary>
        public bool IsSlimeRain { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether King Slime has been defeated.
        /// </summary>
        public bool HasDefeatedKingSlime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Queen Bee has been defeated.
        /// </summary>
        public bool HasDefeatedQueenBee { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Duke Fishron has been defeated.
        /// </summary>
        public bool HasDefeatedDukeFishron { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Martians have been defeated.
        /// </summary>
        public bool HasDefeatedMartians { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Ancient Cultist has been defeated.
        /// </summary>
        public bool HasDefeatedAncientCultist { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Moon Lord has been defeated.
        /// </summary>
        public bool HasDefeatedMoonLord { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a Pumpking has been defeated.
        /// </summary>
        public bool HasDefeatedPumpking { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a Mourning Wood has been defeated.
        /// </summary>
        public bool HasDefeatedMourningWood { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an Ice Queen has been defeated.
        /// </summary>
        public bool HasDefeatedIceQueen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a Santank has been defeated.
        /// </summary>
        public bool HasDefeatedSantank { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an Everscream has been defeated.
        /// </summary>
        public bool HasDefeatedEverscream { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a Golem has been defeated.
        /// </summary>
        public bool HasDefeatedGolem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is a birthday party.
        /// </summary>
        public bool IsBirthdayParty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Pirates have been defeated.
        /// </summary>
        public bool HasDefeatedPirates { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Frost Legion has been defeated.
        /// </summary>
        public bool HasDefeatedFrostLegion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Goblins have been defeated.
        /// </summary>
        public bool HasDefeatedGoblins { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is a sandstorm.
        /// </summary>
        public bool IsSandstorm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Old One's Army is invading.
        /// </summary>
        public bool IsOldOnesArmy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether tier 1 of the Old One's Army has been defeated.
        /// </summary>
        public bool HasDefeatedOldOnesArmyTier1 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether tier 2 of the Old One's Army has been defeated.
        /// </summary>
        public bool HasDefeatedOldOnesArmyTier2 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether tier 3 of the Old One's Army has been defeated.
        /// </summary>
        public bool HasDefeatedOldOnesArmyTier3 { get; set; }

        /// <summary>
        /// Gets or sets the invasion type.
        /// </summary>
        public InvasionType InvasionType { get; set; }

        /// <summary>
        /// Gets or sets the lobby ID.
        /// </summary>
        public ulong LobbyId { get; set; }

        /// <summary>
        /// Gets or sets the sandstorm intensity.
        /// </summary>
        public float SandstormIntensity { get; set; }

        private protected override PacketType Type => PacketType.WorldInfo;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            Time = reader.ReadInt32();

            Terraria.BitsByte timeFlags = reader.ReadByte();
            IsDaytime = timeFlags[0];
            IsBloodMoon = timeFlags[1];
            IsEclipse = timeFlags[2];

            MoonPhase = reader.ReadByte();
            WorldWidth = reader.ReadInt16();
            WorldHeight = reader.ReadInt16();
            SpawnX = reader.ReadInt16();
            SpawnY = reader.ReadInt16();
            SurfaceY = reader.ReadInt16();
            RockLayerY = reader.ReadInt16();
            WorldId = reader.ReadInt32();
            _worldName = reader.ReadString();
            WorldGuid = new Guid(reader.ReadBytes(16));
            WorldGeneratorVersion = reader.ReadUInt64();
            MoonType = reader.ReadByte();
            TreeBackgroundStyle = reader.ReadByte();
            CorruptionBackgroundStyle = reader.ReadByte();
            JungleBackgroundStyle = reader.ReadByte();
            SnowBackgroundStyle = reader.ReadByte();
            HallowBackgroundStyle = reader.ReadByte();
            CrimsonBackgroundStyle = reader.ReadByte();
            DesertBackgroundStyle = reader.ReadByte();
            OceanBackgroundStyle = reader.ReadByte();
            IceCaveBackgroundStyle = reader.ReadByte();
            UndergroundJungleBackgroundStyle = reader.ReadByte();
            HellBackgroundStyle = reader.ReadByte();
            WindSpeed = reader.ReadSingle();
            NumberOfClouds = reader.ReadByte();
            TreeStyleBoundaries = new[] {reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32()};
            TreeStyles = reader.ReadBytes(4);
            CaveBackgroundStyleBoundaries = new[] {reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32()};
            CaveBackgroundStyles = reader.ReadBytes(4);
            Rain = reader.ReadSingle();

            Terraria.BitsByte worldFlags = reader.ReadByte();
            Terraria.BitsByte worldFlags2 = reader.ReadByte();
            Terraria.BitsByte worldFlags3 = reader.ReadByte();
            Terraria.BitsByte worldFlags4 = reader.ReadByte();
            Terraria.BitsByte worldFlags5 = reader.ReadByte();
            HasSmashedShadowOrb = worldFlags[0];
            HasDefeatedEyeOfCthulhu = worldFlags[1];
            HasDefeatedEvilBoss = worldFlags[2];
            HasDefeatedSkeletron = worldFlags[3];
            IsHardMode = worldFlags[4];
            HasDefeatedClown = worldFlags[5];
            IsServerSideCharacter = worldFlags[6];
            HasDefeatedPlantera = worldFlags[7];
            HasDefeatedDestroyer = worldFlags2[0];
            HasDefeatedTwins = worldFlags2[1];
            HasDefeatedSkeletronPrime = worldFlags2[2];
            HasDefeatedMechanicalBoss = worldFlags2[3];
            IsCloudBackgroundActive = worldFlags2[4];
            IsCrimson = worldFlags2[5];
            IsPumpkinMoon = worldFlags2[6];
            IsFrostMoon = worldFlags2[7];
            IsExpertMode = worldFlags3[0];
            IsFastForwardingTime = worldFlags3[1];
            IsSlimeRain = worldFlags3[2];
            HasDefeatedKingSlime = worldFlags3[3];
            HasDefeatedQueenBee = worldFlags3[4];
            HasDefeatedDukeFishron = worldFlags3[5];
            HasDefeatedMartians = worldFlags3[6];
            HasDefeatedAncientCultist = worldFlags3[7];
            HasDefeatedMoonLord = worldFlags4[0];
            HasDefeatedPumpking = worldFlags4[1];
            HasDefeatedMourningWood = worldFlags4[2];
            HasDefeatedIceQueen = worldFlags4[3];
            HasDefeatedSantank = worldFlags4[4];
            HasDefeatedEverscream = worldFlags4[5];
            HasDefeatedGolem = worldFlags4[6];
            IsBirthdayParty = worldFlags4[7];
            HasDefeatedPirates = worldFlags5[0];
            HasDefeatedFrostLegion = worldFlags5[1];
            HasDefeatedGoblins = worldFlags5[2];
            IsSandstorm = worldFlags5[3];
            IsOldOnesArmy = worldFlags5[4];
            HasDefeatedOldOnesArmyTier1 = worldFlags5[5];
            HasDefeatedOldOnesArmyTier2 = worldFlags5[6];
            HasDefeatedOldOnesArmyTier3 = worldFlags5[7];

            InvasionType = (InvasionType)reader.ReadSByte();
            LobbyId = reader.ReadUInt64();
            SandstormIntensity = reader.ReadSingle();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Time);

            Terraria.BitsByte timeFlags = 0;
            timeFlags[0] = IsDaytime;
            timeFlags[1] = IsBloodMoon;
            timeFlags[2] = IsEclipse;
            writer.Write(timeFlags);

            writer.Write(MoonPhase);
            writer.Write(WorldWidth);
            writer.Write(WorldHeight);
            writer.Write(SpawnX);
            writer.Write(SpawnY);
            writer.Write(SurfaceY);
            writer.Write(RockLayerY);
            writer.Write(WorldId);
            writer.Write(WorldName);
            writer.Write(WorldGuid.ToByteArray());
            writer.Write(WorldGeneratorVersion);
            writer.Write(MoonType);
            writer.Write(TreeBackgroundStyle);
            writer.Write(CorruptionBackgroundStyle);
            writer.Write(JungleBackgroundStyle);
            writer.Write(SnowBackgroundStyle);
            writer.Write(HallowBackgroundStyle);
            writer.Write(CrimsonBackgroundStyle);
            writer.Write(DesertBackgroundStyle);
            writer.Write(OceanBackgroundStyle);
            writer.Write(IceCaveBackgroundStyle);
            writer.Write(UndergroundJungleBackgroundStyle);
            writer.Write(HellBackgroundStyle);
            writer.Write(WindSpeed);
            writer.Write(NumberOfClouds);

            foreach (var boundary in TreeStyleBoundaries) {
                writer.Write(boundary);
            }

            writer.Write(TreeStyles);

            foreach (var boundary in CaveBackgroundStyleBoundaries) {
                writer.Write(boundary);
            }

            writer.Write(CaveBackgroundStyles);
            writer.Write(Rain);

            Terraria.BitsByte worldFlags = 0;
            Terraria.BitsByte worldFlags2 = 0;
            Terraria.BitsByte worldFlags3 = 0;
            Terraria.BitsByte worldFlags4 = 0;
            Terraria.BitsByte worldFlags5 = 0;
            worldFlags[0] = HasSmashedShadowOrb;
            worldFlags[1] = HasDefeatedEyeOfCthulhu;
            worldFlags[2] = HasDefeatedEvilBoss;
            worldFlags[3] = HasDefeatedSkeletron;
            worldFlags[4] = IsHardMode;
            worldFlags[5] = HasDefeatedClown;
            worldFlags[6] = IsServerSideCharacter;
            worldFlags[7] = HasDefeatedPlantera;
            worldFlags2[0] = HasDefeatedDestroyer;
            worldFlags2[1] = HasDefeatedTwins;
            worldFlags2[2] = HasDefeatedSkeletronPrime;
            worldFlags2[3] = HasDefeatedMechanicalBoss;
            worldFlags2[4] = IsCloudBackgroundActive;
            worldFlags2[5] = IsCrimson;
            worldFlags2[6] = IsPumpkinMoon;
            worldFlags2[7] = IsFrostMoon;
            worldFlags3[0] = IsExpertMode;
            worldFlags3[1] = IsFastForwardingTime;
            worldFlags3[2] = IsSlimeRain;
            worldFlags3[3] = HasDefeatedKingSlime;
            worldFlags3[4] = HasDefeatedQueenBee;
            worldFlags3[5] = HasDefeatedDukeFishron;
            worldFlags3[6] = HasDefeatedMartians;
            worldFlags3[7] = HasDefeatedAncientCultist;
            worldFlags4[0] = HasDefeatedMoonLord;
            worldFlags4[1] = HasDefeatedPumpking;
            worldFlags4[2] = HasDefeatedMourningWood;
            worldFlags4[3] = HasDefeatedIceQueen;
            worldFlags4[4] = HasDefeatedSantank;
            worldFlags4[5] = HasDefeatedEverscream;
            worldFlags4[6] = HasDefeatedGolem;
            worldFlags4[7] = IsBirthdayParty;
            worldFlags5[0] = HasDefeatedPirates;
            worldFlags5[1] = HasDefeatedFrostLegion;
            worldFlags5[2] = HasDefeatedGoblins;
            worldFlags5[3] = IsSandstorm;
            worldFlags5[4] = IsOldOnesArmy;
            worldFlags5[5] = HasDefeatedOldOnesArmyTier1;
            worldFlags5[6] = HasDefeatedOldOnesArmyTier2;
            worldFlags5[7] = HasDefeatedOldOnesArmyTier3;
            writer.Write(worldFlags);
            writer.Write(worldFlags2);
            writer.Write(worldFlags3);
            writer.Write(worldFlags4);
            writer.Write(worldFlags5);

            writer.Write((sbyte)InvasionType);
            writer.Write(LobbyId);
            writer.Write(SandstormIntensity);
        }
    }
}
