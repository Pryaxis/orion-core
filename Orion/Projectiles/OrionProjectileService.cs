using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Orion.Framework;
using Orion.Projectiles.Events;
using OTAPI;

namespace Orion.Projectiles {
    /// <summary>
    /// Orion's implementation of <see cref="IProjectileService"/>.
    /// </summary>
    internal sealed class OrionProjectileService : OrionService, IProjectileService {
        private readonly IProjectile[] _projectiles;

        public override string Author => "Pryaxis";
        public override string Name => "Orion Projectile Service";

        public int Count => Terraria.Main.maxProjectiles;

        public IProjectile this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                if (_projectiles[index]?.WrappedProjectile != Terraria.Main.projectile[index]) {
                    _projectiles[index] = new OrionProjectile(Terraria.Main.projectile[index]);
                }

                var projectile = _projectiles[index];
                Debug.Assert(projectile != null, $"{nameof(projectile)} should not be null.");
                Debug.Assert(projectile.WrappedProjectile != null, 
                             $"{nameof(projectile.WrappedProjectile)} should not be null.");
                return projectile;
            }
        }
        
        public event EventHandler<ProjectileSettingDefaultsEventArgs> ProjectileSettingDefaults;
        public event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;
        public event EventHandler<ProjectileUpdatingEventArgs> ProjectileUpdating;
        public event EventHandler<ProjectileUpdatingEventArgs> ProjectileUpdatingAi;
        public event EventHandler<ProjectileUpdatedEventArgs> ProjectileUpdatedAi;
        public event EventHandler<ProjectileUpdatedEventArgs> ProjectileUpdated;
        public event EventHandler<ProjectileRemovingEventArgs> ProjectileRemoving;
        public event EventHandler<ProjectileRemovedEventArgs> ProjectileRemoved;

        public OrionProjectileService() {
            _projectiles = new IProjectile[Terraria.Main.maxProjectiles];

            Hooks.Projectile.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Projectile.PostSetDefaultsById = PostSetDefaultsByIdHandler;
            Hooks.Projectile.PreUpdate = PreUpdateHandler;
            Hooks.Projectile.PreAI = PreAiHandler;
            Hooks.Projectile.PostAI = PostAiHandler;
            Hooks.Projectile.PostUpdate = PostUpdateHandler;
            Hooks.Projectile.PreKill = PreKillHandler;
            Hooks.Projectile.PostKilled = PostKilledHandler;
        }

        public IEnumerator<IProjectile> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IProjectile SpawnProjectile(ProjectileType type, Vector2 position, Vector2 velocity, int damage, float knockback,
                                           float[] aiValues = null) {
            if (aiValues != null && aiValues.Length != 2) {
                throw new ArgumentException($"{nameof(aiValues)} must have length 2.", nameof(aiValues));
            }

            var ai0 = aiValues?[0] ?? 0;
            var ai1 = aiValues?[1] ?? 0;
            var projectileIndex = Terraria.Projectile.NewProjectile(position, velocity, (int)type, damage, knockback,
                                                                    255, ai0, ai1);

            Debug.Assert(projectileIndex >= 0 && projectileIndex < Count,
                         $"{nameof(projectileIndex)} should be a valid index.");

            return this[projectileIndex];
        }


        private HookResult PreSetDefaultsByIdHandler(Terraria.Projectile terrariaProjectile, ref int type) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new ProjectileSettingDefaultsEventArgs(projectile, (ProjectileType)type);
            ProjectileSettingDefaults?.Invoke(this, args);

            type = (int)args.Type;
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostSetDefaultsByIdHandler(Terraria.Projectile terrariaProjectile, int type) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new ProjectileSetDefaultsEventArgs(projectile);
            ProjectileSetDefaults?.Invoke(this, args);
        }

        private HookResult PreUpdateHandler(Terraria.Projectile terrariaProjectile, ref int projectileIndex) {
            Debug.Assert(projectileIndex >= 0 && projectileIndex < Count,
                         $"{nameof(projectileIndex)} must be a valid index.");

            var args = new ProjectileUpdatingEventArgs(this[projectileIndex]);
            ProjectileUpdating?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreAiHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new ProjectileUpdatingEventArgs(projectile);
            ProjectileUpdatingAi?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostAiHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new ProjectileUpdatedEventArgs(projectile);
            ProjectileUpdatedAi?.Invoke(this, args);
        }

        private void PostUpdateHandler(Terraria.Projectile terrariaProjectile, int projectileIndex) {
            Debug.Assert(projectileIndex >= 0 && projectileIndex < Count,
                         $"{nameof(projectileIndex)} must be a valid index.");

            var args = new ProjectileUpdatedEventArgs(this[projectileIndex]);
            ProjectileUpdated?.Invoke(this, args);
        }

        private HookResult PreKillHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new ProjectileRemovingEventArgs(projectile);
            ProjectileRemoving?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostKilledHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new ProjectileRemovedEventArgs(projectile);
            ProjectileRemoved?.Invoke(this, args);
        }
    }
}
