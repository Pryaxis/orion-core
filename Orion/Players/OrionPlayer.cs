using System;
using Orion.Entities;

namespace Orion.Players {
    /// <summary>
    /// Orion's implementation of <see cref="IPlayer"/>.
    /// </summary>
    internal sealed class OrionPlayer : OrionEntity, IPlayer {
        public Terraria.Player WrappedPlayer { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionPlayer"/> class wrapping the specified Terraria Player
        /// instance.
        /// </summary>
        /// <param name="terrariaPlayer">The player.</param>
        /// <exception cref="ArgumentNullException"><paramref name="terrariaPlayer"/> is <c>null</c>.</exception>
        public OrionPlayer(Terraria.Player terrariaPlayer) : base(terrariaPlayer) {
            WrappedPlayer = terrariaPlayer ?? throw new ArgumentNullException();
        }
    }
}
