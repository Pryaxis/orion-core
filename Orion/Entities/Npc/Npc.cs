using System;
using Microsoft.Xna.Framework;

namespace Orion.Entities.Npc
{
	/// <summary>
	/// Wraps a Terraria NPC instance.
	/// </summary>
	public class Npc : INpc
	{
		/// <inheritdoc/>
		public int Damage => WrappedNpc.damage;

		/// <inheritdoc/>
		public int Defense => WrappedNpc.defense;

		/// <inheritdoc/>
		public int Health
		{
			get { return WrappedNpc.life; }
			set { WrappedNpc.life = value; }
		}

		/// <inheritdoc/>
		public int MaxHealth
		{
			get { return WrappedNpc.lifeMax; }
			set { WrappedNpc.lifeMax = value; }
		}

		/// <inheritdoc/>
		public string Name => WrappedNpc.name;

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedNpc.position; }
			set { WrappedNpc.position = value; }
		}

		/// <inheritdoc/>
		public int Type => WrappedNpc.netID;

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedNpc.velocity; }
			set { WrappedNpc.velocity = value; }
		}

		/// <inheritdoc/>
		public Terraria.NPC WrappedNpc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Npc"/> class wrapping the specified Terraria NPC instance.
		/// </summary>
		/// <param name="terrariaNpc">The Terraria NPC instance to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaNpc"/> was null.</exception>
		public Npc(Terraria.NPC terrariaNpc)
		{
			if (terrariaNpc == null)
			{
				throw new ArgumentNullException(nameof(terrariaNpc));
			}

			WrappedNpc = terrariaNpc;
		}

		/// <inheritdoc/>
		public void Kill()
		{
			Health = 0;
			WrappedNpc.checkDead();
		}

		/// <inheritdoc/>
		public void SetDefaults(int type)
		{
			if (type < 0 || type >= Terraria.Main.maxNPCTypes)
			{
				throw new ArgumentOutOfRangeException(nameof(type));
			}

			WrappedNpc.SetDefaults(type);
		}
	}
}
