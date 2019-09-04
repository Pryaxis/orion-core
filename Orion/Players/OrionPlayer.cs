using System.Diagnostics;
using Orion.Entities;

namespace Orion.Players {
    /// <summary>
    /// Orion's implementation of <see cref="IPlayer"/>.
    /// </summary>
    internal sealed class OrionPlayer : OrionEntity, IPlayer {
        internal Terraria.Player WrappedPlayer { get; }

        public OrionPlayer(Terraria.Player terrariaPlayer) : base(terrariaPlayer) {
            Debug.Assert(terrariaPlayer != null, $"{nameof(terrariaPlayer)} should not be null.");

            WrappedPlayer = terrariaPlayer;
        }
    }
}
