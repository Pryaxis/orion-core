using System;
using System.Collections.Generic;
using System.Linq;
using Orion.Core;
using Orion.Events.Projectile;
using Orion.Framework;
using Orion.Interfaces;
using OTAPI.Core;

namespace Orion.Services
{
	/// <summary>
	/// Implements the <see cref="IProjectileService"/> functionality.
	/// </summary>
	public class ProjectileService : ServiceBase, IProjectileService
	{
		private bool _disposed;
		private readonly IProjectile[] _projectiles;

		/// <summary>
		/// Occurs after a <see cref="IProjectile"/> has it defaults set.
		/// </summary>
		public event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;

		/// <summary>
		/// Occurs before a <see cref="IProjectile"/> has its defaults set.
		/// </summary>
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

		/// <summary>
		/// Finds all <see cref="IProjectile"/>s in the world matching a predicate. 
		/// </summary>
		/// <param name="predicate">The predicate to match with, or null for none.</param>
		/// <returns>An enumerable collection of <see cref="IProjectile"/>s matching the predicate.</returns>
		public IEnumerable<IProjectile> Find(Predicate<IProjectile> predicate = null)
		{
			var projectiles = new List<IProjectile>();
			for (int i = 0; i < _projectiles.Length; i++)
			{
				if (_projectiles[i]?.WrappedProjectile != Terraria.Main.projectile[i])
				{
					_projectiles[i] = new Projectile(Terraria.Main.projectile[i]);
				}
				projectiles.Add(_projectiles[i]);
			}
			return projectiles.Where(p => p.WrappedProjectile.active && (predicate?.Invoke(p) ?? true));
		}

		/// <summary>
		/// Disposes the service and its unmanaged resources, if any, optionally disposing its managed resources, if
		/// any.
		/// </summary>
		/// <param name="disposing">
		/// true to dispose managed and unmanaged resources, false to only dispose unmanaged resources.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Hooks.Projectile.PostSetDefaultsById = null;
					Hooks.Projectile.PreSetDefaultsById = null;
				}
				_disposed = true;
			}
			base.Dispose(disposing);
		}

		private void InvokeProjectileSetDefaults(Terraria.Projectile terrariaProjectile, int type)
		{
			var projectile = new Projectile(terrariaProjectile);
			var args = new ProjectileSetDefaultsEventArgs(projectile, type);
			ProjectileSetDefaults?.Invoke(this, args);
		}

		private HookResult InvokeProjectileSettingDefaults(Terraria.Projectile terrariaProjectile, ref int type)
		{
			var projectile = new Projectile(terrariaProjectile);
			var args = new ProjectileSettingDefaultsEventArgs(projectile, type);
			ProjectileSettingDefaults?.Invoke(this, args);
			type = args.Type;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
