using System.Diagnostics;
using Orion.Entities;

namespace Orion.Players {
    /// <summary>
    /// Orion's implementation of <see cref="IPlayer"/>.
    /// </summary>
    internal sealed class OrionPlayer : OrionEntity, IPlayer {
        public int Hp {
            get => WrappedPlayer.statLife;
            set => WrappedPlayer.statLife = value;
        }

        public int MaxHp {
            get => WrappedPlayer.statLifeMax;
            set => WrappedPlayer.statLifeMax = value;
        }

        public int Mp {
            get => WrappedPlayer.statMana;
            set => WrappedPlayer.statMana = value;
        }

        public int MaxMp {
            get => WrappedPlayer.statManaMax;
            set => WrappedPlayer.statManaMax = value;
        }

        internal Terraria.Player WrappedPlayer { get; }

        public OrionPlayer(Terraria.Player terrariaPlayer) : base(terrariaPlayer) {
            Debug.Assert(terrariaPlayer != null, $"{nameof(terrariaPlayer)} should not be null.");

            WrappedPlayer = terrariaPlayer;
        }
    }
}
