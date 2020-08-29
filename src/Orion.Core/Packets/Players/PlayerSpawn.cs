using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet that's sent from a client to the server when spawning.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 10)]
    public struct PlayerSpawn : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference.

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the X spawn coordinate.
        /// </summary>
        [field: FieldOffset(1)] public short SpawnX { get; set; }

        /// <summary>
        /// Gets or sets the X spawn coordinate.
        /// </summary>
        [field: FieldOffset(3)] public short SpawnY { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates how much time is left until the player respawns.
        /// </summary>
        [field: FieldOffset(5)] public int TimeUntilRespawn{ get; set; }

        /// <summary>
        /// Gets or sets the spawn context.
        /// </summary>
        [field: FieldOffset(9)] public PlayerSpawnContext SpawnContext { get; set; }

        PacketId IPacket.Id => PacketId.PlayerSpawn;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            return span.Read(ref _bytes, 10);
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            return span.Write(ref _bytes, 10);
        }
    }

    /// <summary>
    /// Specifies a <see cref="PlayerSpawn"/> context.
    /// </summary>
    public enum PlayerSpawnContext
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        ReviveFromDeath = 0,
        SpawningIntoTheWorld = 1,
        RecallFromItem = 2
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
