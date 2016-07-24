using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Orion.Interfaces;

namespace Orion.Core
{
	/// <summary>
	/// Wraps a Terraria Projectile.
	/// </summary>
	public class Projectile : IProjectile
	{
		/// <summary>
		/// Gets the projectile damage.
		/// </summary>
		public int Damage => WrappedProjectile.damage;

		/// <summary>
		/// Gets the projectile name.
		/// </summary>
		public string Name => WrappedProjectile.name;

		/// <summary>
		/// Gets the projectile type ID.
		/// </summary>
		public int Type => WrappedProjectile.type;

		/// <summary>
		/// Gets or sets the projectile's position.
		/// </summary>
		public Vector2 Position
		{
			get { return WrappedProjectile.position; }
			set { WrappedProjectile.position = value; }
		}

		/// <summary>
		/// Gets or sets the projectile's velocity.
		/// </summary>
		public Vector2 Velocity
		{
			get { return WrappedProjectile.velocity; }
			set { WrappedProjectile.velocity = value; }
		}

		/// <summary>
		/// Gets the wrapped Terraria projectile.
		/// </summary>
		public Terraria.Projectile WrappedProjectile { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Projectile"/> class wrapping the specified Terraria projectile.
		/// </summary>
		/// <param name="projectile">The Terraria projectile to wrap.</param>
		public Projectile(Terraria.Projectile projectile)
		{
			WrappedProjectile = projectile;
		}
	}
}
