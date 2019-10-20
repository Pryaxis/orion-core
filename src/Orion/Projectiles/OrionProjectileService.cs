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
using Orion.Events.Projectiles;
using Orion.Utils;
using OTAPI;
using Serilog;
using Main = Terraria.Main;
using TerrariaProjectile = Terraria.Projectile;

namespace Orion.Projectiles {
    [Service("orion-projs")]
    internal sealed class OrionProjectileService : OrionService, IProjectileService {
        public IReadOnlyArray<IProjectile> Projectiles { get; }
        public EventHandlerCollection<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults { get; }
        public EventHandlerCollection<ProjectileUpdateEventArgs> ProjectileUpdate { get; }
        public EventHandlerCollection<ProjectileRemoveEventArgs> ProjectileRemove { get; }

        public OrionProjectileService(ILogger log) : base(log) {
            Debug.Assert(log != null, "log should not be null");
            Debug.Assert(Main.projectile != null, "Terraria projectiles should not be null");

            // Ignore the last projectile since it is used as a failure slot.
            Projectiles = new WrappedReadOnlyArray<OrionProjectile, TerrariaProjectile>(
                Main.projectile.AsMemory(..^1),
                (projectileIndex, terrariaProjectile) => new OrionProjectile(projectileIndex, terrariaProjectile));

            ProjectileSetDefaults = new EventHandlerCollection<ProjectileSetDefaultsEventArgs>();
            ProjectileUpdate = new EventHandlerCollection<ProjectileUpdateEventArgs>();
            ProjectileRemove = new EventHandlerCollection<ProjectileRemoveEventArgs>();

            Hooks.Projectile.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Projectile.PreUpdate = PreUpdateHandler;
            Hooks.Projectile.PreKill = PreKillHandler;
        }

        public override void Dispose() {
            Hooks.Projectile.PreSetDefaultsById = null;
            Hooks.Projectile.PreUpdate = null;
            Hooks.Projectile.PreKill = null;
        }

        public IProjectile? SpawnProjectile(
                ProjectileType type, Vector2 position, Vector2 velocity, int damage, float knockback,
                float[]? aiValues = null) {
            if (aiValues != null && aiValues.Length != TerrariaProjectile.maxAI) {
                throw new ArgumentException(
                    $"Array does not have length {TerrariaProjectile.maxAI}.", nameof(aiValues));
            }

            LogSpawnProjectile(type, position);

            var ai0 = aiValues?[0] ?? 0;
            var ai1 = aiValues?[1] ?? 0;
            var projectileIndex = TerrariaProjectile.NewProjectile(position, velocity, (int)type,
                damage, knockback, 255, ai0, ai1);
            return projectileIndex >= 0 && projectileIndex < Projectiles.Count ? Projectiles[projectileIndex] : null;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogSpawnProjectile(ProjectileType type, Vector2 position) =>
            // Not localized because this string is developer-facing.
            Log.Debug("Spawning {ProjectileType} at {Position}", type, position);

        private IProjectile GetProjectile(TerrariaProjectile terrariaProjectile) {
            Debug.Assert(terrariaProjectile.whoAmI >= 0 && terrariaProjectile.whoAmI < Projectiles.Count,
                "Terraria projectile should have a valid index");

            // We want to retrieve the world projectile if this projectile is real. Otherwise, return a "fake"
            // projectile.
            return terrariaProjectile == Main.projectile[terrariaProjectile.whoAmI]
                ? Projectiles[terrariaProjectile.whoAmI]
                : new OrionProjectile(terrariaProjectile);
        }

        // =============================================================================================================
        // Handling ProjectileSetDefaults
        // =============================================================================================================

        private HookResult PreSetDefaultsByIdHandler(TerrariaProjectile terrariaProjectile, ref int projectileType_) {
            Debug.Assert(terrariaProjectile != null, "Terraria projectile should not be null");

            var projectile = GetProjectile(terrariaProjectile);
            var projectileType = (ProjectileType)projectileType_;
            var args = new ProjectileSetDefaultsEventArgs(projectile, projectileType);

            LogProjectileSetDefaults_Before(args);
            ProjectileSetDefaults.Invoke(this, args);
            LogProjectileSetDefaults_After(args);

            if (args.IsCanceled()) {
                return HookResult.Cancel;
            } else if (args.IsDirty) {
                projectileType_ = (int)args.ProjectileType;
            }

            return HookResult.Continue;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogProjectileSetDefaults_Before(ProjectileSetDefaultsEventArgs args) =>
            // Not localized because this string is developer-facing.
            Log.Verbose(
                "Invoking {Event} with [{Projectile}, {ProjectileType}]",
                ProjectileSetDefaults, args.Projectile, args.ProjectileType);

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogProjectileSetDefaults_After(ProjectileSetDefaultsEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Verbose(
                    "Canceled {Event} for {CancellationReason}",
                    ProjectileSetDefaults, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Verbose(
                    "Altered {Event} to [{Projectile}, {ProjectileType}]",
                    ProjectileSetDefaults, args.Projectile, args.ProjectileType);
            }
        }

        // =============================================================================================================
        // Handling ProjectileUpdate
        // =============================================================================================================

        private HookResult PreUpdateHandler(TerrariaProjectile _, ref int projectileIndex) {
            Debug.Assert(projectileIndex >= 0 && projectileIndex < Projectiles.Count,
                "projectile index should be valid");

            var projectile = Projectiles[projectileIndex];
            var args = new ProjectileUpdateEventArgs(projectile);

            LogProjectileUpdate_Before(args);
            ProjectileUpdate.Invoke(this, args);
            LogProjectileUpdate_After(args);

            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogProjectileUpdate_Before(ProjectileUpdateEventArgs args) =>
            // Not localized because this string is developer-facing.
            Log.Verbose(
                "Invoking {Event} with [{Projectile}, {ProjectileType}]",
                ProjectileUpdate, args.Projectile, args.Projectile.Type);

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogProjectileUpdate_After(ProjectileUpdateEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Verbose("Canceled {Event} for {CancellationReason}", ProjectileUpdate, args.CancellationReason);
            }
        }

        // =============================================================================================================
        // Handling ProjectileRemove
        // =============================================================================================================

        private HookResult PreKillHandler(TerrariaProjectile terrariaProjectile) {
            Debug.Assert(terrariaProjectile != null, "Terraria projectile should not be null");

            var projectile = GetProjectile(terrariaProjectile);
            var args = new ProjectileRemoveEventArgs(projectile);

            LogProjectileRemove_Before(args);
            ProjectileRemove.Invoke(this, args);
            LogProjectileRemove_After(args);

            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogProjectileRemove_Before(ProjectileRemoveEventArgs args) =>
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Projectile}, {ProjectileType}]",
                ProjectileRemove, args.Projectile, args.Projectile.Type);

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogProjectileRemove_After(ProjectileRemoveEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {CancellationReason}", ProjectileRemove, args.CancellationReason);
            }
        }
    }
}
