using System;
using System.Runtime.InteropServices;
using Orion.Core.Utils;

namespace Orion.Core.Packets.DataStructures.Modules
{
    /// <summary>
    /// A module sent add, remove, and teleport through pylons.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 6)]
    public struct Pylon : IModule
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        [field: FieldOffset(0)] public PylonAction Action { get; set; }

        /// <summary>
        /// Gets or sets the pylon's X tile coordinate.
        /// </summary>
        [field: FieldOffset(1)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the pylon's Y tile coordinate.
        /// </summary>
        [field: FieldOffset(3)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the type of pylon.
        /// </summary>
        [field: FieldOffset(5)] public PylonType Type { get; set; }

        ModuleId IModule.Id => ModuleId.Pylon;

        int IModule.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 6);

        int IModule.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 6);
    }

    /// <summary>
    /// Specifies a <see cref="Pylon"/> module action.
    /// </summary>
    public enum PylonAction : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        AddPylon,
        RemovePylon,
        Teleport
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Specifies a <see cref="Pylon"/> type.
    /// </summary>
    public enum PylonType : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        SurfacePurity,
        Jungle,
        Hallow,
        Underground,
        Beach,
        Desert,
        Snow,
        GlowingMushroom,
        Victory
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
