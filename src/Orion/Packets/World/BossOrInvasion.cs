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

using System.Diagnostics.CodeAnalysis;

namespace Orion.Packets.World {
    /// <summary>
    /// Specifies a boss or invasion in a <see cref="BossOrInvasionPacket"/>.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum BossOrInvasion : short {
        /// <summary>
        /// Indicates that the Moon Lord should be summoned.
        /// </summary>
        MoonLord = -8,

        /// <summary>
        /// Indicates that the Martians should be summoned.
        /// </summary>
        Martians = -7,

        /// <summary>
        /// Indicates that the Eclipse should be summoned.
        /// </summary>
        Eclipse = -6,

        /// <summary>
        /// Indicates that the Frost Moon should be summoned.
        /// </summary>
        FrostMoon = -5,

        /// <summary>
        /// Indicates that the Pumpkin Moon should be summoned.
        /// </summary>
        PumpkinMoon = -4,

        /// <summary>
        /// Indicates that the Pirates should be summoned.
        /// </summary>
        Pirates = -3,

        /// <summary>
        /// Indicates that the Frost Legion should be summoned.
        /// </summary>
        FrostLegion = -2,

        /// <summary>
        /// Indicates that the Goblins should be summoned.
        /// </summary>
        Goblins = -1,

        /// <summary>
        /// Indicates that the Eye of Cthulhu should be summoned.
        /// </summary>
        EyeOfCthulhu = 4,

        /// <summary>
        /// Indicates that the Eater of Worlds should be summoned.
        /// </summary>
        EaterOfWorlds = 13,

        /// <summary>
        /// Indicates that Skeletron should be summoned.
        /// </summary>
        Skeletron = 50,

        /// <summary>
        /// Indicates that Retinazer should be summoned.
        /// </summary>
        Retinazer = 125,

        /// <summary>
        /// Indicates that Spazmatism should be summoned.
        /// </summary>
        Spazmatism = 126,

        /// <summary>
        /// Indicates that Skeletron Prime should be summoned.
        /// </summary>
        SkeletronPrime = 127,

        /// <summary>
        /// Indicates that Skeletron Prime's cannon should be summoned.
        /// </summary>
        PrimeCannon = 128,

        /// <summary>
        /// Indicates that Skeletron Prime's saw should be summoned.
        /// </summary>
        PrimeSaw = 129,

        /// <summary>
        /// Indicates that Skeletron Prime's vice should be summoned.
        /// </summary>
        PrimeVice = 130,

        /// <summary>
        /// Indicates that Skeletron Prime's laser should be summoned.
        /// </summary>
        PrimeLaser = 131,

        /// <summary>
        /// Indicates that The Destroyer should be summoned.
        /// </summary>
        TheDestroyer = 134,

        /// <summary>
        /// Indicates that Queen Bee should be summoned.
        /// </summary>
        QueenBee = 222,

        /// <summary>
        /// Indicates that Golem should be summoned.
        /// </summary>
        Golem = 245,

        /// <summary>
        /// Indicates that the Brain Of Cthulhu should be summoned.
        /// </summary>
        BrainOfCthulhu = 266,

        /// <summary>
        /// Indicates that Duke Fishron should be summoned.
        /// </summary>
        DukeFishron = 370
    }
}
