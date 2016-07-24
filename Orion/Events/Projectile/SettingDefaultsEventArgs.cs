using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Interfaces;
using System.ComponentModel;

namespace Orion.Events.Projectile
{
	/// <summary>
	/// Provides data for the see <see cref="IProjectileService.SettingDefaults"/> event.
	/// </summary>
	public class SettingDefaultsEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the <see cref="IProjectile"/> that's having its defaults set.
		/// </summary>
		public Terraria.Projectile Projectile { get; }

		/// <summary>
		/// Gets or sets the type ID for the <see cref="IProjectile"/> is having its defaults set to.
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingDefaultsEventArgs"/> class.
		/// </summary>
		/// <param name="projectile">The <see cref="IProjectile"/> that's having its defaults set.</param>
		/// <param name="type">The type ID that the <see cref="IProjectile"/> is having its defaults set to.</param>
		public SettingDefaultsEventArgs(Terraria.Projectile projectile, int type)
		{
			Projectile = projectile;
			Type = type;
		}
	}
}
