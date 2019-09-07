﻿using Orion.Entities;

namespace Orion.Items {
    /// <summary>
    /// Provides a wrapper around a Terraria.Item instance that may or may not exist within the world.
    /// </summary>
    public interface IItem : IEntity {
        /// <summary>
        /// Gets the item type.
        /// </summary>
        ItemType Type { get; }

        /// <summary>
        /// Gets or sets the item stack size.
        /// </summary>
        int StackSize { get; set; }

        /// <summary>
        /// Gets the item prefix.
        /// </summary>
        ItemPrefix Prefix { get; }

        /// <summary>
        /// Gets or sets the maximum item stack size.
        /// </summary>
        int MaxStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item rarity.
        /// </summary>
        ItemRarity Rarity { get; set; }

        /// <summary>
        /// Gets or sets the item damage.
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        /// Gets or sets the item's use time.
        /// </summary>
        int UseTime { get; set; }

        /// <summary>
        /// Applies the given <see cref="ItemType"/> to the item.
        /// </summary>
        /// <param name="type">The type.</param>
        void ApplyType(ItemType type);

        /// <summary>
        /// Applies the given prefix to the item.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        void ApplyPrefix(ItemPrefix prefix);
    }
}