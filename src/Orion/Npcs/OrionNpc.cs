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
using System.Diagnostics.CodeAnalysis;
using Orion.Entities;
using Terraria;

namespace Orion.Npcs {
    internal sealed class OrionNpc : OrionEntity<NPC>, INpc {
        public override string Name {
            get => Wrapped.GivenOrTypeName;
            set => Wrapped._givenName = value ?? throw new ArgumentNullException(nameof(value));
        }

        public NpcType Type => (NpcType)Wrapped.netID;

        public int Health {
            get => Wrapped.life;
            set => Wrapped.life = value;
        }

        public int MaxHealth {
            get => Wrapped.lifeMax;
            set => Wrapped.lifeMax = value;
        }

        public OrionNpc(NPC terrariaNpc) : base(terrariaNpc) { }

        public void ApplyType(NpcType type) {
            Wrapped.SetDefaults((int)type);
        }

        public void Damage(int damage, float knockback, int hitDirection, bool isHitCritical = false) {
            Wrapped.StrikeNPC(damage, knockback, hitDirection, isHitCritical);
        }

        public void Kill() {
            Health = 0;
            Wrapped.checkDead();
            IsActive = false;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type} @ ({Position})";

        public void Transform(NpcType newType) {
            Wrapped.Transform((int)newType);
        }
    }
}
