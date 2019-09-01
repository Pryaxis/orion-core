namespace Orion.Networking.Packets {
    using System;
    using System.IO;
    using System.Text;
    using Orion.Networking.Packets.Extensions;
    using Orion.World;

    /// <summary>
    /// Packet sent to the client to provide information about the world.
    /// </summary>
    public sealed class WorldInfoPacket : TerrariaPacket {
        private string _worldName = "";

        private protected override int HeaderlessLength => 117 + WorldName.GetBinaryLength(Encoding.UTF8);

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => false;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.WorldInfo;

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets the time flags.
        /// </summary>
        public WorldTimeFlags TimeFlags { get; set; }

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
        /// Gets or sets the spawn X position.
        /// </summary>
        public short SpawnX { get; set; }

        /// <summary>
        /// Gets or sets the spawn Y position.
        /// </summary>
        public short SpawnY { get; set; }

        /// <summary>
        /// Gets or sets the surface Y position.
        /// </summary>
        public short SurfaceY { get; set; }

        /// <summary>
        /// Gets or sets the rock layer Y position.
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
        /// Gets or sets the state flags.
        /// </summary>
        public WorldStateFlags StateFlags { get; set; }

        /// <summary>
        /// Gets or sets the state flags, part two.
        /// </summary>
        public WorldStateFlags2 StateFlags2 { get; set; }

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

        /// <summary>
        /// Reads a <see cref="WorldInfoPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static WorldInfoPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            return new WorldInfoPacket {
                Time = reader.ReadInt32(),
                TimeFlags = (WorldTimeFlags)reader.ReadByte(),
                MoonPhase = reader.ReadByte(),
                WorldWidth = reader.ReadInt16(),
                WorldHeight = reader.ReadInt16(),
                SpawnX = reader.ReadInt16(),
                SpawnY = reader.ReadInt16(),
                SurfaceY = reader.ReadInt16(),
                RockLayerY = reader.ReadInt16(),
                WorldId = reader.ReadInt32(),
                WorldName = reader.ReadString(),
                WorldGuid = new Guid(reader.ReadBytes(16)),
                WorldGeneratorVersion = reader.ReadUInt64(),
                MoonType = reader.ReadByte(),
                TreeBackgroundStyle = reader.ReadByte(),
                CorruptionBackgroundStyle = reader.ReadByte(),
                JungleBackgroundStyle = reader.ReadByte(),
                SnowBackgroundStyle = reader.ReadByte(),
                HallowBackgroundStyle = reader.ReadByte(),
                CrimsonBackgroundStyle = reader.ReadByte(),
                DesertBackgroundStyle = reader.ReadByte(),
                OceanBackgroundStyle = reader.ReadByte(),
                IceCaveBackgroundStyle = reader.ReadByte(),
                UndergroundJungleBackgroundStyle = reader.ReadByte(),
                HellBackgroundStyle = reader.ReadByte(),
                WindSpeed = reader.ReadSingle(),
                NumberOfClouds = reader.ReadByte(),
                TreeStyleBoundaries = new[] {reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32()},
                TreeStyles = reader.ReadBytes(4),
                CaveBackgroundStyleBoundaries = new[] {reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32()},
                CaveBackgroundStyles = reader.ReadBytes(4),
                Rain = reader.ReadSingle(),
                StateFlags = (WorldStateFlags)reader.ReadInt32(),
                StateFlags2 = (WorldStateFlags2)reader.ReadByte(),
                InvasionType = (InvasionType)reader.ReadSByte(),
                LobbyId = reader.ReadUInt64(),
                SandstormIntensity = reader.ReadSingle(),
            };
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Time);
            writer.Write((byte)TimeFlags);
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
            writer.Write((int)StateFlags);
            writer.Write((byte)StateFlags2);
            writer.Write((sbyte)InvasionType);
            writer.Write(LobbyId);
            writer.Write(SandstormIntensity);
        }
    }
    
    /// <summary>
    /// Flags that specify information about the time of the world.
    /// </summary>
    [Flags]
    public enum WorldTimeFlags : byte {
        /// <summary>
        /// Indicates that it is day.
        /// </summary>
        IsDaytime = 1 << 0,

        /// <summary>
        /// Indicates that it is a blood moon.
        /// </summary>
        IsBloodMoon = 1 << 1,

        /// <summary>
        /// Indicates that it is an eclipse.
        /// </summary>
        IsEclipse = 1 << 2
    }

    /// <summary>
    /// Flags that specify information about the state of the world.
    /// </summary>
    [Flags]
    public enum WorldStateFlags {
        /// <summary>
        /// Indicates nothing.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that a shadow orb has been smashed.
        /// </summary>
        SmashedShadowOrb = 1 << 0,

        /// <summary>
        /// Indicates that the Eye of Cthulhu has been defeated.
        /// </summary>
        DefeatedEyeOfCthulhu = 1 << 1,

        /// <summary>
        /// Indicates that the "evil boss" (Eater of Worlds, or Brain of Cthulhu) has been defeated.
        /// </summary>
        DefeatedEvilBoss = 1 << 2,

        /// <summary>
        /// Indicates that Skeletron has been defeated.
        /// </summary>
        DefeatedSkeletron = 1 << 3,

        /// <summary>
        /// Indicates that the world is hard mode.
        /// </summary>
        HardMode = 1 << 4,

        /// <summary>
        /// Indicates that a clown has been defeated.
        /// </summary>
        DefeatedClown = 1 << 5,

        /// <summary>
        /// Indicates that the world has server-side characters.
        /// </summary>
        ServerSideCharacter = 1 << 6,

        /// <summary>
        /// Indicates that Plantera has been defeated.
        /// </summary>
        DefeatedPlantera = 1 << 7,

        /// <summary>
        /// Indicates that the Destroyer has been defeated.
        /// </summary>
        DefeatedDestroyer = 1 << 8,

        /// <summary>
        /// Indicates that the Twins have been defeated.
        /// </summary>
        DefeatedTwins = 1 << 9,

        /// <summary>
        /// Indicates that Skeletron Prime has been defeated.
        /// </summary>
        DefeatedSkeletronPrime = 1 << 10,

        /// <summary>
        /// Indicates that any mechanical boss has been defeated.
        /// </summary>
        DefeatedMechanicalBoss = 1 << 11,

        /// <summary>
        /// Indicates that the cloud background is active.
        /// </summary>
        CloudBackgroundActive = 1 << 12,

        /// <summary>
        /// Indicates that the world is crimson, not corruption.
        /// </summary>
        Crimson = 1 << 13,

        /// <summary>
        /// Indicates that the world is in a pumpkin moon.
        /// </summary>
        PumpkinMoon = 1 << 14,

        /// <summary>
        /// Indicates that the world is in a frost moon.
        /// </summary>
        FrostMoon = 1 << 15,

        /// <summary>
        /// Indicates that the world is in expert mode.
        /// </summary>
        ExpertMode = 1 << 16,

        /// <summary>
        /// Indicates that time is fast-forwarding.
        /// </summary>
        FastForwardingTime = 1 << 17,

        /// <summary>
        /// Indicates that there is a slime rain.
        /// </summary>
        SlimeRain = 1 << 18,

        /// <summary>
        /// Indicates that the King Slime has been defeated.
        /// </summary>
        DefeatedKingSlime = 1 << 19,

        /// <summary>
        /// Indicates that the Queen Bee has been defeated.
        /// </summary>
        DefeatedQueenBee = 1 << 20,

        /// <summary>
        /// Indicates that Duke Fishron has been defeated.
        /// </summary>
        DefeatedDukeFishron = 1 << 21,

        /// <summary>
        /// Indicates that the Martians have been defeated.
        /// </summary>
        DefeatedMartians = 1 << 22,

        /// <summary>
        /// Indicates that the Ancient Cultist has been defeated.
        /// </summary>
        DefeatedAncientCultist = 1 << 23,

        /// <summary>
        /// Indicates that the Moon Lord has been defeated.
        /// </summary>
        DefeatedMoonLord = 1 << 24,

        /// <summary>
        /// Indicates that a Pumpking has been defeated.
        /// </summary>
        DefeatedPumpking = 1 << 25,

        /// <summary>
        /// Indicates that a Mourning Wood has been defeated.
        /// </summary>
        DefeatedMourningWood = 1 << 26,

        /// <summary>
        /// Indicates that an Ice Queen has been defeated.
        /// </summary>
        DefeatedIceQueen = 1 << 27,

        /// <summary>
        /// Indicates that a Santank has been defeated.
        /// </summary>
        DefeatedSantank = 1 << 28,

        /// <summary>
        /// Indicates that an Everscream has been defeated.
        /// </summary>
        DefeatedEverscream = 1 << 29,

        /// <summary>
        /// Indicates that the Golem has been defeated.
        /// </summary>
        DefeatedGolem = 1 << 30,

        /// <summary>
        /// Indicates that it is a birthday party.
        /// </summary>
        BirthdayParty = 1 << 31,
    }

    /// <summary>
    /// Flags that specify information about the state of the world.
    /// </summary>
    [Flags]
    public enum WorldStateFlags2 {
        /// <summary>
        /// Indicates that the Pirates have been defeated.
        /// </summary>
        DefeatedPirates = 1 << 0,

        /// <summary>
        /// Indicates that the Frost Legion has been defeated.
        /// </summary>
        DefeatedFrostLegion = 1 << 1,

        /// <summary>
        /// Indicates that the Goblins have been defeated.
        /// </summary>
        DefeatedGoblins = 1 << 2,

        /// <summary>
        /// Indicates that it is a sandstorm.
        /// </summary>
        Sandstorm = 1 << 3,

        /// <summary>
        /// Indicates that it is an Old One's Army event.
        /// </summary>
        OldOnesArmy = 1 << 4,

        /// <summary>
        /// Indicates that tier 1 of the Old One's Army has been defeated.
        /// </summary>
        DefeatedOldOnesArmyTier1 = 1 << 5,

        /// <summary>
        /// Indicates that tier 2 of the Old One's Army has been defeated.
        /// </summary>
        DefeatedOldOnesArmyTier2 = 1 << 6,

        /// <summary>
        /// Indicates that tier 3 of the Old One's Army has been defeated.
        /// </summary>
        DefeatedOldOnesArmyTier3 = 1 << 7,
    }
}
