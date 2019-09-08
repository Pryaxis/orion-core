using System;
using Orion.Entities;

namespace Orion.Players {
    internal sealed class OrionPlayer : OrionEntity<Terraria.Player>, IPlayer {
        public override string Name {
            get => Wrapped.name;
            set => Wrapped.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int Health {
            get => Wrapped.statLife;
            set => Wrapped.statLife = value;
        }

        public int MaxHealth {
            get => Wrapped.statLifeMax;
            set => Wrapped.statLifeMax = value;
        }

        public int Mp {
            get => Wrapped.statMana;
            set => Wrapped.statMana = value;
        }

        public int MaxMp {
            get => Wrapped.statManaMax;
            set => Wrapped.statManaMax = value;
        }

        public OrionPlayer(Terraria.Player terrariaPlayer) : base(terrariaPlayer) { }
    }
}
