using System;
using System.Diagnostics;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Orion's implementation of <see cref="ISign"/>.
    /// </summary>
    internal sealed class OrionSign : ISign {
        public int X {
            get => Wrapped.x;
            set => Wrapped.x = value;
        }

        public int Y {
            get => Wrapped.y;
            set => Wrapped.y = value;
        }

        public string Text {
            get => Wrapped.text;
            set => Wrapped.text = value ?? throw new ArgumentNullException(nameof(value));
        }

        internal Terraria.Sign Wrapped { get; }

        public OrionSign(Terraria.Sign terrariaSign) {
            Debug.Assert(terrariaSign != null, $"{nameof(terrariaSign)} should not be null.");

            Wrapped = terrariaSign;
        }
    }
}
