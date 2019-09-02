using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Orion.Framework;
using Orion.Projectiles.Events;
using OTAPI;

namespace Orion.Projectiles {
    /// <summary>
    /// Orion's implementation of <see cref="IProjectileService"/>.
    /// </summary>
    internal sealed class OrionProjectileService : OrionService, IProjectileService {
        private readonly IProjectile[] _projectiles;

        /// <inheritdoc />
        public override string Author => "Pryaxis";

        /// <inheritdoc />
        public override string Name => "Orion Projectile Service";

        /// <inheritdoc />
        public int Count => Terraria.Main.maxProjectiles;

        /// <inheritdoc />
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
        
        /// <inheritdoc />
        public event EventHandler<ProjectileSettingDefaultsEventArgs> ProjectileSettingDefaults;
        
        /// <inheritdoc />
        public event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;
        
        /// <inheritdoc />
        public event EventHandler<ProjectileUpdatingEventArgs> ProjectileUpdating;
        
        /// <inheritdoc />
        public event EventHandler<ProjectileUpdatingEventArgs> ProjectileUpdatingAi;
        
        /// <inheritdoc />
        public event EventHandler<ProjectileUpdatedEventArgs> ProjectileUpdatedAi;
        
        /// <inheritdoc />
        public event EventHandler<ProjectileUpdatedEventArgs> ProjectileUpdated;
        
        /// <inheritdoc />
        public event EventHandler<ProjectileRemovingEventArgs> ProjectileRemoving;
        
        /// <inheritdoc />
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

        /// <inheritdoc />
        public IEnumerator<IProjectile> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private HookResult PreSetDefaultsByIdHandler(Terraria.Projectile terrariaProjectile, ref int type) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var settingDefaultsArgs = new ProjectileSettingDefaultsEventArgs(projectile, (ProjectileType)type);
            ProjectileSettingDefaults?.Invoke(this, settingDefaultsArgs);

            type = (int)settingDefaultsArgs.Type;
            return settingDefaultsArgs.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostSetDefaultsByIdHandler(Terraria.Projectile terrariaProjectile, int type) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var setDefaultsArgs = new ProjectileSetDefaultsEventArgs(projectile);
            ProjectileSetDefaults?.Invoke(this, setDefaultsArgs);
        }

        private HookResult PreUpdateHandler(Terraria.Projectile terrariaProjectile, ref int index) {
            Debug.Assert(index >= 0 && index < Count, $"{nameof(index)} must be a valid index.");

            var updatingArgs = new ProjectileUpdatingEventArgs(this[index]);
            ProjectileUpdating?.Invoke(this, updatingArgs);

            return updatingArgs.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreAiHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var updatingArgs = new ProjectileUpdatingEventArgs(projectile);
            ProjectileUpdatingAi?.Invoke(this, updatingArgs);

            return updatingArgs.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostAiHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var updatedArgs = new ProjectileUpdatedEventArgs(projectile);
            ProjectileUpdatedAi?.Invoke(this, updatedArgs);
        }

        private void PostUpdateHandler(Terraria.Projectile terrariaProjectile, int index) {
            Debug.Assert(index >= 0 && index < Count, $"{nameof(index)} must be a valid index.");

            var updatedArgs = new ProjectileUpdatedEventArgs(this[index]);
            ProjectileUpdated?.Invoke(this, updatedArgs);
        }

        private HookResult PreKillHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var removingArgs = new ProjectileRemovingEventArgs(projectile);
            ProjectileRemoving?.Invoke(this, removingArgs);

            return removingArgs.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostKilledHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var removedArgs = new ProjectileRemovedEventArgs(projectile);
            ProjectileRemoved?.Invoke(this, removedArgs);
        }
    }
}
