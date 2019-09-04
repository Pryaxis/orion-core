using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Framework;
using Orion.Items.Events;

namespace Orion.Items {
    /// <summary>
    /// Provides a mechanism for managing items.
    /// </summary>
    public interface IItemService : IReadOnlyList<IItem>, IService {
        /// <summary>
        /// Occurs when an item is having its defaults set.
        /// </summary>
        event EventHandler<SettingItemDefaultsEventArgs> SettingItemDefaults;

        /// <summary>
        /// Occurs when an item had its defaults set.
        /// </summary>
        event EventHandler<SetItemDefaultsEventArgs> SetItemDefaults;

        /// <summary>
        /// Occurs when an item is being updated.
        /// </summary>
        event EventHandler<UpdatingItemEventArgs> UpdatingItem;

        /// <summary>
        /// Occurs when an item was updated.
        /// </summary>
        event EventHandler<UpdatedItemEventArgs> UpdatedItem;

        /// <summary>
        /// Spawns an item with the specified type at the position with the stack size and prefix.
        /// </summary>
        /// <param name="type">The item type.</param>
        /// <param name="position">The position.</param>
        /// <param name="stackSize">The stack size.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>The resulting item.</returns>
        IItem SpawnItem(ItemType type, Vector2 position, int stackSize = 1, ItemPrefix prefix = ItemPrefix.None);
    }
}
