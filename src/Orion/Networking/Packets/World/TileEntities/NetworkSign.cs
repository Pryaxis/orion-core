using System;
using Orion.Utils;
using Orion.World.TileEntities;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Represents a sign that is transmitted over the network.
    /// </summary>
    public sealed class NetSign : AnnotatableObject, ISign {
        private string _name;

        /// <inheritdoc />
        public int Index { get; set; }

        /// <inheritdoc />
        public int X { get; set; }

        /// <inheritdoc />
        public int Y { get; set; }

        /// <inheritdoc />
        public string Text {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
