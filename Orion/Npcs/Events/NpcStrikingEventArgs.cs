using System;
using System.ComponentModel;

namespace Orion.Npcs.Events
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcStriking"/> event.
	/// </summary>
	public class NpcStrikingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets or sets the <see cref="INpc"/> instance that was hit.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Gets or sets the hit damage.
		/// </summary>
		public int Damage { get; set; }

		/// <summary>
		/// Gets or sets the hit knockback.
		/// </summary>
		public float Knockback { get; set; }

		/// <summary>
		/// Gets or sets the hit direction.
		/// </summary>
		public int HitDirection { get; set; }

		/// <summary>
		/// Whether the hit was critical.
		/// </summary>
		public bool Critical { get; set; }

		/// <summary>
		/// Whether to display the hit effect. 
		/// </summary>
		public bool NoEffect { get; set; }

		/// <summary>
		/// Whether the hit was caused by a bug net.
		/// </summary>
		public bool FromNet { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcStrikingEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> instance that was hit.</param>
		/// <param name="damage">The hit damage.</param>
		/// <param name="knockback">The hit knockback.</param>
		/// <param name="hitDirection">The hit direction.</param>
		/// <param name="critical">Whether the hit was critical.</param>
		/// <param name="noEffect">Whether to display the hit effect.</param>
		/// <param name="fromNet">Whether the hit was caused by a bug net.</param>
		public NpcStrikingEventArgs(INpc npc, int damage, float knockback, int hitDirection, bool critical, bool noEffect, bool fromNet)
		{
			if (npc == null)
			{
				throw new ArgumentNullException(nameof(npc));
			}

			Npc = npc;
			Damage = damage;
			Knockback = knockback;
			HitDirection = hitDirection;
			Critical = critical;
			NoEffect = noEffect;
			FromNet = fromNet;
		}
	}
}
