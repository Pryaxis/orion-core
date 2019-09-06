using System;
using System.Diagnostics;
using Orion.Items;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Orion's implementation of <see cref="IChest"/>.
    /// </summary>
    internal sealed class OrionChest : IChest {
        public int X {
            get => Wrapped.x;
            set => Wrapped.x = value;
        }

        public int Y {
            get => Wrapped.y;
            set => Wrapped.y = value;
        }

        public string Name {
            get => Wrapped.name;
            set => Wrapped.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IItemList Items { get; }

        internal Terraria.Chest Wrapped { get; }

        public OrionChest(Terraria.Chest terrariaChest) {
            Debug.Assert(terrariaChest != null, $"{nameof(terrariaChest)} should not be null.");

            Wrapped = terrariaChest;

            Items = new OrionItemList(Wrapped.item);
        }
    }
}
