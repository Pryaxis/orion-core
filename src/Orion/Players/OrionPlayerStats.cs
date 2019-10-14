// Copyright (c) 2019 Pryaxis & Orion Contributors
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

using System.Diagnostics;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    internal sealed class OrionPlayerStats : IPlayerStats {
        private readonly TerrariaPlayer _wrapped;

        public int Health {
            get => _wrapped.statLife;
            set => _wrapped.statLife = value;
        }

        public int MaxHealth {
            get => _wrapped.statLifeMax;
            set => _wrapped.statLifeMax = value;
        }

        public int Mana {
            get => _wrapped.statMana;
            set => _wrapped.statMana = value;
        }

        public int MaxMana {
            get => _wrapped.statManaMax;
            set => _wrapped.statManaMax = value;
        }

        public OrionPlayerStats(TerrariaPlayer terrariaPlayer) {
            Debug.Assert(terrariaPlayer != null, "Terraria player should not be null");

            _wrapped = terrariaPlayer;
        }
    }
}
