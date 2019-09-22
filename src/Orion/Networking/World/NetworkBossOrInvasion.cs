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

using JetBrains.Annotations;

namespace Orion.Networking.World {
    /// <summary>
    /// Specifies a boss or invasion that is transmitted over the network.
    /// </summary>
    [PublicAPI]
    public enum NetworkBossOrInvasion : short {
#pragma warning disable 1591
        MoonLord = -8,
        Martians = -7,
        Eclipse = -6,
        FrostMoon = -5,
        PumpkinMoon = -4,
        Pirates = -3,
        FrostLegion = -2,
        Goblins = -1,
        EyeOfCthulhu = 4,
        EaterOfWorlds = 13,
        Skeletron = 50,
        Retinazer = 125,
        Spazmatism = 126,
        SkeletronPrime = 127,
        PrimeCannon = 128,
        PrimeSaw = 129,
        PrimeVice = 130,
        PrimeLaser = 131,
        TheDestroyer = 134,
        QueenBee = 222,
        Golem = 245,
        BrainOfCthulhu = 266,
        DukeFishron = 370
#pragma warning restore 1591
    }
}
