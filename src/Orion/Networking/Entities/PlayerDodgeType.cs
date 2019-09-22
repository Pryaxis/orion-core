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
using Orion.Networking.Packets.Entities;

namespace Orion.Networking.Entities {
    /// <summary>
    /// Specifies a player dodge type in a <see cref="PlayerDodgePacket"/>.
    /// </summary>
    [PublicAPI]
    public enum PlayerDodgeType : byte {
        /// <summary>
        /// Indicates a ninja dodge. This is caused by a Tabi or Master Ninja Gear.
        /// </summary>
        Ninja = 1,

        /// <summary>
        /// Indicates a shadow dodge. This is caused by Titanium Armor.
        /// </summary>
        Shadow = 2
    }
}
