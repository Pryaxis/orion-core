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
		public int Damage
		{
			get { return WrappedProjectile.damage; }
			set { WrappedProjectile.damage = value; }
		}

		/// <inheritdoc/>
		public int Height
		{
			get { return WrappedProjectile.height; }
			set { WrappedProjectile.height = value; }
		}

		/// <inheritdoc/>
		public bool IsHostile
		{
			get { return WrappedProjectile.hostile; }
			set { WrappedProjectile.hostile = value; }
		}

		/// <inheritdoc/>
		public bool IsMagic
		{
			get { return WrappedProjectile.magic; }
			set { WrappedProjectile.magic = value; }
		}

		/// <inheritdoc/>
		public bool IsMelee
		{
			get { return WrappedProjectile.melee; }
			set { WrappedProjectile.melee = value; }
		}

		/// <inheritdoc/>
		public bool IsMinion
		{
			get { return WrappedProjectile.minion; }
			set { WrappedProjectile.minion = value; }
		}

		/// <inheritdoc/>
		public bool IsRanged
		{
			get { return WrappedProjectile.ranged; }
			set { WrappedProjectile.ranged = value; }
		}

		/// <inheritdoc/>
		public bool IsThrown
		{
			get { return WrappedProjectile.thrown; }
			set { WrappedProjectile.thrown = value; }
		}

		/// <inheritdoc/>
		public string Name
		{
			get { return WrappedProjectile.name; }
			set { WrappedProjectile.name = value; }
		}

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedProjectile.position; }
			set { WrappedProjectile.position = value; }
		}

		/// <inheritdoc/>
		public ProjectileType Type => (ProjectileType)WrappedProjectile.type;

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedProjectile.velocity; }
			set { WrappedProjectile.velocity = value; }
		}

		/// <inheritdoc/>
		public int Width
		{
			get { return WrappedProjectile.width; }
			set { WrappedProjectile.width = value; }
		}

		/// <inheritdoc/>
		public Terraria.Projectile WrappedProjectile { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Projectile"/> class wrapping the specified Terraria projectile
		/// instance.
		/// </summary>
		/// <param name="terrariaProjectile">The Terraria projectile to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaProjectile"/> is null.</exception>
		public Projectile(Terraria.Projectile terrariaProjectile)
		{
			if (terrariaProjectile == null)
			{
				throw new ArgumentNullException(nameof(terrariaProjectile));
			}

			WrappedProjectile = terrariaProjectile;
		}

		/// <inheritdoc/>
		public void Kill() => WrappedProjectile.Kill();

		/// <inheritdoc/>
		public void SetDefaults(ProjectileType type) => WrappedProjectile.SetDefaults((int)type);
	}
}
