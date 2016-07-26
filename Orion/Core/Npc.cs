using System;
using Microsoft.Xna.Framework;
using Orion.Interfaces;

namespace Orion.Core
{
	/// <summary>
	/// Wraps a Terraria NPC.
	/// </summary>
	public class Npc : INpc
	{
		/// <summary>
		/// Gets the NPC's damage.
		/// </summary>
		public int Damage => WrappedNpc.damage;

		/// <summary>
		/// Gets the NPC's defense.
		/// </summary>
		public int Defense => WrappedNpc.defense;

		/// <summary>
		/// Gets or sets the NPC's health.
		/// </summary>
		public int Health
		{
			get { return WrappedNpc.life; }
			set { WrappedNpc.life = value; }
		}

		/// <summary>
		/// Gets or sets the NPC's maximum health.
		/// </summary>
		public int MaxHealth
		{
			get { return WrappedNpc.lifeMax; }
			set { WrappedNpc.lifeMax = value; }
		}

		/// <summary>
		/// Gets the NPC's name.
		/// </summary>
		public string Name => WrappedNpc.name;

		/// <summary>
		/// Gets or sets the NPC's position in the world.
		/// </summary>
		public Vector2 Position
		{
			get { return WrappedNpc.position; }
			set { WrappedNpc.position = value; }
		}

		/// <summary>
		/// Gets the NPC's type.
		/// </summary>
		public int Type => WrappedNpc.netID;

		/// <summary>
		/// Gets or sets the NPC's velocity in the world.
		/// </summary>
		public Vector2 Velocity
		{
			get { return WrappedNpc.velocity; }
			set { WrappedNpc.velocity = value; }
		}

		/// <summary>
		/// Gets the wrapped Terraria NPC.
		/// </summary>
		public Terraria.NPC WrappedNpc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Npc"/> class wrapping the specified Terraria NPC.
		/// </summary>
		/// <param name="terrariaNpc">The Terraria NPC to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaNpc"/> was null.</exception>
		public Npc(Terraria.NPC terrariaNpc)
		{
			if (terrariaNpc == null)
			{
				throw new ArgumentNullException(nameof(terrariaNpc));
			}

			WrappedNpc = terrariaNpc;
		}

		/// <summary>
		/// Kills the NPC.
		/// </summary>
		public void Kill()
		{
			Health = 0;
			WrappedNpc.checkDead();
		}

		/// <summary>
		/// Sets the NPC's defaults to the specified type's.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="type"/> was an invalid type.</exception>
		public void SetDefaults(int type)
		{
			if (type < 0 || type > Terraria.Main.maxNPCTypes)
			{
				throw new ArgumentOutOfRangeException(nameof(type));
			}

			WrappedNpc.SetDefaults(type);
		}
	}
}
