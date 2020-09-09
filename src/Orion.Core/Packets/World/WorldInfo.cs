using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Orion.Core.Packets.DataStructures;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent from the server to clients to propagate world changes.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public sealed class WorldInfo : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference
        [FieldOffset(32)] private byte _bytes2; // Used to obtain an interior reference
        [FieldOffset(4)] private Flags8 _solarFlags;
        [FieldOffset(24)] private string? _worldName;
        [FieldOffset(33)] private byte[] _uniqueIdBytes = new byte[16];
        [FieldOffset(94)] private Flags8 _worldFlags;
        [FieldOffset(95)] private Flags8 _worldFlags2;
        [FieldOffset(96)] private Flags8 _worldFlags3;
        [FieldOffset(97)] private Flags8 _worldFlags4;
        [FieldOffset(98)] private Flags8 _worldFlags5;
        [FieldOffset(99)] private Flags8 _worldFlags6;
        [FieldOffset(100)] private Flags8 _worldFlags7;

        /// <summary>
        /// Gets or sets the time of day.
        /// </summary>
        [field: FieldOffset(0)] public int Time { get; set; }

        /// <summary>
        /// Gets or sets the moon phase.
        /// </summary>
        [field: FieldOffset(5)] public MoonPhase MoonPhase { get; set; }

        /// <summary>
        /// Gets or sets the world width in tiles.
        /// </summary>
        [field: FieldOffset(6)] public short MaxTilesX { get; set; }

        /// <summary>
        /// Gets or sets the world height in tiles.
        /// </summary>
        [field: FieldOffset(8)] public short MaxTilesY { get; set; }

        /// <summary>
        /// Gets or sets the main spawn point's X coordinate.
        /// </summary>
        [field: FieldOffset(10)] public short SpawnTileX { get; set; }

        /// <summary>
        /// Gets or sets the main spawn point's Y coordinate.
        /// </summary>
        [field: FieldOffset(12)] public short SpawnTileY { get; set; }

        /// <summary>
        /// Gets or sets the height at which surface starts.
        /// </summary>
        [field: FieldOffset(14)] public short WorldSurface { get; set; }

        /// <summary>
        /// Gets or sets the value at which the rock layer starts.
        /// </summary>
        [field: FieldOffset(16)] public short RockLayer { get; set; }

        /// <summary>
        /// Gets or sets the world identifier.
        /// </summary>
        [field: FieldOffset(18)] public int WorldId { get; set; }

        /// <summary>
        /// Gets or sets the world name.
        /// </summary>
        public string WorldName
        {
            get => _worldName ??= string.Empty;
            set => _worldName = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the game mode.
        /// </summary>
        [field: FieldOffset(32)] public GameMode GameMode { get; set; }

        /// <summary>
        /// Gets or sets the world generation version.
        /// </summary>
        [field: FieldOffset(33)] public ulong WorldGenerationVersion { get; set; }

        /// <summary>
        /// Gets or sets the moon type. Used to identify the proper texture asset.
        /// </summary>
        /// <remarks>See "Notes" at https://terraria.gamepedia.com/Moon_phase for more details.</remarks>
        [field: FieldOffset(41)] public byte MoonType { get; set; }

        /// <summary>
        /// Gets or sets the first forest biome background style.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(42)] public byte ForestBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the second forest biome background style.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(43)] public byte ForestBackgroundStyle2 { get; set; }

        /// <summary>
        /// Gets or sets the third forest biome background style.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(44)] public byte ForestBackgroundStyle3 { get; set; }

        /// <summary>
        /// Gets or sets the fourth forest biome background style.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(45)] public byte ForestBackgroundStyle4 { get; set; }

        /// <summary>
        /// Gets or sets the corrupt biome's background style.
        /// </summary>
        [field: FieldOffset(46)] public byte CorruptBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the jungle biome's background style.
        /// </summary>
        [field: FieldOffset(47)] public byte JungleBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the snow biome's background style.
        /// </summary>
        [field: FieldOffset(48)] public byte SnowBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the hallow biome's background style.
        /// </summary>
        [field: FieldOffset(49)] public byte HallowBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the crimson biome's background style.
        /// </summary>
        [field: FieldOffset(50)] public byte CrimsonBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the desert biome's background style.
        /// </summary>
        [field: FieldOffset(51)] public byte DesertBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the ocean biome's background style.
        /// </summary>
        [field: FieldOffset(52)] public byte OceanBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the mushroom biome's background style.
        /// </summary>
        [field: FieldOffset(53)] public byte MushroomBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the underworld biome's background style.
        /// </summary>
        [field: FieldOffset(54)] public byte UnderworldBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the underground ice background style.
        /// </summary>
        [field: FieldOffset(55)] public byte UndergroundIceBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the underground jungle background style.
        /// </summary>
        [field: FieldOffset(56)] public byte UndergroundJungleBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the underground hell background style.
        /// </summary>
        [field: FieldOffset(57)] public byte UndergroundHellBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the wind speed.
        /// </summary>
        [field: FieldOffset(58)] public float WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the number of clouds.
        /// </summary>
        [field: FieldOffset(62)] public byte NumberOfClouds { get; set; }

        /// <summary>
        /// Gets or sets the world's globally unique identifier.
        /// </summary>
        public Guid UniqueId
        {
            get => new Guid(_uniqueIdBytes);
            set => _uniqueIdBytes = value.ToByteArray();
        }

        /// <summary>
        /// Gets an array of coordinates that enclose the 4 forest regions.
        /// </summary>
        /// <remarks>
        /// Terraria provides 4 variants of forest backgrounds. Each variant takes up a certain map area, thus creating a forest region.
        /// This property is used to define at which coordinate one region ends and another begins. The game uses this information
        /// to render the appropriate forest background based on the player's position (given they're in the right biome).
        /// E.g, 0 &lt;= x &lt; <see cref="ForestRegionEdges"/>[0] represents Forest1, <see cref="ForestRegionEdges"/>[0] &lt;= x &lt; <see cref="ForestRegionEdges"/>[1] represents Forest2 etc.
        /// </remarks>
        [field: FieldOffset(63)] public byte[] ForestRegionEdges { get; } = new byte[3];

        /// <summary>
        /// Gets an array of styles used for each forest variant.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(66)] public byte[] ForestStyles { get; } = new byte[4];

        /// <summary>
        /// Gets an array of coordinates that encloses cave regions.
        /// </summary>
        /// <remarks>
        /// This property is used to define at which coordinate one region ends and another begins. The game uses this information
        /// to render the appropriate cave background style based on the player's position (given they're in the right biome or not too deep underground).
        /// </remarks>
        [field: FieldOffset(70)] public byte[] CaveRegionEdges { get; } = new byte[3];

        /// <summary>
        /// Gets an array of styles used for caves.
        /// </summary>
        [field: FieldOffset(73)] public byte[] CaveBackgroundStyles { get; } = new byte[3];

        /// <summary>
        /// Gets the current style variation for each world area. Used for tree effects and background rendering.
        /// </summary>
        [field: FieldOffset(77)] public byte[] AreaStyleVariation { get; } = new byte[13];

        /// <summary>
        /// Gets or sets the rain intensity. Values are [0, 1].
        /// </summary>
        [field: FieldOffset(90)] public float RainIntensity { get; set; }

        /// <summary>
        /// Gets or sets the Copper tier.
        /// </summary>
        [field: FieldOffset(101)] public short CopperTier { get; set; }

        /// <summary>
        /// Gets or sets the Iron tier.
        /// </summary>
        [field: FieldOffset(103)] public short IronTier { get; set; }

        /// <summary>
        /// Gets or sets the Silver tier.
        /// </summary>
        [field: FieldOffset(105)] public short SilverTier { get; set; }

        /// <summary>
        /// Gets or sets the Gold tier.
        /// </summary>
        [field: FieldOffset(107)] public short GoldTier { get; set; }

        /// <summary>
        /// Gets or sets the Cobalt tier.
        /// </summary>
        [field: FieldOffset(109)] public short CobaltTier { get; set; }

        /// <summary>
        /// Gets or sets the Mythril tier.
        /// </summary>
        [field: FieldOffset(111)] public short MythrilTier { get; set; }

        /// <summary>
        /// Gets or sets the Adamantite tier.
        /// </summary>
        [field: FieldOffset(113)] public short AdamantiteTier { get; set; }

        /// <summary>
        /// Gets or sets the current invasion type.
        /// </summary>
        [field: FieldOffset(115)] public sbyte InvasionType { get; set; }

        /// <summary>
        /// Gets or sets the lobby ID.
        /// </summary>
        [field: FieldOffset(116)] public ulong LobbyId { get; set; }

        /// <summary>
        /// Gets or sets the sandstorm severity.
        /// </summary>
        [field: FieldOffset(118)] public float SandstormSeverity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is currently day time.
        /// </summary>
        public bool IsDayTime
        {
            get => _solarFlags[0];
            set => _solarFlags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is currently blood moon.
        /// </summary>
        public bool IsBloodMoon
        {
            get => _solarFlags[1];
            set => _solarFlags[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether an eclipse is happening.
        /// </summary>
        public bool IsEclipse
        {
            get => _solarFlags[2];
            set => _solarFlags[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether at least one Shadow Orb was smashed.
        /// </summary>
        public bool WasShadowOrbSmashed
        {
            get => _worldFlags[0];
            set => _worldFlags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Eye of Cthulhu has been defeated.
        /// </summary>
        public bool IsEyeOfCthulhuDefeated
        {
            get => _worldFlags[1];
            set => _worldFlags[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Eater of Worlds / Brain of Cthulhu has been defeated.
        /// </summary>
        public bool IsEowOrBrainOfCthulhuDefeated
        {
            get => _worldFlags[2];
            set => _worldFlags[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether Skeletron has been defeated.
        /// </summary>
        public bool IsSkeletronDefeated
        {
            get => _worldFlags[3];
            set => _worldFlags[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the world has reached hardmode stage.
        /// </summary>
        public bool IsHardmode
        {
            get => _worldFlags[4];
            set => _worldFlags[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Clown has been defeated.
        /// </summary>
        public bool IsClownDefeated
        {
            get => _worldFlags[5];
            set => _worldFlags[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether Server Sided Characters are enabled.
        /// </summary>
        public bool ServerSideCharactersEnabled
        {
            get => _worldFlags[6];
            set => _worldFlags[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether Plantera has been defeated.
        /// </summary>
        public bool IsPlanteraDefeated
        {
            get => _worldFlags[7];
            set => _worldFlags[7] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Destroyer of Worlds has been defeated.
        /// </summary>
        public bool IsDestroyerDefeated
        {
            get => _worldFlags2[0];
            set => _worldFlags2[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Twins were defeated.
        /// </summary>
        public bool IsMechanicalEyeDefeated
        {
            get => _worldFlags2[1];
            set => _worldFlags2[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether Skeletron Prime has been defeated.
        /// </summary>
        public bool IsSkeletronPrimeDefeated
        {
            get => _worldFlags2[2];
            set => _worldFlags2[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether any mechanical boss has been defeated.
        /// </summary>
        public bool DownedAnyMechanicalBoss
        {
            get => _worldFlags2[3];
            set => _worldFlags2[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether clouds are rendered when drawing the background.
        /// </summary>
        public bool IsCloudBackgroundActive
        {
            get => _worldFlags2[4];
            set => _worldFlags2[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether Crimson is the world evil.
        /// </summary>
        public bool IsCrimson
        {
            get => _worldFlags2[5];
            set => _worldFlags2[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a pumpkin moon event is happening.
        /// </summary>
        public bool IsPumpkinMoon
        {
            get => _worldFlags2[6];
            set => _worldFlags2[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a snow moon event is happening.
        /// </summary>
        public bool IsSnowMoon
        {
            get => _worldFlags2[7];
            set => _worldFlags2[7] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether time is fast forwarded.
        /// </summary>
        public bool FastForwardTime
        {
            get => _worldFlags3[1]; // This is not a typo. The first bit is ignored.
            set => _worldFlags3[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is raining slime.
        /// </summary>
        public bool IsRainingSlime
        {
            get => _worldFlags3[2];
            set => _worldFlags3[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Slime King has been defeated.
        /// </summary>
        public bool IsSlimeKingDefeated
        {
            get => _worldFlags3[3];
            set => _worldFlags3[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Queen Bee has been defeated.
        /// </summary>
        public bool IsQueenBeeDefeated
        {
            get => _worldFlags3[4];
            set => _worldFlags3[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether Duke Fishron has been defeated.
        /// </summary>
        public bool IsFishronDefeated
        {
            get => _worldFlags3[5];
            set => _worldFlags3[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Martians have been defeated.
        /// </summary>
        public bool AreMartiansDefeated
        {
            get => _worldFlags3[6];
            set => _worldFlags3[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Ancient Cultists have been defeated.
        /// </summary>
        public bool AreCultistsDefeated
        {
            get => _worldFlags3[7];
            set => _worldFlags3[7] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Moon Lord has been defeated.
        /// </summary>
        public bool IsMoonLordDefeated
        {
            get => _worldFlags4[0];
            set => _worldFlags4[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Haloween King has been defeated.
        /// </summary>
        public bool IsHalloweenKingDefeated
        {
            get => _worldFlags4[1];
            set => _worldFlags4[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Haloween Tree has been defeated.
        /// </summary>
        public bool IsHalloweenTreeDefeated
        {
            get => _worldFlags4[2];
            set => _worldFlags4[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Christmas Queen has been defeated.
        /// </summary>
        public bool IsChristmasQueenDefeated
        {
            get => _worldFlags4[3];
            set => _worldFlags4[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Christmas Santank has been defeated.
        /// </summary>
        public bool IsChristmasSantankDefeated
        {
            get => _worldFlags4[4];
            set => _worldFlags4[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Christmas Tree has been defeated.
        /// </summary>
        public bool IsChristmasTreeDefeated
        {
            get => _worldFlags4[5];
            set => _worldFlags4[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Golem has been defeated.
        /// </summary>
        public bool IsGolemDefeated
        {
            get => _worldFlags4[6];
            set => _worldFlags4[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a birthday party is being thrown.
        /// </summary>
        public bool IsManualBirthdayParty
        {
            get => _worldFlags4[7];
            set => _worldFlags4[7] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Pirates have been defeated.
        /// </summary>
        public bool ArePiratesDefeated
        {
            get => _worldFlags5[0];
            set => _worldFlags5[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Frost Moon has been defeated.
        /// </summary>
        public bool IsFrostMoonDefeated
        {
            get => _worldFlags5[1];
            set => _worldFlags5[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Goblins have been defeated.
        /// </summary>
        public bool AreGoblinsDefeated
        {
            get => _worldFlags5[2];
            set => _worldFlags5[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a sandstorm is happening.
        /// </summary>
        public bool IsSandstormHappening
        {
            get => _worldFlags5[3];
            set => _worldFlags5[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the DD2 event is happening.
        /// </summary>
        public bool IsDD2EventHappening
        {
            get => _worldFlags5[4];
            set => _worldFlags5[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the first DD2 invasion has been defeated.
        /// </summary>
        public bool IsFirstDD2InvasionDefeated
        {
            get => _worldFlags5[5];
            set => _worldFlags5[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the second DD2 invasion has been defeated.
        /// </summary>
        public bool IsSecondDD2InvasionDefeated
        {
            get => _worldFlags5[6];
            set => _worldFlags5[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the third DD2 invasion has been defeated.
        /// </summary>
        public bool IsThirdDD2InvasionDefeated
        {
            get => _worldFlags5[7];
            set => _worldFlags5[7] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Combat Book has been used.
        /// </summary>
        public bool WasCombatBookUsed
        {
            get => _worldFlags6[0];
            set => _worldFlags6[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the server triggered the lantern event.
        /// </summary>
        public bool IsManualLanternNight
        {
            get => _worldFlags6[1];
            set => _worldFlags6[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Solar Tower has been downed.
        /// </summary>
        public bool IsSolarTowerDowned
        {
            get => _worldFlags6[2];
            set => _worldFlags6[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Vortex Tower has been downed.
        /// </summary>
        public bool IsVortexTowerDowned
        {
            get => _worldFlags6[3];
            set => _worldFlags6[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Nebula Tower has been downed.
        /// </summary>
        public bool IsNebulaTowerDowned
        {
            get => _worldFlags6[4];
            set => _worldFlags6[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Stardust Tower has been downed.
        /// </summary>
        public bool IsStardustTowerDowned
        {
            get => _worldFlags6[5];
            set => _worldFlags6[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether Halloween should be forced for the day.
        /// </summary>
        public bool ForceHalloweenForToday
        {
            get => _worldFlags6[6];
            set => _worldFlags6[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether Christmas should be forced for the day.
        /// </summary>
        public bool ForceChristmasForToday
        {
            get => _worldFlags6[7];
            set => _worldFlags6[7] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a cat was bought.
        /// </summary>
        public bool WasCatBought
        {
            get => _worldFlags7[0];
            set => _worldFlags7[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a dog was bought.
        /// </summary>
        public bool WasDogBought
        {
            get => _worldFlags7[1];
            set => _worldFlags7[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a bunny was bought.
        /// </summary>
        public bool WasBunnyBought
        {
            get => _worldFlags7[2];
            set => _worldFlags7[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether cake is free.
        /// </summary>
        public bool IsFreeCake
        {
            get => _worldFlags7[3];
            set => _worldFlags7[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the world is "drunk".
        /// </summary>
        public bool IsDrunkWorld
        {
            get => _worldFlags7[4];
            set => _worldFlags7[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Empress of Light has been defeated.
        /// </summary>
        public bool IsEmpressOfLightDefeated
        {
            get => _worldFlags7[5];
            set => _worldFlags7[5] = value;
        }

        /// <summary>
        /// Gets or sets a vlaue indicating whether Queen Slime has been defeated.
        /// </summary>
        public bool IsQueenSlimeDefeated
        {
            get => _worldFlags7[6];
            set => _worldFlags7[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether <c>getGoodWorldGen</c> was used upon world generation. Aims to drastically increase the game's difficulty.
        /// </summary>
        public bool IsForTheWorthyWorld
        {
            get => _worldFlags7[7];
            set => _worldFlags7[7] = value;
        }

        PacketId IPacket.Id => PacketId.WorldInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            throw new NotImplementedException();
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Specifies a game (difficulty) mode.
    /// </summary>
    public enum GameMode : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Normal = 0,
        Expert = 1,
        Master = 2,
        Creative = 3
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Specifies a moon phase.
    /// </summary>
    public enum MoonPhase : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Full,
        ThreeQuartersAtLeft,
        HalfAtLeft,
        QuarterAtLeft,
        Empty,
        QuarterAtRight,
        HalfAtRight,
        ThreeQuartersAtRight
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Specifies a world area.
    /// </summary>
    public enum WorldArea : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Forest = 0,
        Forest2 = 1,
        Forest3 = 2,
        Forest4 = 3,
        Corruption = 4,
        Jungle = 5,
        Snow = 6,
        Hallow = 7,
        Crimson = 8,
        Desert = 9,
        Ocean = 10,
        GlowingMushroom = 11,
        Underworld = 12
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
