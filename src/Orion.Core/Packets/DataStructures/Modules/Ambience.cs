using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.DataStructures.Modules
{
    /// <summary>
    /// A module sent to set ambience.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 6)]
    public struct Ambience : IModule
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets a random seed.
        /// </summary>
        [field: FieldOffset(1)] public int Seed { get; set; }

        /// <summary>
        /// Gets or sets the sky entity type.
        /// </summary>
        [field: FieldOffset(5)] public AmbienceSkyType SkyEntityType { get; set; }

        ModuleId IModule.Id => ModuleId.Ambience;

        int IModule.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 6);

        int IModule.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 6);
    }

    /// <summary>
    /// Specifies an <see cref="Ambience"/> sky type.
    /// </summary>
    public enum AmbienceSkyType : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        BirdsV,
        Wyvern,
        Airship,
        AirBalloon,
        Eyeball,
        Meteor,
        BoneSerpent,
        Bats,
        Butterflies,
        LostKite,
        Vulture,
        PixiePosse,
        Seagulls,
        SlimeBalloons,
        Gastropods,
        Pegasus,
        EaterOfSouls,
        Crimera,
        Hellbats
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
