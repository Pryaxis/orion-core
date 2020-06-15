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
using System.Collections.Generic;
using System.Diagnostics;
using Orion.Core.Collections;
using Orion.Core.DataStructures;
using Orion.Core.Events.Projectiles;
using Orion.Core.Framework;
using Serilog;

namespace Orion.Core.Projectiles {
    [Binding("orion-projs", Author = "Pryaxis", Priority = BindingPriority.Lowest)]
    internal sealed class OrionProjectileService : OrionService, IProjectileService {
        private readonly object _lock = new object();

        public OrionProjectileService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            // Construct the `Projectiles` array. Note that the last projectile should be ignored, as it is not a real
            // projectile.
            Projectiles = new WrappedReadOnlyList<OrionProjectile, Terraria.Projectile>(
                Terraria.Main.projectile.AsMemory(..^1),
                (projectileIndex, terrariaProjectile) => new OrionProjectile(projectileIndex, terrariaProjectile));

            OTAPI.Hooks.Projectile.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            OTAPI.Hooks.Projectile.PreUpdate = PreUpdateHandler;
        }

        public IReadOnlyList<IProjectile> Projectiles { get; }

        public override void Dispose() {
            OTAPI.Hooks.Projectile.PreSetDefaultsById = null;
            OTAPI.Hooks.Projectile.PreUpdate = null;
        }

        public IProjectile SpawnProjectile(
                ProjectileId id, Vector2f position, Vector2f velocity, int damage, float knockback) {
            // Not localized because this string is developer-facing.
            Log.Debug("Spawning {ProjectileId} at {Position}", id, position);

            lock (_lock) {
                var projectileIndex = Terraria.Projectile.NewProjectile(
                    position.X, position.Y, velocity.X, velocity.Y, (int)id, damage, knockback);
                Debug.Assert(projectileIndex >= 0 && projectileIndex < Projectiles.Count);

                return Projectiles[projectileIndex];
            }
        }

        // =============================================================================================================
        // OTAPI hooks
        //

        private OTAPI.HookResult PreSetDefaultsByIdHandler(
                Terraria.Projectile terrariaProjectile, ref int projectileId) {
            Debug.Assert(terrariaProjectile != null);

            var projectile = GetProjectile(terrariaProjectile);
            var evt = new ProjectileDefaultsEvent(projectile) { Id = (ProjectileId)projectileId };
            Kernel.Raise(evt, Log);
            if (evt.IsCanceled) {
                return OTAPI.HookResult.Cancel;
            }

            projectileId = (int)evt.Id;
            return OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult PreUpdateHandler(Terraria.Projectile terrariaProjectile, ref int projectileIndex) {
            Debug.Assert(projectileIndex >= 0 && projectileIndex < Projectiles.Count);

            var evt = new ProjectileTickEvent(Projectiles[projectileIndex]);
            Kernel.Raise(evt, Log);
            return evt.IsCanceled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        // Gets an `IProjectile` which corresponds to the given Terraria projectile. Retrieves the `IProjectile` from
        // the `Projectiles` array, if possible.
        private IProjectile GetProjectile(Terraria.Projectile terrariaProjectile) {
            Debug.Assert(terrariaProjectile != null);

            var projectileIndex = terrariaProjectile.whoAmI;
            Debug.Assert(projectileIndex >= 0 && projectileIndex < Projectiles.Count);

            var isConcrete = terrariaProjectile == Terraria.Main.projectile[projectileIndex];
            return isConcrete ? Projectiles[projectileIndex] : new OrionProjectile(terrariaProjectile);
        }
    }
}
