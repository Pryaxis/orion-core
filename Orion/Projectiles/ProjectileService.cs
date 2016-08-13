using System;
using System.Collections.Generic;
using System.Linq;
using Orion.Framework;
using Orion.Projectiles.Events;
using OTAPI;

namespace Orion.Projectiles
{
	/// <summary>
	/// Manages projectiles;
	/// </summary>
	[Service("Projectile Service", Author = "Nyx Studios")]
	public class ProjectileService : SharedService, IProjectileService
	{
		private readonly IProjectile[] _projectiles;

		/// <inheritdoc/>
		public event EventHandler<ProjectileKilledEventArgs> ProjectileKilled;

		/// <inheritdoc/>
		public event EventHandler<ProjectileKillingEventArgs> ProjectileKilling;

		/// <inheritdoc/>
		public event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;

		/// <inheritdoc/>
		public event EventHandler<ProjectileSettingDefaultsEventArgs> ProjectileSettingDefaults;

		/// <inheritdoc/>
		public event EventHandler<ProjectileUpdatedEventArgs> ProjectileUpdated;

		/// <inheritdoc/>
		public event EventHandler<ProjectileUpdatedAIEventArgs> ProjectileUpdatedAI;

		/// <inheritdoc/>
		public event EventHandler<ProjectileUpdatingEventArgs> ProjectileUpdating;

		/// <inheritdoc/>
		public event EventHandler<ProjectileUpdatingAIEventArgs> ProjectileUpdatingAI;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public ProjectileService(Orion orion) : base(orion)
		{
			_projectiles = new IProjectile[Terraria.Main.projectile.Length];
			Hooks.Projectile.PostKilled = InvokeProjectileKilled;
			Hooks.Projectile.PreKill = InvokeProjectileKilling;
			Hooks.Projectile.PostSetDefaultsById = InvokeProjectileSetDefaults;
			Hooks.Projectile.PreSetDefaultsById = InvokeProjectileSettingDefaults;
			Hooks.Projectile.PostUpdate = InvokeProjectileUpdated;
			Hooks.Projectile.PostAI = InvokeProjectileUpdatedAI;
			Hooks.Projectile.PreUpdate = InvokeProjectileUpdating;
			Hooks.Projectile.PreAI = InvokeProjectileUpdatingAI;
		}

		/// <inheritdoc/>
		/// <remarks>
		/// The projectiles are cached in an array. Calling this method multiple times will result in the same
		/// instances as long as Terraria's projectile array remains unchanged.
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

		private void InvokeProjectileKilled(Terraria.Projectile terrariaProjectile)
		{
			var projectile = new Projectile(terrariaProjectile);
			var args = new ProjectileKilledEventArgs(projectile);
			ProjectileKilled?.Invoke(this, args);
		}

		private HookResult InvokeProjectileKilling(Terraria.Projectile terrariaProjectile)
		{
			var projectile = new Projectile(terrariaProjectile);
			var args = new ProjectileKillingEventArgs(projectile);
			ProjectileKilling?.Invoke(this, args);
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
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

		private void InvokeProjectileUpdated(Terraria.Projectile terrariaProjectile, int index)
		{
			var projectile = new Projectile(terrariaProjectile);
			var args = new ProjectileUpdatedEventArgs(projectile);
			ProjectileUpdated?.Invoke(this, args);
		}

		private void InvokeProjectileUpdatedAI(Terraria.Projectile terrariaProjectile)
		{
			var projectile = new Projectile(terrariaProjectile);
			var args = new ProjectileUpdatedAIEventArgs(projectile);
			ProjectileUpdatedAI?.Invoke(this, args);
		}

		private HookResult InvokeProjectileUpdating(Terraria.Projectile terrariaProjectile, ref int index)
		{
			var projectile = new Projectile(terrariaProjectile);
			var args = new ProjectileUpdatingEventArgs(projectile);
			ProjectileUpdating?.Invoke(this, args);
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		private HookResult InvokeProjectileUpdatingAI(Terraria.Projectile terrariaProjectile)
		{
			var projectile = new Projectile(terrariaProjectile);
			var args = new ProjectileUpdatingAIEventArgs(projectile);
			ProjectileUpdatingAI?.Invoke(this, args);
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
