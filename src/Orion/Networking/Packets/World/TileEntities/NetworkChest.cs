using System;
using Orion.Items;
using Orion.Utils;
using Orion.World.TileEntities;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Represents a chest that is transmitted over the network.
    /// </summary>
    public sealed class NetChest : AnnotatableObject, IChest {
        private string _name;

        /// <inheritdoc />
        public int Index { get; set; }

        /// <inheritdoc />
        public int X { get; set; }

        /// <inheritdoc />
        public int Y { get; set; }

        /// <inheritdoc />
        public string Name {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc />
        public IItemList Items => throw new InvalidOperationException();
    }
}
