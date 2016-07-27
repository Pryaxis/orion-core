using System;
using System.Collections.Generic;
using System.Linq;
using Orion.Core;
using Orion.Events.Projectile;
using Orion.Framework;
using OTAPI.Core;

namespace Orion.Services
{
	/// <summary>
	/// Manages <see cref="IProjectile"/>s.
	/// </summary>
	[Service("Projectile Service", Author = "Nyx Studios")]
	public class ProjectileService : ServiceBase, IProjectileService
	{
		private bool _disposed;
		private readonly IProjectile[] _projectiles;

		/// <inheritdoc/>
		public event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;

		/// <inheritdoc/>
		public event EventHandler<ProjectileSettingDefaultsEventArgs> ProjectileSettingDefaults;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		/// <remarks>
		/// This constructor registers the OTAPI hooks.
		/// </remarks>
		public ProjectileService(Orion orion) : base(orion)
		{
			_projectiles = new IProjectile[Terraria.Main.projectile.Length];
			Hooks.Projectile.PostSetDefaultsById = InvokeProjectileSetDefaults;
			Hooks.Projectile.PreSetDefaultsById = InvokeProjectileSettingDefaults;
		}

		/// <inheritdoc/>
		/// <remarks>
		/// The <see cref="IProjectile"/>s are cached in an array. Calling this method multiple times will result in the
		/// same <see cref="IProjectile"/> references as long as the Terraria projectile array is not updated.
		/// </remarks>
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

		/// <inheritdoc/>
		/// <remarks>
		/// This method deregisters the OTAPI hooks.
		/// </remarks>
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
			var args = new ProjectileSetDefaultsEventArgs(projectile);
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
