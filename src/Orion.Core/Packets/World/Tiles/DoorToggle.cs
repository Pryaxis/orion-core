using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World.Tiles
{
    /// <summary>
    /// A packet sent to toggle door states.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 6)]
    public struct DoorToggle : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the toggle action.
        /// </summary>
        [field: FieldOffset(0)] public DoorAction Action { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        [field: FieldOffset(1)] public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        [field: FieldOffset(3)] public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the direction. <see langword="false"/> indicates the left direction whereas <see langword="true"/> indicates the right direction (or whether the player is standing above a trapdoor with trapdoor actions).
        /// </summary>
        /// <remarks>Used with <see cref="DoorAction.Open"/>, <see cref="DoorAction.TrapdoorOpen"/>, and <see cref="DoorAction.TrapdoorClose"/>.</remarks>
        [field: FieldOffset(5)] public bool IsRightDirectionOpen { get; set; }

        PacketId IPacket.Id => PacketId.DoorToggle;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 6);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 6);
    }

    /// <summary>
    /// Specifies a <see cref="DoorToggle"/> action.
    /// </summary>
    public enum DoorAction : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Open,
        Close,
        TrapdoorOpen,
        TrapdoorClose,
        TallGateOpen,
        TallGateClose
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
