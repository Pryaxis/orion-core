using System;
using Orion.Entities;

namespace Orion.Items {
    /// <summary>
    /// Orion's implementation of <see cref="IItem"/>.
    /// </summary>
    internal sealed class OrionItem : OrionEntity, IItem {
        /// <inheritdoc />
        public ItemType Type {
            get => (ItemType)WrappedItem.type;
            set => WrappedItem.type = (int)value;
        }

        /// <inheritdoc />
        public int StackSize {
            get => WrappedItem.stack;
            set => WrappedItem.stack = value;
        }

        /// <inheritdoc />
        public int MaxStackSize {
            get => WrappedItem.maxStack;
            set => WrappedItem.maxStack = value;
        }

        /// <inheritdoc />
        public ItemPrefix Prefix {
            get => (ItemPrefix)WrappedItem.prefix;
            set => WrappedItem.prefix = (byte)value;
        }

        /// <inheritdoc />
        public ItemRarity Rarity {
            get => (ItemRarity)WrappedItem.rare;
            set => WrappedItem.rare = (int)value;
        }

        /// <inheritdoc />
        public int Damage {
            get => WrappedItem.damage;
            set => WrappedItem.damage = value;
        }

        /// <inheritdoc />
        public int UseTime {
            get => WrappedItem.useTime;
            set => WrappedItem.useTime = value;
        }

        /// <inheritdoc />
        public Terraria.Item WrappedItem { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OrionItem"/> class wrapping the specified Terraria Item
        /// instance.
        /// </summary>
        /// <param name="terrariaItem">The item.</param>
        /// <exception cref="ArgumentNullException"><paramref name="terrariaItem"/> is <c>null</c>.</exception>
        public OrionItem(Terraria.Item terrariaItem) : base(terrariaItem) {
            WrappedItem = terrariaItem ?? throw new ArgumentNullException(nameof(terrariaItem));
        }

        /// <inheritdoc />
        public void ApplyPrefix(ItemPrefix prefix) {
            WrappedItem.Prefix((int)prefix);
        }
    }
}
