using System;
using Orion.Entities;

namespace Orion.Items {
    /// <summary>
    /// Orion's implementation of <see cref="IItem"/>.
    /// </summary>
    internal sealed class OrionItem : OrionEntity, IItem {
        public ItemType Type {
            get => (ItemType)WrappedItem.type;
            set => WrappedItem.type = (int)value;
        }

        public int StackSize {
            get => WrappedItem.stack;
            set => WrappedItem.stack = value;
        }

        public int MaxStackSize {
            get => WrappedItem.maxStack;
            set => WrappedItem.maxStack = value;
        }

        public ItemPrefix Prefix {
            get => (ItemPrefix)WrappedItem.prefix;
            set => WrappedItem.prefix = (byte)value;
        }

        public ItemRarity Rarity {
            get => (ItemRarity)WrappedItem.rare;
            set => WrappedItem.rare = (int)value;
        }

        public int Damage {
            get => WrappedItem.damage;
            set => WrappedItem.damage = value;
        }

        public int UseTime {
            get => WrappedItem.useTime;
            set => WrappedItem.useTime = value;
        }

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

        public void ApplyPrefix(ItemPrefix prefix) {
            WrappedItem.Prefix((int)prefix);
        }
    }
}
