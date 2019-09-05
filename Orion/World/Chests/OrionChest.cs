using System;
using System.Diagnostics;
using Orion.Items;

namespace Orion.World.Chests {
    /// <summary>
    /// Orion's implementation of <see cref="IChest"/>.
    /// </summary>
    internal sealed class OrionChest : IChest {
        public int X {
            get => WrappedChest.x;
            set => WrappedChest.x = value;
        }

        public int Y {
            get => WrappedChest.y;
            set => WrappedChest.y = value;
        }

        public string Name {
            get => WrappedChest.name;
            set => WrappedChest.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IItemList Items { get; }


        internal Terraria.Chest WrappedChest { get; }


        public OrionChest(Terraria.Chest terrariaChest) {
            Debug.Assert(terrariaChest != null, $"{nameof(terrariaChest)} should not be null.");

            WrappedChest = terrariaChest;

            Items = new OrionItemList(WrappedChest.item);
        }
    }
}
