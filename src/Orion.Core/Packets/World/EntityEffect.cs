using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to play sounds, spawn skeletrons, start sundialing or create mimic smoke.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public struct EntityEffect : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        [field: FieldOffset(1)] public EntityEffectAction Action { get; set; }

        PacketId IPacket.Id => PacketId.EntityEffect;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 2);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 2);
    }

    /// <summary>
    /// Specifies an <see cref="EntityEffect"/> action.
    /// </summary>
    public enum EntityEffectAction : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        SpawnSkeletron = 1,
        PlaySound = 2,
        StartSundialing = 3,
        BigMimicSpawnSmoke = 4
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
