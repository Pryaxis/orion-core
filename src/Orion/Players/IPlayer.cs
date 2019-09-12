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

using Orion.Entities;
using Orion.Items;
using Orion.Utils;

namespace Orion.Players {
    /// <summary>
    /// Represents a Terraria player.
    /// </summary>
    public interface IPlayer : IEntity {
        /// <summary>
        /// Gets or sets the player's <see cref="PlayerDifficulty"/>.
        /// </summary>
        PlayerDifficulty Difficulty { get; set; }

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

        /// <summary>
        /// Gets or sets the player's <see cref="PlayerTeam"/>.
        /// </summary>
        PlayerTeam Team { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in PvP.
        /// </summary>
        bool IsInPvp { get; set; }

        /// <summary>
        /// Gets the player's buffs.
        /// </summary>
        IArray<Buff> Buffs { get; }

        /// <summary>
        /// Gets the player's inventory.
        /// </summary>
        IReadOnlyArray<IItem> Inventory { get; }

        /// <summary>
        /// Gets the player's equips (armor, accessories, etc.).
        /// </summary>
        IReadOnlyArray<IItem> Equips { get; }

        /// <summary>
        /// Gets the player's dyes corresponding to the player's equips.
        /// </summary>
        IReadOnlyArray<IItem> Dyes { get; }

        /// <summary>
        /// Gets the player's miscellaneous equips (grappling hook, pet, etc.).
        /// </summary>
        IReadOnlyArray<IItem> MiscEquips { get; }

        /// <summary>
        /// Gets the player's miscellaneous dyes corresponding to the player's miscellaneous equips.
        /// </summary>
        IReadOnlyArray<IItem> MiscDyes { get; }

        /// <summary>
        /// Gets the player's piggy bank.
        /// </summary>
        IReadOnlyArray<IItem> PiggyBank { get; }

        /// <summary>
        /// Gets the player's safe.
        /// </summary>
        IReadOnlyArray<IItem> Safe { get; }

        /// <summary>
        /// Gets the player's trash can.
        /// </summary>
        IItem TrashCan { get; }

        /// <summary>
        /// Gets the player's defender's forge.
        /// </summary>
        IReadOnlyArray<IItem> DefendersForge { get; }

        /// <summary>
        /// Adds the given buff to the player.
        /// </summary>
        /// <param name="buff">The buff.</param>
        void AddBuff(Buff buff);
    }
}
