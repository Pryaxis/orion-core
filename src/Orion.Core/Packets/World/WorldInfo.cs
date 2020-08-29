//using System;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using System.Text;
//using Orion.Core.Packets.DataStructures;

//namespace Orion.Core.Packets.World
//{
//    /// <summary>
//    /// A packet sent from the server to clients to propagate world changes.
//    /// </summary>
//    [StructLayout(LayoutKind.Explicit)]
//    public sealed class WorldInfo : IPacket
//    {
//        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
//        [FieldOffset(4)] private Flags8 _solarFlags;

//        /// <summary>
//        /// Gets or sets the time of day.
//        /// </summary>
//        [field: FieldOffset(0)] public int Time { get; set; }

//        /// <summary>
//        /// Gets or sets a value indicating whether it is currently day time.
//        /// </summary>
//        public bool IsDayTime
//        {
//            get => _solarFlags[0];
//            set => _solarFlags[0] = value;
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether it is currently blood moon.
//        /// </summary>
//        public bool IsBloodMoon
//        {
//            get => _solarFlags[1];
//            set => _solarFlags[1] = value;
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether an eclipse is happening.
//        /// </summary>
//        public bool IsEclipse
//        {
//            get => _solarFlags[2];
//            set => _solarFlags[2] = value;
//        }

//        /// <summary>
//        /// Gets or sets the moon phase.
//        /// </summary>
//        [field: FieldOffset(5)] public MoonPhase MoonPhase { get; set; }

//        /// <summary>
//        /// Gets or sets the world width in tiles.
//        /// </summary>
//        public short MaxTilesX { get; set; }

//        /// <summary>
//        /// Gets or sets the world height in tiles.
//        /// </summary>
//        public short MaxTilesY { get; set; }

//        /// <summary>
//        /// Gets or sets the main spawn point's X coordinate.
//        /// </summary>
//        public short SpawnTileX { get; set; }

//        /// <summary>
//        /// Gets or sets the main spawn point's Y coordinate.
//        /// </summary>
//        public short SpawnTileY { get; set; }

//        /// <summary>
//        /// Gets or sets the height at which surface starts.
//        /// </summary>
//        public short WorldSurface { get; set; }

//        /// <summary>
//        /// Gets or sets the value at which the rock layer starts.
//        /// </summary>
//        public short RockLayer { get; set; }

//        /// <summary>
//        /// Gets or sets the world identifier.
//        /// </summary>
//        public int WorldId { get; set; }

//        /// <summary>
//        /// Gets or sets the world name.
//        /// </summary>
//        public string WorldName { get; set; }

//        /// <summary>
//        /// Gets or sets the game mode.
//        /// </summary>
//        public GameMode GameMode { get; set; }

//        public byte[] UniqueId { get; set; }

//        /// <summary>
//        /// Gets or sets the world generation version.
//        /// </summary>
//        public ulong WorldGenerationVersion { get; set; }

//        /// <summary>
//        /// Gets or sets the moon type. Used to identify the proper texture asset.
//        /// </summary>
//        /// <remarks>See "Notes" at https://terraria.gamepedia.com/Moon_phase for more details.</remarks>
//        public byte MoonType { get; set; }

//        PacketId IPacket.Id => PacketId.WorldInfo;

//        int IPacket.ReadBody(Span<byte> span, PacketContext context)
//        {
//            throw new NotImplementedException();
//        }

//        int IPacket.WriteBody(Span<byte> span, PacketContext context)
//        {
//            throw new NotImplementedException();
//        }
//    }

//    /// <summary>
//    /// Specifies a game (difficulty) mode.
//    /// </summary>
//    public enum GameMode : byte
//    {
//#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
//        Normal = 0,
//        Expert = 1,
//        Master = 2,
//        Creative = 3
//#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
//    }

//    /// <summary>
//    /// Specifies a moon phase.
//    /// </summary>
//    public enum MoonPhase : byte
//    {
//#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
//        Full,
//        ThreeQuartersAtLeft,
//        HalfAtLeft,
//        QuarterAtLeft,
//        Empty,
//        QuarterAtRight,
//        HalfAtRight,
//        ThreeQuartersAtRight
//#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
//    }
//}
