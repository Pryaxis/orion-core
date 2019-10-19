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

using System;
using Orion.Items;
using Orion.Npcs;
using Orion.Utils;

namespace Orion.Events.Npcs {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcLoot"/> event. This event can be canceled.
    /// </summary>
    [EventArgs("npc-loot")]
    public sealed class NpcLootEventArgs : NpcEventArgs, ICancelable, IDirtiable {
        private ItemType _itemType;
        private int _stackSize;
        private ItemPrefix _itemPrefix;

        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <inheritdoc/>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the item's type.
        /// </summary>
        /// <value>The item's type.</value>
        public ItemType ItemType {
            get => _itemType;
            set {
                _itemType = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        /// <value>The item's stack size.</value>
        public int StackSize {
            get => _stackSize;
            set {
                _stackSize = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's prefix.
        /// </summary>
        /// <value>The item's prefix.</value>
        public ItemPrefix ItemPrefix {
            get => _itemPrefix;
            set {
                _itemPrefix = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcLootEventArgs"/> class with the specified NPC,
        /// item type, stack size, and item prefix.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <param name="itemType">The item type.</param>
        /// <param name="stackSize">The stack size.</param>
        /// <param name="itemPrefix">The item prefix.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
        public NpcLootEventArgs(INpc npc, ItemType itemType, int stackSize, ItemPrefix itemPrefix) : base(npc) {
            _itemType = itemType;
            _stackSize = stackSize;
            _itemPrefix = itemPrefix;
        }

        /// <inheritdoc/>
        public void Clean() => IsDirty = false;
    }
}
