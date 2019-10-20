// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Npcs;
using Orion.Packets.Players;
using Orion.Utils;
using Orion.World;

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to set world information.
    /// </summary>
    /// <remarks>
    /// This packet is sent in response to a <see cref="PlayerJoinPacket"/> and is sent to synchronize the world state.
    /// </remarks>
    public sealed class WorldInfoPacket : Packet {
        private int _time;
        private bool _isDaytime;
        private bool _isBloodMoon;
        private bool _isEclipse;
        private byte _moonPhase;
        private short _width;
        private short _height;
        private short _spawnX;
        private short _spawnY;
        private short _surfaceY;
        private short _rockLayerY;
        private int _worldId;
        private string _name = string.Empty;
        private Guid _guid;
        private ulong _generatorVersion;
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
        private DirtiableArray<int> _treeStyleBoundaries = new DirtiableArray<int>(3);
        private DirtiableArray<byte> _treeStyles = new DirtiableArray<byte>(4);
        private DirtiableArray<int> _caveBackgroundStyleBoundaries = new DirtiableArray<int>(3);
        private DirtiableArray<byte> _caveBackgroundStyles = new DirtiableArray<byte>(4);
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
        private bool _hasDefeatedLunaticCultist;
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
        private InvasionType _currentInvasionType;
        private ulong _lobbyId;
        private float _sandstormIntensity;

        /// <inheritdoc/>
        public override bool IsDirty =>
            base.IsDirty || _treeStyleBoundaries.IsDirty || _treeStyles.IsDirty ||
            _caveBackgroundStyleBoundaries.IsDirty || _caveBackgroundStyles.IsDirty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.WorldInfo;

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
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
        /// <value><see langword="true"/> if it is daytime; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if it is a blood moon; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if it is an eclipse; otherwise, <see langword="false"/>.</value>
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
        /// <value>The moon phase.</value>
        public byte MoonPhase {
            get => _moonPhase;
            set {
                _moonPhase = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the world's width.
        /// </summary>
        /// <value>The world's width.</value>
        public short Width {
            get => _width;
            set {
                _width = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the world's height.
        /// </summary>
        /// <value>The world's height.</value>
        public short Height {
            get => _height;
            set {
                _height = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the spawn's X coordinate.
        /// </summary>
        /// <value>The spawn's X coordinate.</value>
        public short SpawnX {
            get => _spawnX;
            set {
                _spawnX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the spawn's Y coordinate.
        /// </summary>
        /// <value>The spawn's Y coordinate.</value>
        public short SpawnY {
            get => _spawnY;
            set {
                _spawnY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the surface's Y coordinate.
        /// </summary>
        /// <value>The surface's Y coordinate.</value>
        public short SurfaceY {
            get => _surfaceY;
            set {
                _surfaceY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the rock layer's Y coordinate.
        /// </summary>
        /// <value>The rock layer's Y coordinate.</value>
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
        /// <value>The world ID.</value>
        public int WorldId {
            get => _worldId;
            set {
                _worldId = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the world's name.
        /// </summary>
        /// <value>The world's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Name {
            get => _name;
            set {
                _name = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }


        /// <summary>
        /// Gets or sets the world's GUID.
        /// </summary>
        /// <value>The world's GUID.</value>
        [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Don't care")]
        public Guid Guid {
            get => _guid;
            set {
                _guid = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the world's generator version.
        /// </summary>
        /// <value>The world's generator version.</value>
        public ulong GeneratorVersion {
            get => _generatorVersion;
            set {
                _generatorVersion = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the moon type.
        /// </summary>
        /// <value>The moon type.</value>
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
        /// <value>The tree background style.</value>
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
        /// <value>The corruption background style.</value>
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
        /// <value>The jungle background style.</value>
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
        /// <value>The snow background style.</value>
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
        /// <value>The hallow background style.</value>
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
        /// <value>The crimson background style.</value>
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
        /// <value>The desert background style.</value>
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
        /// <value>The ocean background style.</value>
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
        /// <value>The ice cave background style.</value>
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
        /// <value>The underground jungle background style.</value>
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
        /// <value>The hell background style.</value>
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
        /// <value>The wind speed.</value>
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
        /// <value>The number of clouds.</value>
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
        /// <value>The tree style boundaries.</value>
        public IArray<int> TreeStyleBoundaries => _treeStyleBoundaries;

        /// <summary>
        /// Gets the tree styles.
        /// </summary>
        /// <value>The tree styles.</value>
        public IArray<byte> TreeStyles => _treeStyles;

        /// <summary>
        /// Gets the cave background style boundaries.
        /// </summary>
        /// <value>The cave background style boundaries.</value>
        public IArray<int> CaveBackgroundStyleBoundaries => _caveBackgroundStyleBoundaries;

        /// <summary>
        /// Gets the cave background styles.
        /// </summary>
        /// <value>The cave background styles.</value>
        public IArray<byte> CaveBackgroundStyles => _caveBackgroundStyles;

        /// <summary>
        /// Gets or sets the rain.
        /// </summary>
        /// <value>The rain.</value>
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
        /// <value><see langword="true"/> if a shadow orb has been smashed; otherwise, <see langword="false"/>.</value>
        public bool HasSmashedShadowOrb {
            get => _hasSmashedShadowOrb;
            set {
                _hasSmashedShadowOrb = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="NpcType.EyeOfCthulhu"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the <see cref="NpcType.EyeOfCthulhu"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
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
        /// <value>
        /// <see langword="true"/> if the "evil" boss has been defeated; otherwise, <see langword="false"/>.
        /// </value>
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
        /// <value><see langword="true"/> if Skeletron has been defeated; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if the world is in hard mode; otherwise, <see langword="false"/>.</value>
        public bool IsHardMode {
            get => _isHardMode;
            set {
                _isHardMode = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a <see cref="NpcType.Clown"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if a <see cref="NpcType.Clown"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
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
        /// <value>
        /// <see langword="true"/> if the world has server-side characters; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsServerSideCharacter {
            get => _isServerSideCharacter;
            set {
                _isServerSideCharacter = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="NpcType.Plantera"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if <see cref="NpcType.Plantera"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
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
        /// <value>
        /// <see langword="true"/> if the Destroyer has been defeated; otherwise, <see langword="false"/>.
        /// </value>
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
        /// <value><see langword="true"/> if the Twins have been defeated; otherwise, <see langword="false"/>.</value>
        public bool HasDefeatedTwins {
            get => _hasDefeatedTwins;
            set {
                _hasDefeatedTwins = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="NpcType.SkeletronPrime"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if <see cref="NpcType.SkeletronPrime"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
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
        /// <value>
        /// <see langword="true"/> if a mechanical boss has been defeated; otherwise, <see langword="false"/>.
        /// </value>
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
        /// <value><see langword="true"/> if the cloud background is active; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if the world is crimson; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if it is a pumpkin moon; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if it is a frost moon; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if the world is in expert mode; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if time is being fast-forwarded; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if it is a slime rain; otherwise, <see langword="false"/>.</value>
        public bool IsSlimeRain {
            get => _isSlimeRain;
            set {
                _isSlimeRain = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="NpcType.KingSlime"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if <see cref="NpcType.KingSlime"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedKingSlime {
            get => _hasDefeatedKingSlime;
            set {
                _hasDefeatedKingSlime = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="NpcType.QueenBee"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if <see cref="NpcType.QueenBee"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedQueenBee {
            get => _hasDefeatedQueenBee;
            set {
                _hasDefeatedQueenBee = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="NpcType.DukeFishron"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if <see cref="NpcType.DukeFishron"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedDukeFishron {
            get => _hasDefeatedDukeFishron;
            set {
                _hasDefeatedDukeFishron = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="InvasionType.Martians"/> have been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the <see cref="InvasionType.Martians"/> have been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedMartians {
            get => _hasDefeatedMartians;
            set {
                _hasDefeatedMartians = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a <see cref="NpcType.LunaticCultist"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if a <see cref="NpcType.LunaticCultist"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedLunaticCultist {
            get => _hasDefeatedLunaticCultist;
            set {
                _hasDefeatedLunaticCultist = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Moon Lord has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the Moon Lord has been defeated; otherwise, <see langword="false"/>.
        /// </value>
        public bool HasDefeatedMoonLord {
            get => _hasDefeatedMoonLord;
            set {
                _hasDefeatedMoonLord = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a <see cref="NpcType.Pumpking"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if a <see cref="NpcType.Pumpking"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedPumpking {
            get => _hasDefeatedPumpking;
            set {
                _hasDefeatedPumpking = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a <see cref="NpcType.MourningWood"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if a <see cref="NpcType.MourningWood"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedMourningWood {
            get => _hasDefeatedMourningWood;
            set {
                _hasDefeatedMourningWood = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an <see cref="NpcType.IceQueen"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if an <see cref="NpcType.IceQueen"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedIceQueen {
            get => _hasDefeatedIceQueen;
            set {
                _hasDefeatedIceQueen = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a <see cref="NpcType.SantaNK1"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if a <see cref="NpcType.SantaNK1"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedSantank {
            get => _hasDefeatedSantank;
            set {
                _hasDefeatedSantank = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an <see cref="NpcType.Everscream"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if an <see cref="NpcType.Everscream"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
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
        /// <value><see langword="true"/> if a Golem has been defeated; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if it is a birthday party; otherwise, <see langword="false"/>.</value>
        public bool IsBirthdayParty {
            get => _isBirthdayParty;
            set {
                _isBirthdayParty = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="InvasionType.Pirates"/> have been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the <see cref="InvasionType.Pirates"/> have been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedPirates {
            get => _hasDefeatedPirates;
            set {
                _hasDefeatedPirates = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="InvasionType.FrostLegion"/> has been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the <see cref="InvasionType.FrostLegion"/> has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedFrostLegion {
            get => _hasDefeatedFrostLegion;
            set {
                _hasDefeatedFrostLegion = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="InvasionType.Goblins"/> have been defeated.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the <see cref="InvasionType.Goblins"/> have been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
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
        /// <value><see langword="true"/> if it is a sandstorm; otherwise, <see langword="false"/>.</value>
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
        /// <value><see langword="true"/> if the Old One's Army is invading; otherwise, <see langword="false"/>.</value>
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
        /// <value>
        /// <see langword="true"/> if tier 1 of the Old One's Army has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
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
        /// <value>
        /// <see langword="true"/> if tier 2 of the Old One's Army has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
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
        /// <value>
        /// <see langword="true"/> if tier 3 of the Old One's Army has been defeated; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool HasDefeatedOldOnesArmyTier3 {
            get => _hasDefeatedOldOnesArmyTier3;
            set {
                _hasDefeatedOldOnesArmyTier3 = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the current invasion type.
        /// </summary>
        /// <value>The current invasion type.</value>
        public InvasionType CurrentInvasionType {
            get => _currentInvasionType;
            set {
                _currentInvasionType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the sandstorm intensity.
        /// </summary>
        /// <value>The sandstorm intensity.</value>
        public float SandstormIntensity {
            get => _sandstormIntensity;
            set {
                _sandstormIntensity = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        public override void Clean() {
            base.Clean();
            _treeStyleBoundaries.Clean();
            _treeStyles.Clean();
            _caveBackgroundStyleBoundaries.Clean();
            _caveBackgroundStyles.Clean();
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _time = reader.ReadInt32();

            Terraria.BitsByte timeFlags = reader.ReadByte();
            _isDaytime = timeFlags[0];
            _isBloodMoon = timeFlags[1];
            _isEclipse = timeFlags[2];

            _moonPhase = reader.ReadByte();
            _width = reader.ReadInt16();
            _height = reader.ReadInt16();
            _spawnX = reader.ReadInt16();
            _spawnY = reader.ReadInt16();
            _surfaceY = reader.ReadInt16();
            _rockLayerY = reader.ReadInt16();
            _worldId = reader.ReadInt32();
            _name = reader.ReadString();
            _guid = new Guid(reader.ReadBytes(16));
            _generatorVersion = reader.ReadUInt64();
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

            var treeStyleBoundaries = new int[_treeStyleBoundaries.Count];
            for (var i = 0; i < TreeStyleBoundaries.Count; ++i) {
                treeStyleBoundaries[i] = reader.ReadInt32();
            }

            var treeStyles = new byte[_treeStyles.Count];
            for (var i = 0; i < TreeStyles.Count; ++i) {
                treeStyles[i] = reader.ReadByte();
            }

            var caveBackgroundStyleBoundaries = new int[_caveBackgroundStyleBoundaries.Count];
            for (var i = 0; i < CaveBackgroundStyleBoundaries.Count; ++i) {
                caveBackgroundStyleBoundaries[i] = reader.ReadInt32();
            }

            var caveBackgroundStyles = new byte[_caveBackgroundStyles.Count];
            for (var i = 0; i < CaveBackgroundStyles.Count; ++i) {
                caveBackgroundStyles[i] = reader.ReadByte();
            }

            _treeStyleBoundaries = new DirtiableArray<int>(treeStyleBoundaries);
            _treeStyles = new DirtiableArray<byte>(treeStyles);
            _caveBackgroundStyleBoundaries = new DirtiableArray<int>(caveBackgroundStyleBoundaries);
            _caveBackgroundStyles = new DirtiableArray<byte>(caveBackgroundStyles);
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
            _hasDefeatedLunaticCultist = worldFlags3[7];
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

            _currentInvasionType = (InvasionType)reader.ReadSByte();
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
            writer.Write(_width);
            writer.Write(_height);
            writer.Write(_spawnX);
            writer.Write(_spawnY);
            writer.Write(_surfaceY);
            writer.Write(_rockLayerY);
            writer.Write(_worldId);
            writer.Write(_name);
            writer.Write(_guid.ToByteArray());
            writer.Write(_generatorVersion);
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

            foreach (var boundary in _treeStyleBoundaries) {
                writer.Write(boundary);
            }

            foreach (var style in _treeStyles) {
                writer.Write(style);
            }

            foreach (var boundary in _caveBackgroundStyleBoundaries) {
                writer.Write(boundary);
            }

            foreach (var style in _caveBackgroundStyles) {
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
            worldFlags3[7] = _hasDefeatedLunaticCultist;
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

            writer.Write((sbyte)_currentInvasionType);
            writer.Write(_lobbyId);
            writer.Write(_sandstormIntensity);
        }
    }
}
