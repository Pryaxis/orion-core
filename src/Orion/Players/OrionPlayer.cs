using System;
using Orion.Entities;
using Orion.Utils;

namespace Orion.Players {
    internal sealed class OrionPlayer : OrionEntity<Terraria.Player>, IPlayer {
        public override string Name {
            get => Wrapped.name;
            set => Wrapped.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public PlayerDifficulty Difficulty {
            get => (PlayerDifficulty)Wrapped.difficulty;
            set => Wrapped.difficulty = (byte)value;
        }

        public int Health {
            get => Wrapped.statLife;
            set => Wrapped.statLife = value;
        }

        public int MaxHealth {
            get => Wrapped.statLifeMax;
            set => Wrapped.statLifeMax = value;
        }

        public int Mana {
            get => Wrapped.statMana;
            set => Wrapped.statMana = value;
        }

        public int MaxMana {
            get => Wrapped.statManaMax;
            set => Wrapped.statManaMax = value;
        }

        public PlayerTeam Team {
            get => (PlayerTeam)Wrapped.team;
            set => Wrapped.team = (int)value;
        }

        public bool IsInPvp {
            get => Wrapped.hostile;
            set => Wrapped.hostile = value;
        }

        public IArray<Buff> Buffs { get; }

        public OrionPlayer(Terraria.Player terrariaPlayer) : base(terrariaPlayer) {
            Buffs = new BuffArray(terrariaPlayer);
        }

        public void AddBuff(Buff buff) {
            Wrapped.AddBuff((int)buff.BuffType, (int)(buff.Duration.TotalSeconds * 60.0));
        }


        private class BuffArray : IArray<Buff> {
            public Buff this[int index] {
                get => new Buff((BuffType)_wrapped.buffType[index], TimeSpan.FromSeconds(_wrapped.buffTime[index] / 60.0));
                set {
                    _wrapped.buffType[index] = (int)value.BuffType;
                    _wrapped.buffTime[index] = (int)(value.Duration.TotalSeconds * 60.0);
                }
            }

            public int Count => Terraria.Player.maxBuffs;

            private readonly Terraria.Player _wrapped;

            public BuffArray(Terraria.Player terrariaPlayer) {
                _wrapped = terrariaPlayer;
            }
        }
    }
}
