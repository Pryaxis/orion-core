// Copyright (c) 2019 Pryaxis & Orion Contributors
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

using Orion.Items;
using Orion.Utils;

namespace Orion.Players {
    /// <summary>
    /// Represents a player's inventory.
    /// </summary>
    public interface IPlayerInventory {
        /// <summary>
        /// Gets the player's main inventory, which includes the hotbar, main inventory rows, coins, ammo, and mouse
        /// cursor.
        /// 
        /// <para/>
        /// 
        /// This represents inventory slot indices 0 to 58.
        /// </summary>
        IReadOnlyArray<IItem> Main { get; }

        /// <summary>
        /// Gets the player's equips, which include armor, accessories, vanity armor, and vanity accessories.
        /// 
        /// <para/>
        /// 
        /// This represents inventory slot indices 58 to 77.
        /// </summary>
        IReadOnlyArray<IItem> Equips { get; }

        /// <summary>
        /// Gets the player's dyes which correspond directly to <see cref="Equips"/>.
        /// 
        /// <para/>
        /// 
        /// This represents inventory slot indices 78 to 97.
        /// </summary>
        IReadOnlyArray<IItem> Dyes { get; }

        /// <summary>
        /// Gets the player's miscellaneous equips, which include the pet, light pet, minecart, mount, and grappling
        /// hook.
        /// 
        /// <para/>
        /// 
        /// This represents inventory slot indices 98 to 102.
        /// </summary>
        IReadOnlyArray<IItem> MiscEquips { get; }

        /// <summary>
        /// Gets the player's miscellaneous dyes which correspond directly to <see cref="MiscEquips"/>.
        /// 
        /// <para/>
        /// 
        /// This represents inventory slot indices 103 to 107. 
        /// </summary>
        IReadOnlyArray<IItem> MiscDyes { get; }

        /// <summary>
        /// Gets the player's piggy bank. This is a personal storage container.
        /// 
        /// <para/>
        /// 
        /// This represents inventory slot indices 108 to 147.
        /// </summary>
        IReadOnlyArray<IItem> PiggyBank { get; }

        /// <summary>
        /// Gets the player's safe. This is a personal storage container.
        /// 
        /// <para/>
        /// 
        /// This represents inventory slot incies 148 to 187.
        /// </summary>
        IReadOnlyArray<IItem> Safe { get; }

        /// <summary>
        /// Gets the player's defender's forge. This is a personal storage container.
        /// 
        /// <para/>
        /// 
        /// This represents inventory slot indices 189 to 228.
        /// </summary>
        IReadOnlyArray<IItem> DefendersForge { get; }

        /// <summary>
        /// Gets the player's trash can.
        /// 
        /// <para/>
        /// 
        /// This represents inventory slot index 188.
        /// </summary>
        IItem TrashCan { get; }
    }
}
