using System;
using System.Collections.Generic;
using System.Linq;
using Orion.Framework;
using Orion.Projectiles.Events;
using OTAPI.Core;

namespace Orion.Projectiles
{
	/// <summary>
	/// Manages <see cref="IProjectile"/> instances.
	/// </summary>
	[Service("Projectile Service", Author = "Nyx Studios")]
	public class ProjectileService : SharedService, IProjectileService
	{
		private readonly IProjectile[] _projectiles;
		private bool _disposed;

		/// <inheritdoc/>
		public event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;

		/// <inheritdoc/>
		public event EventHandler<ProjectileSettingDefaultsEventArgs> ProjectileSettingDefaults;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public ProjectileService(Orion orion) : base(orion)
		{
			_projectiles = new IProjectile[Terraria.Main.projectile.Length];
			Hooks.Projectile.PostSetDefaultsById = InvokeProjectileSetDefaults;
			Hooks.Projectile.PreSetDefaultsById = InvokeProjectileSettingDefaults;
		}

		/// <inheritdoc/>
		/// <remarks>
		/// The <see cref="IProjectile"/> instances are cached in an array. Calling this method multiple times will
		/// result in the same <see cref="IProjectile"/> instances as long as Terraria's projectile array remains
		/// unchanged.
		/// </remarks>
		public IEnumerable<IProjectile> FindProjectiles(Predicate<IProjectile> predicate = null)
		{
			var projectiles = new List<IProjectile>();
			for (var i = 0; i < _projectiles.Length; i++)
			{
				if (_projectiles[i]?.WrappedProjectile != Terraria.Main.projectile[i])
				{
					_projectiles[i] = new Projectile(Terraria.Main.projectile[i]);
				}
				projectiles.Add(_projectiles[i]);
			}
			return projectiles.Where(p => p.WrappedProjectile.active && (predicate?.Invoke(p) ?? true));
		}

		private void InvokeProjectileSetDefaults(Terraria.Projectile terrariaProjectile, int type)
		{
			var projectile = new Projectile(terrariaProjectile);
			var args = new ProjectileSetDefaultsEventArgs(projectile);
			ProjectileSetDefaults?.Invoke(this, args);
		}

		private HookResult InvokeProjectileSettingDefaults(Terraria.Projectile terrariaProjectile, ref int type)
		{
			var projectile = new Projectile(terrariaProjectile);
			var args = new ProjectileSettingDefaultsEventArgs(projectile, (ProjectileType)type);
			ProjectileSettingDefaults?.Invoke(this, args);
			type = (int)args.Type;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
