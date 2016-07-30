using System;
using Microsoft.Xna.Framework;

namespace Orion.Projectiles
{
	/// <summary>
	/// Wraps a Terraria projectile instance.
	/// </summary>
	public class Projectile : IProjectile
	{
		/// <inheritdoc/>
		public int Damage => WrappedProjectile.damage;

		/// <inheritdoc/>
		public bool IsHostile => WrappedProjectile.hostile;

		/// <inheritdoc/>
		public string Name => WrappedProjectile.name;

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedProjectile.position; }
			set { WrappedProjectile.position = value; }
		}

		/// <inheritdoc/>
		public ProjectileType Type => WrappedProjectile.type;

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedProjectile.velocity; }
			set { WrappedProjectile.velocity = value; }
		}

		/// <inheritdoc/>
		public Terraria.Projectile WrappedProjectile { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Projectile"/> class wrapping the specified Terraria projectile
		/// instance.
		/// </summary>
		/// <param name="projectile">The Terraria projectile to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="projectile"/> was null.</exception>
		public Projectile(Terraria.Projectile projectile)
		{
			if (projectile == null)
			{
				throw new ArgumentNullException(nameof(projectile));
			}

			WrappedProjectile = projectile;
		}

		/// <inheritdoc/>
		public void SetDefaults(ProjectileType type) => WrappedProjectile.SetDefaults(type);
	}
}
