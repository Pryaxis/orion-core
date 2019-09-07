using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Orion.Projectiles.Events;

namespace Orion.Projectiles {
    internal sealed class OrionProjectileService : OrionService, IProjectileService {
        private readonly IList<Terraria.Projectile> _terrariaProjectiles;
        private readonly IList<OrionProjectile> _projectiles;
        
        [ExcludeFromCodeCoverage]
        public override string Author => "Pryaxis";
        
        [ExcludeFromCodeCoverage]
        public override string Name => "Orion Projectile Service";

        /*
         * We need to subtract 1 from the count. This is because Terraria actually has an extra slot which is reserved
         * as a failure index.
         */
        public int Count => _projectiles.Count - 1;

        public IProjectile this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                if (_projectiles[index]?.Wrapped != _terrariaProjectiles[index]) {
                    _projectiles[index] = new OrionProjectile(_terrariaProjectiles[index]);
                }

                var projectile = _projectiles[index];
                Debug.Assert(projectile != null, $"{nameof(projectile)} should not be null.");
                Debug.Assert(projectile.Wrapped != null, 
                             $"{nameof(projectile.Wrapped)} should not be null.");
                return projectile;
            }
        }
        
        public event EventHandler<SettingProjectileDefaultsEventArgs> SettingProjectileDefaults;
        public event EventHandler<SetProjectileDefaultsEventArgs> SetProjectileDefaults;
        public event EventHandler<UpdatingProjectileEventArgs> UpdatingProjectile;
        public event EventHandler<UpdatingProjectileEventArgs> UpdatingProjectileAi;
        public event EventHandler<UpdatedProjectileEventArgs> UpdatedProjectileAi;
        public event EventHandler<UpdatedProjectileEventArgs> UpdatedProjectile;
        public event EventHandler<RemovingProjectileEventArgs> RemovingProjectile;
        public event EventHandler<RemovedProjectileEventArgs> RemovedProjectile;

        public OrionProjectileService() {
            _terrariaProjectiles = Terraria.Main.projectile;
            _projectiles = new OrionProjectile[_terrariaProjectiles.Count];

            OTAPI.Hooks.Projectile.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            OTAPI.Hooks.Projectile.PostSetDefaultsById = PostSetDefaultsByIdHandler;
            OTAPI.Hooks.Projectile.PreUpdate = PreUpdateHandler;
            OTAPI.Hooks.Projectile.PreAI = PreAiHandler;
            OTAPI.Hooks.Projectile.PostAI = PostAiHandler;
            OTAPI.Hooks.Projectile.PostUpdate = PostUpdateHandler;
            OTAPI.Hooks.Projectile.PreKill = PreKillHandler;
            OTAPI.Hooks.Projectile.PostKilled = PostKilledHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) {
                return;
            }

            OTAPI.Hooks.Projectile.PreSetDefaultsById = null;
            OTAPI.Hooks.Projectile.PostSetDefaultsById = null;
            OTAPI.Hooks.Projectile.PreUpdate = null;
            OTAPI.Hooks.Projectile.PreAI = null;
            OTAPI.Hooks.Projectile.PostAI = null;
            OTAPI.Hooks.Projectile.PostUpdate = null;
            OTAPI.Hooks.Projectile.PreKill = null;
            OTAPI.Hooks.Projectile.PostKilled = null;
        }

        public IEnumerator<IProjectile> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }
        
        [ExcludeFromCodeCoverage]
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


        private OTAPI.HookResult PreSetDefaultsByIdHandler(Terraria.Projectile terrariaProjectile, ref int type) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new SettingProjectileDefaultsEventArgs(projectile, (ProjectileType)type);
            SettingProjectileDefaults?.Invoke(this, args);

            type = (int)args.Type;
            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private void PostSetDefaultsByIdHandler(Terraria.Projectile terrariaProjectile, int type) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new SetProjectileDefaultsEventArgs(projectile);
            SetProjectileDefaults?.Invoke(this, args);
        }

        private OTAPI.HookResult PreUpdateHandler(Terraria.Projectile terrariaProjectile, ref int projectileIndex) {
            Debug.Assert(projectileIndex >= 0 && projectileIndex < Count,
                         $"{nameof(projectileIndex)} must be a valid index.");

            var args = new UpdatingProjectileEventArgs(this[projectileIndex]);
            UpdatingProjectile?.Invoke(this, args);

            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult PreAiHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new UpdatingProjectileEventArgs(projectile);
            UpdatingProjectileAi?.Invoke(this, args);

            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private void PostAiHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new UpdatedProjectileEventArgs(projectile);
            UpdatedProjectileAi?.Invoke(this, args);
        }

        private void PostUpdateHandler(Terraria.Projectile terrariaProjectile, int projectileIndex) {
            Debug.Assert(projectileIndex >= 0 && projectileIndex < Count,
                         $"{nameof(projectileIndex)} must be a valid index.");

            var args = new UpdatedProjectileEventArgs(this[projectileIndex]);
            UpdatedProjectile?.Invoke(this, args);
        }

        private OTAPI.HookResult PreKillHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new RemovingProjectileEventArgs(projectile);
            RemovingProjectile?.Invoke(this, args);

            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private void PostKilledHandler(Terraria.Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new RemovedProjectileEventArgs(projectile);
            RemovedProjectile?.Invoke(this, args);
        }
    }
}
