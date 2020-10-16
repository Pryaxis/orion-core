// Copyright (c) 2020 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Runtime.InteropServices;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent from the server to clients to propagate world changes.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 168)]
    public sealed class WorldInfo : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference
        [FieldOffset(32)] private byte _bytes2; // Used to obtain an interior reference
        [FieldOffset(56)] private byte _bytes3; // Used to obtain an interior reference
        [FieldOffset(133)] private byte _bytes4; // Used to obtain an interior reference
        [FieldOffset(4)] private Flags8 _solarFlags;
        [FieldOffset(24)] private string? _worldName;
        [FieldOffset(40)] private byte[] _uniqueIdBytes = new byte[16];
        [FieldOffset(137)] private Flags8 _worldFlags;
        [FieldOffset(138)] private Flags8 _worldFlags2;
        [FieldOffset(139)] private Flags8 _worldFlags3;
        [FieldOffset(140)] private Flags8 _worldFlags4;
        [FieldOffset(141)] private Flags8 _worldFlags5;
        [FieldOffset(142)] private Flags8 _worldFlags6;
        [FieldOffset(143)] private Flags8 _worldFlags7;

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
        [field: FieldOffset(56)] public ulong WorldGenerationVersion { get; set; }

        /// <summary>
        /// Gets or sets the moon type. Used to identify the proper texture asset.
        /// </summary>
        /// <remarks>See "Notes" at https://terraria.gamepedia.com/Moon_phase for more details.</remarks>
        [field: FieldOffset(64)] public byte MoonType { get; set; }

        /// <summary>
        /// Gets or sets the first forest biome background style.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(65)] public byte ForestBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the second forest biome background style.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(66)] public byte ForestBackgroundStyle2 { get; set; }

        /// <summary>
        /// Gets or sets the third forest biome background style.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(67)] public byte ForestBackgroundStyle3 { get; set; }

        /// <summary>
        /// Gets or sets the fourth forest biome background style.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(68)] public byte ForestBackgroundStyle4 { get; set; }

        /// <summary>
        /// Gets or sets the corrupt biome's background style.
        /// </summary>
        [field: FieldOffset(69)] public byte CorruptBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the jungle biome's background style.
        /// </summary>
        [field: FieldOffset(70)] public byte JungleBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the snow biome's background style.
        /// </summary>
        [field: FieldOffset(71)] public byte SnowBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the hallow biome's background style.
        /// </summary>
        [field: FieldOffset(72)] public byte HallowBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the crimson biome's background style.
        /// </summary>
        [field: FieldOffset(73)] public byte CrimsonBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the desert biome's background style.
        /// </summary>
        [field: FieldOffset(74)] public byte DesertBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the ocean biome's background style.
        /// </summary>
        [field: FieldOffset(75)] public byte OceanBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the mushroom biome's background style.
        /// </summary>
        [field: FieldOffset(76)] public byte MushroomBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the underworld biome's background style.
        /// </summary>
        [field: FieldOffset(77)] public byte UnderworldBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the underground ice background style.
        /// </summary>
        [field: FieldOffset(78)] public byte UndergroundIceBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the underground jungle background style.
        /// </summary>
        [field: FieldOffset(79)] public byte UndergroundJungleBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the underground hell background style.
        /// </summary>
        [field: FieldOffset(80)] public byte UndergroundHellBackgroundStyle { get; set; }

        /// <summary>
        /// Gets or sets the wind speed.
        /// </summary>
        [field: FieldOffset(81)] public float WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the number of clouds.
        /// </summary>
        [field: FieldOffset(85)] public byte NumberOfClouds { get; set; }

        /// <summary>
        /// Gets or sets the world's globally unique identifier.
        /// </summary>
        public Guid UniqueId
        {
            get => new Guid(_uniqueIdBytes);
            set => _uniqueIdBytes = value.ToByteArray();
        }

        /// <summary>
        /// Gets or sets the X coordinate that marks the end of the first and the beginning of the second forest region.
        /// </summary>
        /// <remarks>
        /// Terraria provides 4 variants of forest backgrounds. Each variant takes up a certain map area, thus creating a forest region.
        /// The game renders the appropriate forest variant based on the region the player is in (given they're in the right biome).
        /// 0 &lt;= x &lt; <see cref="Forest1Edge"/> represents the first region, thus the game draws the first variant.
        /// </remarks>
        [field: FieldOffset(86)] public int Forest1Edge { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate that marks the end of the second and the beginning of the third forest region.
        /// </summary>
        /// <remarks>
        /// Terraria provides 4 variants of forest backgrounds. Each variant takes up a certain map area, thus creating a forest region.
        /// The game renders the appropriate forest variant based on the region the player is in (given they're in the right biome).
        /// <see cref="Forest1Edge"/> &lt;= x &lt; <see cref="Forest2Edge"/> represents the second region, thus the game draws the second variant.
        /// </remarks>
        [field: FieldOffset(90)] public int Forest2Edge { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate that marks the end of the third and the beginning of the fourth forest region.
        /// </summary>
        /// <remarks>
        /// Terraria provides 4 variants of forest backgrounds. Each variant takes up a certain map area, thus creating a forest region.
        /// The game renders the appropriate forest variant based on the region the player is in (given they're in the right biome).
        /// <see cref="Forest2Edge"/> &lt;= x &lt; <see cref="Forest3Edge"/> represents the third region, thus the game draws the third variant.
        /// </remarks>
        [field: FieldOffset(94)] public int Forest3Edge { get; set; }

        /// <summary>
        /// Gets or sets the style of the first forest variant.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(98)] public byte Forest1Style { get; set; }

        /// <summary>
        /// Gets or sets the style of the second forest variant.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(99)] public byte Forest2Style { get; set; }

        /// <summary>
        /// Gets or sets the style of the third forest variant.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(100)] public byte Forest3Style { get; set; }

        /// <summary>
        /// Gets or sets the style of the fourth forest variant.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of forest backgrounds. Terraria generates a random set of mountain and tree textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(101)] public byte Forest4Style { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate that marks the end of the first and the beginning of the second cave region.
        /// </summary>
        /// <remarks>
        /// Terraria provides 4 variants of cave backgrounds. Each variant takes up a certain map area, thus creating a cave region.
        /// The game renders the appropriate cave variant based on the region the player is in.
        /// 0 &lt;= x &lt; <see cref="Cave1Edge"/> represents the first region, thus the game draws the first variant.
        /// </remarks>
        [field: FieldOffset(102)] public int Cave1Edge { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate that marks the end of the second and the beginning of the third cave region.
        /// </summary>
        /// <remarks>
        /// Terraria provides 4 variants of cave backgrounds. Each variant takes up a certain map area, thus creating a cave region.
        /// The game renders the appropriate cave variant based on the region the player is in.
        /// <see cref="Cave1Edge"/> &lt;= x &lt; <see cref="Cave2Edge"/> represents the second region, thus the game draws the second variant.
        /// </remarks>
        [field: FieldOffset(106)] public int Cave2Edge { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate that marks the end of the third and the beginning of the fourth cave region.
        /// </summary>
        /// <remarks>
        /// Terraria provides 4 variants of cave backgrounds. Each variant takes up a certain map area, thus creating a cave region.
        /// The game renders the appropriate cave variant based on the region the player is in.
        /// <see cref="Cave2Edge"/> &lt;= x &lt; <see cref="Cave3Edge"/> represents the third region, thus the game draws the third variant.
        /// </remarks>
        [field: FieldOffset(110)] public int Cave3Edge { get; set; }

        /// <summary>
        /// Gets or sets the style of the first cave variant.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of cave backgrounds. Terraria generates a random set of textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(114)] public byte Cave1Style { get; set; }

        /// <summary>
        /// Gets or sets the style of the second cave variant.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of cave backgrounds. Terraria generates a random set of textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(115)] public byte Cave2Style { get; set; }

        /// <summary>
        /// Gets or sets the style of the third cave variant.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of cave backgrounds. Terraria generates a random set of textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(116)] public byte Cave3Style { get; set; }

        /// <summary>
        /// Gets or sets the style of the fourth cave variant.
        /// </summary>
        /// <remarks>
        /// There are 4 variants of cave backgrounds. Terraria generates a random set of textures for each 
        /// variant depending on the selected style.
        /// </remarks>
        [field: FieldOffset(117)] public byte Cave4Style { get; set; }

        /// <summary>
        /// Gets the current style variation for each world area. Used for tree effects and background rendering.
        /// </summary>
        [field: FieldOffset(120)] public byte[] AreaStyleVariation { get; } = new byte[13];

        /// <summary>
        /// Gets or sets the rain intensity. Values are [0, 1].
        /// </summary>
        [field: FieldOffset(133)] public float RainIntensity { get; set; }

        /// <summary>
        /// Gets or sets the Copper tier.
        /// </summary>
        [field: FieldOffset(144)] public short CopperTier { get; set; }

        /// <summary>
        /// Gets or sets the Iron tier.
        /// </summary>
        [field: FieldOffset(146)] public short IronTier { get; set; }

        /// <summary>
        /// Gets or sets the Silver tier.
        /// </summary>
        [field: FieldOffset(148)] public short SilverTier { get; set; }

        /// <summary>
        /// Gets or sets the Gold tier.
        /// </summary>
        [field: FieldOffset(150)] public short GoldTier { get; set; }

        /// <summary>
        /// Gets or sets the Cobalt tier.
        /// </summary>
        [field: FieldOffset(152)] public short CobaltTier { get; set; }

        /// <summary>
        /// Gets or sets the Mythril tier.
        /// </summary>
        [field: FieldOffset(154)] public short MythrilTier { get; set; }

        /// <summary>
        /// Gets or sets the Adamantite tier.
        /// </summary>
        [field: FieldOffset(156)] public short AdamantiteTier { get; set; }

        /// <summary>
        /// Gets or sets the current invasion type.
        /// </summary>
        [field: FieldOffset(158)] public sbyte InvasionType { get; set; }

        /// <summary>
        /// Gets or sets the lobby ID.
        /// </summary>
        [field: FieldOffset(159)] public ulong LobbyId { get; set; }

        /// <summary>
        /// Gets or sets the sandstorm severity.
        /// </summary>
        [field: FieldOffset(167)] public float SandstormSeverity { get; set; }

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
            var length = span.Read(ref _bytes, 22);
            length += span[length..].Read(out _worldName);
            length += span[length..].Read(ref _bytes2, 1);
            length += span[length..].Read(ref _uniqueIdBytes[0], 16);
            length += span[length..].Read(ref _bytes3, 62);
            length += span[length..].Read(ref AreaStyleVariation[0], 13);
            length += span[length..].Read(ref _bytes4, 38);
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 22);
            length += span[length..].Write(WorldName);
            length += span[length..].Write(ref _bytes2, 1);
            length += span[length..].Write(ref _uniqueIdBytes[0], 16);
            length += span[length..].Write(ref _bytes3, 62);
            length += span[length..].Write(ref AreaStyleVariation[0], 13);
            length += span[length..].Write(ref _bytes4, 38);
            return length;
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
