// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;
using Orion.Networking.Packets.Entities;
using Orion.Utils;
using Orion.World;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to set world information. This is sent in response to a
    /// <see cref="PlayerJoinPacket"/> and is also sent periodically.
    /// </summary>
    [PublicAPI]
    public sealed class WorldInfoPacket : Packet {
        private int _time;
        private bool _isDaytime;
        private bool _isBloodMoon;
        private bool _isEclipse;
        private byte _moonPhase;
        private short _worldWidth;
        private short _worldHeight;
        private short _spawnX;
        private short _spawnY;
        private short _surfaceY;
        private short _rockLayerY;
        private int _worldId;
        [NotNull] private string _worldName = "";
        private Guid _worldGuid;
        private ulong _worldGeneratorVersion;
        private byte _moonType;
        private byte _treeBackgroundStyle;
        private byte _corruptionBackgroundStyle;
        private byte _jungleBackgroundStyle;
        private byte _snowBackgroundStyle;
        private byte _hallowBackgroundStyle;
        private byte _crimsonBackgroundStyle;
        private byte _desertBackgroundStyle;
        private byte _oceanBackgroundStyle;
        private byte _iceCaveBackgroundStyle;
        private byte _undergroundJungleBackgroundStyle;
        private byte _hellBackgroundStyle;
        private float _windSpeed;
        private byte _numberOfClouds;
        private float _rain;
        private bool _hasSmashedShadowOrb;
        private bool _hasDefeatedEyeOfCthulhu;
        private bool _hasDefeatedEvilBoss;
        private bool _hasDefeatedSkeletron;
        private bool _isHardMode;
        private bool _hasDefeatedClown;
        private bool _isServerSideCharacter;
        private bool _hasDefeatedPlantera;
        private bool _hasDefeatedDestroyer;
        private bool _hasDefeatedTwins;
        private bool _hasDefeatedSkeletronPrime;
        private bool _hasDefeatedMechanicalBoss;
        private bool _isCloudBackgroundActive;
        private bool _isCrimson;
        private bool _isPumpkinMoon;
        private bool _isFrostMoon;
        private bool _isExpertMode;
        private bool _isFastForwardingTime;
        private bool _isSlimeRain;
        private bool _hasDefeatedKingSlime;
        private bool _hasDefeatedQueenBee;
        private bool _hasDefeatedDukeFishron;
        private bool _hasDefeatedMartians;
        private bool _hasDefeatedAncientCultist;
        private bool _hasDefeatedMoonLord;
        private bool _hasDefeatedPumpking;
        private bool _hasDefeatedMourningWood;
        private bool _hasDefeatedIceQueen;
        private bool _hasDefeatedSantank;
        private bool _hasDefeatedEverscream;
        private bool _hasDefeatedGolem;
        private bool _isBirthdayParty;
        private bool _hasDefeatedPirates;
        private bool _hasDefeatedFrostLegion;
        private bool _hasDefeatedGoblins;
        private bool _isSandstorm;
        private bool _isOldOnesArmy;
        private bool _hasDefeatedOldOnesArmyTier1;
        private bool _hasDefeatedOldOnesArmyTier2;
        private bool _hasDefeatedOldOnesArmyTier3;
        private Invasion _currentInvasion;
        private ulong _lobbyId;
        private float _sandstormIntensity;

        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || TreeStyleBoundaries.IsDirty || TreeStyles.IsDirty ||
                                        CaveBackgroundStyleBoundaries.IsDirty || CaveBackgroundStyles.IsDirty;

        /// <inheritdoc />
        public override PacketType Type => PacketType.WorldInfo;

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public int Time {
            get => _time;
            set {
                _time = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is daytime.
        /// </summary>
        public bool IsDaytime {
            get => _isDaytime;
            set {
                _isDaytime = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is a blood moon.
        /// </summary>
        public bool IsBloodMoon {
            get => _isBloodMoon;
            set {
                _isBloodMoon = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is an eclipse.
        /// </summary>
        public bool IsEclipse {
            get => _isEclipse;
            set {
                _isEclipse = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the moon phase.
        /// </summary>
        public byte MoonPhase {
            get => _moonPhase;
            set {
                _moonPhase = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the world width.
        /// </summary>
        public short WorldWidth {
            get => _worldWidth;
            set {
                _worldWidth = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the world height.
        /// </summary>
        public short WorldHeight {
            get => _worldHeight;
            set {
                _worldHeight = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the spawn X coordinate.
        /// </summary>
        public short SpawnX {
            get => _spawnX;
            set {
                _spawnX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the spawn Y coordinate.
        /// </summary>
        public short SpawnY {
            get => _spawnY;
            set {
                _spawnY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the surface Y coordinate.
        /// </summary>
        public short SurfaceY {
            get => _surfaceY;
            set {
                _surfaceY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the rock layer Y coordinate.
        /// </summary>
        public short RockLayerY {
            get => _rockLayerY;
            set {
                _rockLayerY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the world ID.
        /// </summary>
        public int WorldId {
            get => _worldId;
            set {
                _worldId = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the world name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        [NotNull]
        public string WorldName {
            get => _worldName;
            set {
                _worldName = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the world GUID.
        /// </summary>
        public Guid WorldGuid {
            get => _worldGuid;
            set {
                _worldGuid = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the world generator version.
        /// </summary>
        public ulong WorldGeneratorVersion {
            get => _worldGeneratorVersion;
            set {
                _worldGeneratorVersion = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the moon type.
        /// </summary>
        public byte MoonType {
            get => _moonType;
            set {
                _moonType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tree background style.
        /// </summary>
        public byte TreeBackgroundStyle {
            get => _treeBackgroundStyle;
            set {
                _treeBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the corruption background style.
        /// </summary>
        public byte CorruptionBackgroundStyle {
            get => _corruptionBackgroundStyle;
            set {
                _corruptionBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the jungle background style.
        /// </summary>
        public byte JungleBackgroundStyle {
            get => _jungleBackgroundStyle;
            set {
                _jungleBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the snow background style.
        /// </summary>
        public byte SnowBackgroundStyle {
            get => _snowBackgroundStyle;
            set {
                _snowBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the hallow background style.
        /// </summary>
        public byte HallowBackgroundStyle {
            get => _hallowBackgroundStyle;
            set {
                _hallowBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the crimson background style.
        /// </summary>
        public byte CrimsonBackgroundStyle {
            get => _crimsonBackgroundStyle;
            set {
                _crimsonBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the desert background style.
        /// </summary>
        public byte DesertBackgroundStyle {
            get => _desertBackgroundStyle;
            set {
                _desertBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the ocean background style.
        /// </summary>
        public byte OceanBackgroundStyle {
            get => _oceanBackgroundStyle;
            set {
                _oceanBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the ice cave background style.
        /// </summary>
        public byte IceCaveBackgroundStyle {
            get => _iceCaveBackgroundStyle;
            set {
                _iceCaveBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the underground jungle background style.
        /// </summary>
        public byte UndergroundJungleBackgroundStyle {
            get => _undergroundJungleBackgroundStyle;
            set {
                _undergroundJungleBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the hell background style.
        /// </summary>
        public byte HellBackgroundStyle {
            get => _hellBackgroundStyle;
            set {
                _hellBackgroundStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the wind speed.
        /// </summary>
        public float WindSpeed {
            get => _windSpeed;
            set {
                _windSpeed = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the number of clouds.
        /// </summary>
        public byte NumberOfClouds {
            get => _numberOfClouds;
            set {
                _numberOfClouds = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Get the tree style boundaries.
        /// </summary>
        [NotNull]
        public DirtiableArray<int> TreeStyleBoundaries { get; } = new DirtiableArray<int>(3);

        /// <summary>
        /// Gets the tree styles.
        /// </summary>
        [NotNull]
        public DirtiableArray<byte> TreeStyles { get; } = new DirtiableArray<byte>(4);

        /// <summary>
        /// Gets the cave background style boundaries.
        /// </summary>
        [NotNull]
        public DirtiableArray<int> CaveBackgroundStyleBoundaries { get; } = new DirtiableArray<int>(3);

        /// <summary>
        /// Gets the cave background styles.
        /// </summary>
        [NotNull]
        public DirtiableArray<byte> CaveBackgroundStyles { get; } = new DirtiableArray<byte>(4);

        /// <summary>
        /// Gets or sets the rain.
        /// </summary>
        public float Rain {
            get => _rain;
            set {
                _rain = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a shadow orb has been smashed.
        /// </summary>
        public bool HasSmashedShadowOrb {
            get => _hasSmashedShadowOrb;
            set {
                _hasSmashedShadowOrb = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Eye of Cthulhu has been defeated.
        /// </summary>
        public bool HasDefeatedEyeOfCthulhu {
            get => _hasDefeatedEyeOfCthulhu;
            set {
                _hasDefeatedEyeOfCthulhu = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the "evil" boss (Eater of Worlds, or Brain of Cthulhu) has been
        /// defeated.
        /// </summary>
        public bool HasDefeatedEvilBoss {
            get => _hasDefeatedEvilBoss;
            set {
                _hasDefeatedEvilBoss = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Skeletron has been defeated.
        /// </summary>
        public bool HasDefeatedSkeletron {
            get => _hasDefeatedSkeletron;
            set {
                _hasDefeatedSkeletron = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the world is in hard mode.
        /// </summary>
        public bool IsHardMode {
            get => _isHardMode;
            set {
                _isHardMode = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a clown has been defeated.
        /// </summary>
        public bool HasDefeatedClown {
            get => _hasDefeatedClown;
            set {
                _hasDefeatedClown = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the world has server-side characters.
        /// </summary>
        public bool IsServerSideCharacter {
            get => _isServerSideCharacter;
            set {
                _isServerSideCharacter = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Plantera has been defeated.
        /// </summary>
        public bool HasDefeatedPlantera {
            get => _hasDefeatedPlantera;
            set {
                _hasDefeatedPlantera = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Destroyer has been defeated.
        /// </summary>
        public bool HasDefeatedDestroyer {
            get => _hasDefeatedDestroyer;
            set {
                _hasDefeatedDestroyer = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Twins have been defeated.
        /// </summary>
        public bool HasDefeatedTwins {
            get => _hasDefeatedTwins;
            set {
                _hasDefeatedTwins = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Skeletron Prime has been defeated.
        /// </summary>
        public bool HasDefeatedSkeletronPrime {
            get => _hasDefeatedSkeletronPrime;
            set {
                _hasDefeatedSkeletronPrime = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a mechanical boss has been defeated.
        /// </summary>
        public bool HasDefeatedMechanicalBoss {
            get => _hasDefeatedMechanicalBoss;
            set {
                _hasDefeatedMechanicalBoss = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the cloud background is active.
        /// </summary>
        public bool IsCloudBackgroundActive {
            get => _isCloudBackgroundActive;
            set {
                _isCloudBackgroundActive = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the world is crimson.
        /// </summary>
        public bool IsCrimson {
            get => _isCrimson;
            set {
                _isCrimson = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is a pumpkin moon.
        /// </summary>
        public bool IsPumpkinMoon {
            get => _isPumpkinMoon;
            set {
                _isPumpkinMoon = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is a frost moon.
        /// </summary>
        public bool IsFrostMoon {
            get => _isFrostMoon;
            set {
                _isFrostMoon = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the world is in expert mode.
        /// </summary>
        public bool IsExpertMode {
            get => _isExpertMode;
            set {
                _isExpertMode = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether time is being fast-forwarded.
        /// </summary>
        public bool IsFastForwardingTime {
            get => _isFastForwardingTime;
            set {
                _isFastForwardingTime = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is a slime rain.
        /// </summary>
        public bool IsSlimeRain {
            get => _isSlimeRain;
            set {
                _isSlimeRain = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether King Slime has been defeated.
        /// </summary>
        public bool HasDefeatedKingSlime {
            get => _hasDefeatedKingSlime;
            set {
                _hasDefeatedKingSlime = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Queen Bee has been defeated.
        /// </summary>
        public bool HasDefeatedQueenBee {
            get => _hasDefeatedQueenBee;
            set {
                _hasDefeatedQueenBee = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Duke Fishron has been defeated.
        /// </summary>
        public bool HasDefeatedDukeFishron {
            get => _hasDefeatedDukeFishron;
            set {
                _hasDefeatedDukeFishron = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Martians have been defeated.
        /// </summary>
        public bool HasDefeatedMartians {
            get => _hasDefeatedMartians;
            set {
                _hasDefeatedMartians = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Ancient Cultist has been defeated.
        /// </summary>
        public bool HasDefeatedAncientCultist {
            get => _hasDefeatedAncientCultist;
            set {
                _hasDefeatedAncientCultist = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Moon Lord has been defeated.
        /// </summary>
        public bool HasDefeatedMoonLord {
            get => _hasDefeatedMoonLord;
            set {
                _hasDefeatedMoonLord = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a Pumpking has been defeated.
        /// </summary>
        public bool HasDefeatedPumpking {
            get => _hasDefeatedPumpking;
            set {
                _hasDefeatedPumpking = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a Mourning Wood has been defeated.
        /// </summary>
        public bool HasDefeatedMourningWood {
            get => _hasDefeatedMourningWood;
            set {
                _hasDefeatedMourningWood = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an Ice Queen has been defeated.
        /// </summary>
        public bool HasDefeatedIceQueen {
            get => _hasDefeatedIceQueen;
            set {
                _hasDefeatedIceQueen = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a Santank has been defeated.
        /// </summary>
        public bool HasDefeatedSantank {
            get => _hasDefeatedSantank;
            set {
                _hasDefeatedSantank = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an Everscream has been defeated.
        /// </summary>
        public bool HasDefeatedEverscream {
            get => _hasDefeatedEverscream;
            set {
                _hasDefeatedEverscream = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a Golem has been defeated.
        /// </summary>
        public bool HasDefeatedGolem {
            get => _hasDefeatedGolem;
            set {
                _hasDefeatedGolem = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is a birthday party.
        /// </summary>
        public bool IsBirthdayParty {
            get => _isBirthdayParty;
            set {
                _isBirthdayParty = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Pirates have been defeated.
        /// </summary>
        public bool HasDefeatedPirates {
            get => _hasDefeatedPirates;
            set {
                _hasDefeatedPirates = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Frost Legion has been defeated.
        /// </summary>
        public bool HasDefeatedFrostLegion {
            get => _hasDefeatedFrostLegion;
            set {
                _hasDefeatedFrostLegion = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Goblins have been defeated.
        /// </summary>
        public bool HasDefeatedGoblins {
            get => _hasDefeatedGoblins;
            set {
                _hasDefeatedGoblins = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is a sandstorm.
        /// </summary>
        public bool IsSandstorm {
            get => _isSandstorm;
            set {
                _isSandstorm = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Old One's Army is invading.
        /// </summary>
        public bool IsOldOnesArmy {
            get => _isOldOnesArmy;
            set {
                _isOldOnesArmy = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether tier 1 of the Old One's Army has been defeated.
        /// </summary>
        public bool HasDefeatedOldOnesArmyTier1 {
            get => _hasDefeatedOldOnesArmyTier1;
            set {
                _hasDefeatedOldOnesArmyTier1 = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether tier 2 of the Old One's Army has been defeated.
        /// </summary>
        public bool HasDefeatedOldOnesArmyTier2 {
            get => _hasDefeatedOldOnesArmyTier2;
            set {
                _hasDefeatedOldOnesArmyTier2 = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether tier 3 of the Old One's Army has been defeated.
        /// </summary>
        public bool HasDefeatedOldOnesArmyTier3 {
            get => _hasDefeatedOldOnesArmyTier3;
            set {
                _hasDefeatedOldOnesArmyTier3 = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the current invasion.
        /// </summary>
        public Invasion CurrentInvasion {
            get => _currentInvasion;
            set {
                _currentInvasion = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the lobby ID.
        /// </summary>
        public ulong LobbyId {
            get => _lobbyId;
            set {
                _lobbyId = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the sandstorm intensity.
        /// </summary>
        public float SandstormIntensity {
            get => _sandstormIntensity;
            set {
                _sandstormIntensity = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            TreeStyleBoundaries.Clean();
            TreeStyles.Clean();
            CaveBackgroundStyleBoundaries.Clean();
            CaveBackgroundStyles.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{WorldName}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _time = reader.ReadInt32();

            Terraria.BitsByte timeFlags = reader.ReadByte();
            _isDaytime = timeFlags[0];
            _isBloodMoon = timeFlags[1];
            _isEclipse = timeFlags[2];

            _moonPhase = reader.ReadByte();
            _worldWidth = reader.ReadInt16();
            _worldHeight = reader.ReadInt16();
            _spawnX = reader.ReadInt16();
            _spawnY = reader.ReadInt16();
            _surfaceY = reader.ReadInt16();
            _rockLayerY = reader.ReadInt16();
            _worldId = reader.ReadInt32();
            _worldName = reader.ReadString();
            _worldGuid = new Guid(reader.ReadBytes(16));
            _worldGeneratorVersion = reader.ReadUInt64();
            _moonType = reader.ReadByte();
            _treeBackgroundStyle = reader.ReadByte();
            _corruptionBackgroundStyle = reader.ReadByte();
            _jungleBackgroundStyle = reader.ReadByte();
            _snowBackgroundStyle = reader.ReadByte();
            _hallowBackgroundStyle = reader.ReadByte();
            _crimsonBackgroundStyle = reader.ReadByte();
            _desertBackgroundStyle = reader.ReadByte();
            _oceanBackgroundStyle = reader.ReadByte();
            _iceCaveBackgroundStyle = reader.ReadByte();
            _undergroundJungleBackgroundStyle = reader.ReadByte();
            _hellBackgroundStyle = reader.ReadByte();
            _windSpeed = reader.ReadSingle();
            _numberOfClouds = reader.ReadByte();

            for (var i = 0; i < TreeStyleBoundaries.Count; ++i) {
                TreeStyleBoundaries._array[i] = reader.ReadInt32();
            }

            for (var i = 0; i < TreeStyles.Count; ++i) {
                TreeStyles._array[i] = reader.ReadByte();
            }

            for (var i = 0; i < CaveBackgroundStyleBoundaries.Count; ++i) {
                CaveBackgroundStyleBoundaries._array[i] = reader.ReadInt32();
            }

            for (var i = 0; i < CaveBackgroundStyles.Count; ++i) {
                CaveBackgroundStyles._array[i] = reader.ReadByte();
            }

            _rain = reader.ReadSingle();

            Terraria.BitsByte worldFlags = reader.ReadByte();
            Terraria.BitsByte worldFlags2 = reader.ReadByte();
            Terraria.BitsByte worldFlags3 = reader.ReadByte();
            Terraria.BitsByte worldFlags4 = reader.ReadByte();
            Terraria.BitsByte worldFlags5 = reader.ReadByte();
            _hasSmashedShadowOrb = worldFlags[0];
            _hasDefeatedEyeOfCthulhu = worldFlags[1];
            _hasDefeatedEvilBoss = worldFlags[2];
            _hasDefeatedSkeletron = worldFlags[3];
            _isHardMode = worldFlags[4];
            _hasDefeatedClown = worldFlags[5];
            _isServerSideCharacter = worldFlags[6];
            _hasDefeatedPlantera = worldFlags[7];
            _hasDefeatedDestroyer = worldFlags2[0];
            _hasDefeatedTwins = worldFlags2[1];
            _hasDefeatedSkeletronPrime = worldFlags2[2];
            _hasDefeatedMechanicalBoss = worldFlags2[3];
            _isCloudBackgroundActive = worldFlags2[4];
            _isCrimson = worldFlags2[5];
            _isPumpkinMoon = worldFlags2[6];
            _isFrostMoon = worldFlags2[7];
            _isExpertMode = worldFlags3[0];
            _isFastForwardingTime = worldFlags3[1];
            _isSlimeRain = worldFlags3[2];
            _hasDefeatedKingSlime = worldFlags3[3];
            _hasDefeatedQueenBee = worldFlags3[4];
            _hasDefeatedDukeFishron = worldFlags3[5];
            _hasDefeatedMartians = worldFlags3[6];
            _hasDefeatedAncientCultist = worldFlags3[7];
            _hasDefeatedMoonLord = worldFlags4[0];
            _hasDefeatedPumpking = worldFlags4[1];
            _hasDefeatedMourningWood = worldFlags4[2];
            _hasDefeatedIceQueen = worldFlags4[3];
            _hasDefeatedSantank = worldFlags4[4];
            _hasDefeatedEverscream = worldFlags4[5];
            _hasDefeatedGolem = worldFlags4[6];
            _isBirthdayParty = worldFlags4[7];
            _hasDefeatedPirates = worldFlags5[0];
            _hasDefeatedFrostLegion = worldFlags5[1];
            _hasDefeatedGoblins = worldFlags5[2];
            _isSandstorm = worldFlags5[3];
            _isOldOnesArmy = worldFlags5[4];
            _hasDefeatedOldOnesArmyTier1 = worldFlags5[5];
            _hasDefeatedOldOnesArmyTier2 = worldFlags5[6];
            _hasDefeatedOldOnesArmyTier3 = worldFlags5[7];

            _currentInvasion = (Invasion)reader.ReadSByte();
            _lobbyId = reader.ReadUInt64();
            _sandstormIntensity = reader.ReadSingle();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_time);

            Terraria.BitsByte timeFlags = 0;
            timeFlags[0] = _isDaytime;
            timeFlags[1] = _isBloodMoon;
            timeFlags[2] = _isEclipse;
            writer.Write(timeFlags);

            writer.Write(_moonPhase);
            writer.Write(_worldWidth);
            writer.Write(_worldHeight);
            writer.Write(_spawnX);
            writer.Write(_spawnY);
            writer.Write(_surfaceY);
            writer.Write(_rockLayerY);
            writer.Write(_worldId);
            writer.Write(_worldName);
            writer.Write(_worldGuid.ToByteArray());
            writer.Write(_worldGeneratorVersion);
            writer.Write(_moonType);
            writer.Write(_treeBackgroundStyle);
            writer.Write(_corruptionBackgroundStyle);
            writer.Write(_jungleBackgroundStyle);
            writer.Write(_snowBackgroundStyle);
            writer.Write(_hallowBackgroundStyle);
            writer.Write(_crimsonBackgroundStyle);
            writer.Write(_desertBackgroundStyle);
            writer.Write(_oceanBackgroundStyle);
            writer.Write(_iceCaveBackgroundStyle);
            writer.Write(_undergroundJungleBackgroundStyle);
            writer.Write(_hellBackgroundStyle);
            writer.Write(_windSpeed);
            writer.Write(_numberOfClouds);

            foreach (var boundary in TreeStyleBoundaries) {
                writer.Write(boundary);
            }

            foreach (var style in TreeStyles) {
                writer.Write(style);
            }

            foreach (var boundary in CaveBackgroundStyleBoundaries) {
                writer.Write(boundary);
            }

            foreach (var style in CaveBackgroundStyles) {
                writer.Write(style);
            }

            writer.Write(_rain);

            Terraria.BitsByte worldFlags = 0;
            Terraria.BitsByte worldFlags2 = 0;
            Terraria.BitsByte worldFlags3 = 0;
            Terraria.BitsByte worldFlags4 = 0;
            Terraria.BitsByte worldFlags5 = 0;
            worldFlags[0] = _hasSmashedShadowOrb;
            worldFlags[1] = _hasDefeatedEyeOfCthulhu;
            worldFlags[2] = _hasDefeatedEvilBoss;
            worldFlags[3] = _hasDefeatedSkeletron;
            worldFlags[4] = _isHardMode;
            worldFlags[5] = _hasDefeatedClown;
            worldFlags[6] = _isServerSideCharacter;
            worldFlags[7] = _hasDefeatedPlantera;
            worldFlags2[0] = _hasDefeatedDestroyer;
            worldFlags2[1] = _hasDefeatedTwins;
            worldFlags2[2] = _hasDefeatedSkeletronPrime;
            worldFlags2[3] = _hasDefeatedMechanicalBoss;
            worldFlags2[4] = _isCloudBackgroundActive;
            worldFlags2[5] = _isCrimson;
            worldFlags2[6] = _isPumpkinMoon;
            worldFlags2[7] = _isFrostMoon;
            worldFlags3[0] = _isExpertMode;
            worldFlags3[1] = _isFastForwardingTime;
            worldFlags3[2] = _isSlimeRain;
            worldFlags3[3] = _hasDefeatedKingSlime;
            worldFlags3[4] = _hasDefeatedQueenBee;
            worldFlags3[5] = _hasDefeatedDukeFishron;
            worldFlags3[6] = _hasDefeatedMartians;
            worldFlags3[7] = _hasDefeatedAncientCultist;
            worldFlags4[0] = _hasDefeatedMoonLord;
            worldFlags4[1] = _hasDefeatedPumpking;
            worldFlags4[2] = _hasDefeatedMourningWood;
            worldFlags4[3] = _hasDefeatedIceQueen;
            worldFlags4[4] = _hasDefeatedSantank;
            worldFlags4[5] = _hasDefeatedEverscream;
            worldFlags4[6] = _hasDefeatedGolem;
            worldFlags4[7] = _isBirthdayParty;
            worldFlags5[0] = _hasDefeatedPirates;
            worldFlags5[1] = _hasDefeatedFrostLegion;
            worldFlags5[2] = _hasDefeatedGoblins;
            worldFlags5[3] = _isSandstorm;
            worldFlags5[4] = _isOldOnesArmy;
            worldFlags5[5] = _hasDefeatedOldOnesArmyTier1;
            worldFlags5[6] = _hasDefeatedOldOnesArmyTier2;
            worldFlags5[7] = _hasDefeatedOldOnesArmyTier3;
            writer.Write(worldFlags);
            writer.Write(worldFlags2);
            writer.Write(worldFlags3);
            writer.Write(worldFlags4);
            writer.Write(worldFlags5);

            writer.Write((sbyte)_currentInvasion);
            writer.Write(_lobbyId);
            writer.Write(_sandstormIntensity);
        }
    }
}
