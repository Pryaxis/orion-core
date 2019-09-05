using System;
using System.Diagnostics;

namespace Orion.World.Signs {
    /// <summary>
    /// Orion's implementation of <see cref="ISign"/>.
    /// </summary>
    internal sealed class OrionSign : ISign {
        public int X {
            get => WrappedSign.x;
            set => WrappedSign.x = value;
        }

        public int Y {
            get => WrappedSign.y;
            set => WrappedSign.y = value;
        }

        public string Text {
            get => WrappedSign.text;
            set => WrappedSign.text = value ?? throw new ArgumentNullException(nameof(value));
        }


        internal Terraria.Sign WrappedSign { get; }


        public OrionSign(Terraria.Sign terrariaSign) {
            Debug.Assert(terrariaSign != null, $"{nameof(terrariaSign)} should not be null.");

            WrappedSign = terrariaSign;
        }
    }
}
