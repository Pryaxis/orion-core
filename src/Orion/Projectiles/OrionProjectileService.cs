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

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Orion.Events;
using Orion.Events.Extensions;
using Orion.Events.Projectiles;
using Orion.Utils;
using OTAPI;
using Main = Terraria.Main;
using TerrariaProjectile = Terraria.Projectile;

namespace Orion.Projectiles {
    internal sealed class OrionProjectileService : OrionService, IProjectileService {
        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        public IReadOnlyArray<IProjectile> Projectiles { get; }
        public EventHandlerCollection<ProjectileSetDefaultsEventArgs>? ProjectileSetDefaults { get; set; }
        public EventHandlerCollection<ProjectileUpdateEventArgs>? ProjectileUpdate { get; set; }
        public EventHandlerCollection<ProjectileRemoveEventArgs>? ProjectileRemove { get; set; }

        public OrionProjectileService() {
            // Ignore the last projectile since it is used as a failure slot.
            Projectiles = new WrappedReadOnlyArray<OrionProjectile, TerrariaProjectile>(
                Main.projectile.AsMemory(..^1),
                (_, terrariaProjectile) => new OrionProjectile(terrariaProjectile));

            Hooks.Projectile.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Projectile.PreUpdate = PreUpdateHandler;
            Hooks.Projectile.PreKill = PreKillHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) return;

            Hooks.Projectile.PreSetDefaultsById = null;
            Hooks.Projectile.PreUpdate = null;
            Hooks.Projectile.PreKill = null;
        }

        public IProjectile? SpawnProjectile(ProjectileType projectileType, Vector2 position, Vector2 velocity,
                                            int damage, float knockback, float[]? aiValues = null) {
            if (aiValues != null && aiValues.Length != TerrariaProjectile.maxAI) {
                throw new ArgumentException($"Array does not have length {TerrariaProjectile.maxAI}.",
                                            nameof(aiValues));
            }

            var ai0 = aiValues?[0] ?? 0;
            var ai1 = aiValues?[1] ?? 0;
            var projectileIndex =
                TerrariaProjectile.NewProjectile(position, velocity, (int)projectileType, damage, knockback, 255, ai0,
                                                 ai1);
            return projectileIndex >= 0 && projectileIndex < Projectiles.Count ? Projectiles[projectileIndex] : null;
        }

        private HookResult PreSetDefaultsByIdHandler(TerrariaProjectile terrariaProjectile, ref int projectileType) {
            Debug.Assert(terrariaProjectile != null, "terrariaProjectile != null");

            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new ProjectileSetDefaultsEventArgs(projectile, (ProjectileType)projectileType);
            ProjectileSetDefaults?.Invoke(this, args);
            if (args.IsCanceled()) return HookResult.Cancel;

            projectileType = (int)args.ProjectileType;
            return HookResult.Continue;
        }

        private HookResult PreUpdateHandler(TerrariaProjectile terrariaProjectile, ref int projectileIndex) {
            Debug.Assert(terrariaProjectile != null, "terrariaProjectile != null");

            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new ProjectileUpdateEventArgs(projectile);
            ProjectileUpdate?.Invoke(this, args);
            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreKillHandler(TerrariaProjectile terrariaProjectile) {
            Debug.Assert(terrariaProjectile != null, "terrariaProjectile != null");

            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new ProjectileRemoveEventArgs(projectile);
            ProjectileRemove?.Invoke(this, args);
            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }
    }
}
