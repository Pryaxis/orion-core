using System;
using Microsoft.Xna.Framework;
using Orion.Interfaces;

namespace Orion.Core
{
	/// <summary>
	/// Wraps a Terraria projectile.
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
		public int Type => WrappedProjectile.type;

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedProjectile.position; }
			set { WrappedProjectile.position = value; }
		}

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedProjectile.velocity; }
			set { WrappedProjectile.velocity = value; }
		}

		/// <inheritdoc/>
		public Terraria.Projectile WrappedProjectile { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Projectile"/> class wrapping the specified Terraria projectile.
		/// </summary>
		/// <param name="terrariaProjectile">The Terraria projectile to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaProjectile"/> was null.</exception>
		public Projectile(Terraria.Projectile terrariaProjectile)
		{
			if (terrariaProjectile == null)
			{
				throw new ArgumentNullException(nameof(terrariaProjectile));
			}

			WrappedProjectile = terrariaProjectile;
		}

		/// <inheritdoc/>
		public void SetDefaults(int type)
		{
			if (type < 0 || type > Terraria.Main.maxProjectileTypes)
			{
				throw new ArgumentOutOfRangeException(nameof(type));
			}

			WrappedProjectile.SetDefaults(type);
		}
	}
}
