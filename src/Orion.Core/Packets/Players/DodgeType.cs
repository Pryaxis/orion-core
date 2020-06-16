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

namespace Orion.Core.Packets.Players {
    /// <summary>
    /// Describes a dodge type in a <see cref="PlayerDodgePacket"/>.
    /// </summary>
    public enum DodgeType : byte {
        /// <summary>
        /// Indicates a ninja dodge.
        /// </summary>
        Ninja = 1,

        /// <summary>
        /// Indicates a shadow dodge.
        /// </summary>
        Shadow = 2,

        /// <summary>
        /// Indicates a brain of confusion dodge.
        /// </summary>
        BrainOfConfusion = 4
    }
}
