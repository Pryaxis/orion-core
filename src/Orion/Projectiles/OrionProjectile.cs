// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Destructurama.Attributed;
using Orion.Entities;

namespace Orion.Projectiles {
    [LogAsScalar]
    internal sealed class OrionProjectile : OrionEntity<Terraria.Projectile>, IProjectile {
        private string? _nameOverride;

        public OrionProjectile(int projectileIndex, Terraria.Projectile terrariaProjectile)
            : base(projectileIndex, terrariaProjectile) { }

        public OrionProjectile(Terraria.Projectile terrariaProjectile) : this(-1, terrariaProjectile) { }

        public override string Name {
            get => _nameOverride ?? Wrapped.Name;
            set => _nameOverride = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ProjectileId Id => (ProjectileId)Wrapped.type;

        public void SetId(ProjectileId id) {
            Wrapped.SetDefaults((int)id);
        }
    }
}
