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

namespace Orion.Entities {
    /// <summary>
    /// Represents a Terraria player.
    /// </summary>
    [PublicAPI]
    public interface IPlayer : IEntity {
        /// <summary>
        /// Gets the player's statistics.
        /// </summary>
        IPlayerStats Stats { get; }

        /// <summary>
        /// Gets the player's inventory.
        /// </summary>
        IPlayerInventory Inventory { get; }
    }

    /// <summary>
    /// Represents a player's statistics.
    /// </summary>
    [PublicAPI]
    public interface IPlayerStats {
        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum health.
        /// </summary>
        int MaxHealth { get; set; }

        /// <summary>
        /// Gets or sets the player's mana.
        /// </summary>
        int Mana { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum mana.
        /// </summary>
        int MaxMana { get; set; }
    }

    /// <summary>
    /// Represents a player's inventory.
    /// </summary>
    [PublicAPI]
    public interface IPlayerInventory {
        /// <summary>
        /// Gets the player's held item.
        /// </summary>
        IItem HeldItem { get; }
    }
}
