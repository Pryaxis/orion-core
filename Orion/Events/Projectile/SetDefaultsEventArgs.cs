using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Interfaces;

namespace Orion.Events.Projectile
{
	/// <summary>
	/// Provides data for the <see cref="IProjectileService.SetDefaults"/> event.
	/// </summary>
	public class SetDefaultsEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the <see cref="IProjectile"/> that had its defaults set.
		/// </summary>
		public Terraria.Projectile Projectile { get; }

		/// <summary>
		/// Gets the type ID that the <see cref="IProjectile"/> had its defaults set to. 
		/// </summary>
		public int Type { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SetDefaultsEventArgs"/> class. 
		/// </summary>
		/// <param name="projectile">The <see cref="IProjectile"/> that had its defaults set.</param>
		/// <param name="type">The type ID that the <see cref="IProjectile"/> had its defaults set to. </param>
		public SetDefaultsEventArgs(Terraria.Projectile projectile, int type)
		{
			Projectile = projectile;
			Type = type;
		}
	}
}
