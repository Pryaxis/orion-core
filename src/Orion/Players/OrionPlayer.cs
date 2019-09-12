// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Orion.Entities;
using Orion.Items;
using Orion.Utils;
using Terraria;

namespace Orion.Players {
    internal sealed class OrionPlayer : OrionEntity<Player>, IPlayer {
        private OrionItem _trashCan;

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
        public IReadOnlyArray<IItem> Inventory { get; }
        public IReadOnlyArray<IItem> Equips { get; }
        public IReadOnlyArray<IItem> Dyes { get; }
        public IReadOnlyArray<IItem> MiscEquips { get; }
        public IReadOnlyArray<IItem> MiscDyes { get; }
        public IReadOnlyArray<IItem> PiggyBank { get; }
        public IReadOnlyArray<IItem> Safe { get; }

        public IItem TrashCan {
            get {
                if (_trashCan?.Wrapped != Wrapped.trashItem) {
                    _trashCan = new OrionItem(Wrapped.trashItem);
                }

                return _trashCan;
            }
        }

        public IReadOnlyArray<IItem> DefendersForge { get; }

        public OrionPlayer(Player terrariaPlayer) : base(terrariaPlayer) {
            Buffs = new BuffArray(terrariaPlayer);
            Inventory = new OrionItemArray(terrariaPlayer.inventory);
            Equips = new OrionItemArray(terrariaPlayer.armor);
            Dyes = new OrionItemArray(terrariaPlayer.dye);
            MiscEquips = new OrionItemArray(terrariaPlayer.miscEquips);
            MiscDyes = new OrionItemArray(terrariaPlayer.miscDyes);
            PiggyBank = new OrionItemArray(terrariaPlayer.bank.item);
            Safe = new OrionItemArray(terrariaPlayer.bank2.item);
            DefendersForge = new OrionItemArray(terrariaPlayer.bank3.item);
        }

        public void AddBuff(Buff buff) {
            Wrapped.AddBuff((int)buff.BuffType, (int)(buff.Duration.TotalSeconds * 60.0));
        }


        private class BuffArray : IArray<Buff> {
            public Buff this[int index] {
                get => new Buff((BuffType)_wrapped.buffType[index],
                                TimeSpan.FromSeconds(_wrapped.buffTime[index] / 60.0));
                set {
                    _wrapped.buffType[index] = (int)value.BuffType;
                    _wrapped.buffTime[index] = (int)(value.Duration.TotalSeconds * 60.0);
                }
            }

            public int Count => Player.maxBuffs;

            private readonly Player _wrapped;

            public BuffArray(Player terrariaPlayer) {
                _wrapped = terrariaPlayer;
            }

            public IEnumerator<Buff> GetEnumerator() {
                for (var i = 0; i < Count; ++i) {
                    yield return this[i];
                }
            }

            [ExcludeFromCodeCoverage]
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
