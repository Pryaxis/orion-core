using System;
using Orion.Entities;

namespace Orion.Players {
    /// <summary>
    /// Orion's implementation of <see cref="IPlayer"/>.
    /// </summary>
    internal sealed class OrionPlayer : OrionEntity, IPlayer {
        /// <inheritdoc />
        public new Terraria.Player WrappedEntity { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionPlayer"/> class wrapping the specified Terraria Player
        /// instance.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <c>null</c>.</exception>
        public OrionPlayer(Terraria.Player player) : base(player) {
            WrappedEntity = player ?? throw new ArgumentNullException();
        }
    }
}
