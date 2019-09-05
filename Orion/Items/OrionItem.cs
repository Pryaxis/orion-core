using System;
using System.Diagnostics;
using Orion.Entities;

namespace Orion.Items {
    /// <summary>
    /// Orion's implementation of <see cref="Orion.Items.IItem"/>.
    /// </summary>
    internal sealed class OrionItem : OrionEntity<Terraria.Item>, IItem {
        public override string Name {
            get => Wrapped.Name;
            set => Wrapped._nameOverride = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ItemType Type {
            get => (ItemType)Wrapped.type;
            set => Wrapped.type = (int)value;
        }

        public int StackSize {
            get => Wrapped.stack;
            set => Wrapped.stack = value;
        }

        public int MaxStackSize {
            get => Wrapped.maxStack;
            set => Wrapped.maxStack = value;
        }

        public ItemPrefix Prefix => (ItemPrefix)Wrapped.prefix;

        public ItemRarity Rarity {
            get => (ItemRarity)Wrapped.rare;
            set => Wrapped.rare = (int)value;
        }

        public int Damage {
            get => Wrapped.damage;
            set => Wrapped.damage = value;
        }

        public int UseTime {
            get => Wrapped.useTime;
            set => Wrapped.useTime = value;
        }

        public OrionItem(Terraria.Item terrariaItem) : base(terrariaItem) { }

        public void ApplyType(ItemType type) => Wrapped.SetDefaults((int)type);
        public void ApplyPrefix(ItemPrefix prefix) => Wrapped.Prefix((int)prefix);
    }
}
