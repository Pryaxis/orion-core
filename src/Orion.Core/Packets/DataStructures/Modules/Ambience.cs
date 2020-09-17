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
