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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Orion.Hooks;
using Orion.Projectiles.Events;
using OTAPI;
using Terraria;

namespace Orion.Projectiles {
    internal sealed class OrionProjectileService : OrionService, IProjectileService {
        private readonly IList<Projectile> _terrariaProjectiles;
        private readonly IList<OrionProjectile> _projectiles;

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        // We need to subtract 1 from the count. This is because Terraria actually has an extra slot which is reserved
        // as a failure index.
        public int Count => _projectiles.Count - 1;

        public IProjectile this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

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

        public HookHandlerCollection<SettingProjectileDefaultsEventArgs> SettingProjectileDefaults { get; set; }
        public HookHandlerCollection<SetProjectileDefaultsEventArgs> SetProjectileDefaults { get; set; }
        public HookHandlerCollection<UpdatingProjectileEventArgs> UpdatingProjectile { get; set; }
        public HookHandlerCollection<UpdatingProjectileEventArgs> UpdatingProjectileAi { get; set; }
        public HookHandlerCollection<UpdatedProjectileEventArgs> UpdatedProjectileAi { get; set; }
        public HookHandlerCollection<UpdatedProjectileEventArgs> UpdatedProjectile { get; set; }
        public HookHandlerCollection<RemovingProjectileEventArgs> RemovingProjectile { get; set; }
        public HookHandlerCollection<RemovedProjectileEventArgs> RemovedProjectile { get; set; }

        public OrionProjectileService() {
            _terrariaProjectiles = Main.projectile;
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
            if (!disposeManaged) return;

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

        public IProjectile SpawnProjectile(ProjectileType type, Vector2 position, Vector2 velocity, int damage,
                                           float knockback, float[] aiValues = null) {
            if (aiValues != null && aiValues.Length != Projectile.maxAI) {
                throw new ArgumentException($"{nameof(aiValues)} must have length {Projectile.maxAI}.",
                                            nameof(aiValues));
            }

            var ai0 = aiValues?[0] ?? 0;
            var ai1 = aiValues?[1] ?? 0;
            var projectileIndex = Projectile.NewProjectile(position, velocity, (int)type, damage, knockback,
                                                                    255, ai0, ai1);
            return projectileIndex >= 0 && projectileIndex < Count ? this[projectileIndex] : null;
        }


        private HookResult PreSetDefaultsByIdHandler(Projectile terrariaProjectile, ref int type) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new SettingProjectileDefaultsEventArgs(projectile, (ProjectileType)type);
            SettingProjectileDefaults?.Invoke(this, args);

            type = (int)args.Type;
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostSetDefaultsByIdHandler(Projectile terrariaProjectile, int type) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new SetProjectileDefaultsEventArgs(projectile);
            SetProjectileDefaults?.Invoke(this, args);
        }

        private HookResult PreUpdateHandler(Projectile terrariaProjectile, ref int projectileIndex) {
            Debug.Assert(projectileIndex >= 0 && projectileIndex < Count,
                         $"{nameof(projectileIndex)} must be a valid index.");

            var args = new UpdatingProjectileEventArgs(this[projectileIndex]);
            UpdatingProjectile?.Invoke(this, args);
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreAiHandler(Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new UpdatingProjectileEventArgs(projectile);
            UpdatingProjectileAi?.Invoke(this, args);
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostAiHandler(Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new UpdatedProjectileEventArgs(projectile);
            UpdatedProjectileAi?.Invoke(this, args);
        }

        private void PostUpdateHandler(Projectile terrariaProjectile, int projectileIndex) {
            Debug.Assert(projectileIndex >= 0 && projectileIndex < Count,
                         $"{nameof(projectileIndex)} must be a valid index.");

            var args = new UpdatedProjectileEventArgs(this[projectileIndex]);
            UpdatedProjectile?.Invoke(this, args);
        }

        private HookResult PreKillHandler(Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new RemovingProjectileEventArgs(projectile);
            RemovingProjectile?.Invoke(this, args);
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostKilledHandler(Projectile terrariaProjectile) {
            var projectile = new OrionProjectile(terrariaProjectile);
            var args = new RemovedProjectileEventArgs(projectile);
            RemovedProjectile?.Invoke(this, args);
        }
    }
}
