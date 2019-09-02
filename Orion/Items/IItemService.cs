using System;
using System.Collections.Generic;
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
        event EventHandler<ItemSettingDefaultsEventArgs> ItemSettingDefaults;

        /// <summary>
        /// Occurs when an item had its defaults set.
        /// </summary>
        event EventHandler<ItemSetDefaultsEventArgs> ItemSetDefaults;

        /// <summary>
        /// Occurs when an item is being updated.
        /// </summary>
        event EventHandler<ItemUpdatingEventArgs> ItemUpdating;

        /// <summary>
        /// Occurs when an item was updated.
        /// </summary>
        event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
    }
}
