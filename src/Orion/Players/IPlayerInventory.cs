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
        /// Gets the player's main inventory which includes the hotbar, main inventory rows, coins, ammo, and mouse
        /// cursor.
        /// </summary>
        /// <value>The player's main inventory.</value>
        /// <remarks>This property represents inventory slot indices 0 to 58.</remarks>
        IReadOnlyArray<IItem> Main { get; }

        /// <summary>
        /// Gets the player's equips which include armor, accessories, vanity armor, and vanity accessories.
        /// </summary>
        /// <value>The player's equips.</value>
        /// <remarks>This property represents inventory slot indices 59 to 78.</remarks>
        IReadOnlyArray<IItem> Equips { get; }

        /// <summary>
        /// Gets the player's dyes which correspond to a single half of <see cref="Equips"/>.
        /// </summary>
        /// <value>The player's dyes.</value>
        /// <remarks>This property represents inventory slot indices 79 to 88.</remarks>
        IReadOnlyArray<IItem> Dyes { get; }

        /// <summary>
        /// Gets the player's miscellaneous equips which include the pet, light pet, minecart, mount, and grappling
        /// hook.
        /// </summary>
        /// <value>The player's miscellaneous equips.</value>
        /// <remarks>This represents inventory slot indices 89 to 93.</remarks>
        IReadOnlyArray<IItem> MiscEquips { get; }

        /// <summary>
        /// Gets the player's miscellaneous dyes which correspond to <see cref="MiscEquips"/>.
        /// </summary>
        /// <value>The player's miscellaneous dyes.</value>
        /// <remarks>This represents inventory slot indices 94 to 98.</remarks>
        IReadOnlyArray<IItem> MiscDyes { get; }

        /// <summary>
        /// Gets the player's piggy bank. This is a personal storage container.
        /// </summary>
        /// <value>The player's piggy bank.</value>
        /// <remarks>This represents inventory slot indices 99 to 138.</remarks>
        IReadOnlyArray<IItem> PiggyBank { get; }

        /// <summary>
        /// Gets the player's safe. This is a personal storage container.
        /// </summary>
        /// <value>The player's safe.</value>
        /// <remarks>This represents inventory slot indices 139 to 178.</remarks>
        IReadOnlyArray<IItem> Safe { get; }

        /// <summary>
        /// Gets the player's defender's forge. This is a personal storage container.
        /// </summary>
        /// <value>The player's defender's forge.</value>
        /// <remarks>This represents inventory slot indices 180 to 219.</remarks>
        IReadOnlyArray<IItem> DefendersForge { get; }

        /// <summary>
        /// Gets the player's trash can.
        /// </summary>
        /// <value>The player's trash can.</value>
        /// <remarks>This represents inventory slot index 179.</remarks>
        IItem TrashCan { get; }
    }
}
