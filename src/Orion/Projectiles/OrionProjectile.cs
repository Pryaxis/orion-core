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
using Orion.Entities;
using Terraria;

namespace Orion.Projectiles {
    internal sealed class OrionProjectile : OrionEntity<Projectile>, IProjectile {
        private string _nameOverride;

        public override string Name {
            get => _nameOverride ?? Wrapped.Name;
            set => _nameOverride = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ProjectileType Type => (ProjectileType)Wrapped.type;

        public int Damage {
            get => Wrapped.damage;
            set => Wrapped.damage = value;
        }

        public float Knockback {
            get => Wrapped.knockBack;
            set => Wrapped.knockBack = value;
        }

        public bool IsHostile {
            get => Wrapped.hostile;
            set => Wrapped.hostile = value;
        }

        public OrionProjectile(Projectile terrariaProjectile) : base(terrariaProjectile) { }

        public void ApplyType(ProjectileType type) {
            Wrapped.SetDefaults((int)type);
        }

        public void Remove() {
            Wrapped.Kill();
        }
    }
}
