using System;
using Microsoft.Xna.Framework;

namespace Orion.Npcs
{
	/// <summary>
	/// Wraps a Terraria NPC instance.
	/// </summary>
	public class Npc : INpc
	{
		/// <inheritdoc/>
		public int Damage
		{
			get { return WrappedNpc.damage; }
			set { WrappedNpc.damage = value; }
		}

		/// <inheritdoc/>
		public int Defense
		{
			get { return WrappedNpc.defense; }
			set { WrappedNpc.defense = value; }
		}

		/// <inheritdoc/>
		public int Health
		{
			get { return WrappedNpc.life; }
			set { WrappedNpc.life = value; }
		}

		/// <inheritdoc/>
		public int Height
		{
			get { return WrappedNpc.height; }
			set { WrappedNpc.height = value; }
		}

		/// <inheritdoc/>
		public bool IsBoss
		{
			get { return WrappedNpc.boss; }
			set { WrappedNpc.boss = value; }
		}

		/// <inheritdoc/>
		public int MaxHealth
		{
			get { return WrappedNpc.lifeMax; }
			set { WrappedNpc.lifeMax = value; }
		}

		/// <inheritdoc/>
		public string Name
		{
			get { return WrappedNpc.name; }
			set { WrappedNpc.name = value; }
		}

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedNpc.position; }
			set { WrappedNpc.position = value; }
		}

		/// <inheritdoc/>
		public NpcType Type => (NpcType)WrappedNpc.netID;

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedNpc.velocity; }
			set { WrappedNpc.velocity = value; }
		}

		/// <inheritdoc/>
		public int Width
		{
			get { return WrappedNpc.width; }
			set { WrappedNpc.width = value; }
		}

		/// <inheritdoc/>
		public Terraria.NPC WrappedNpc { get; }

		/// <inheritdoc/>
		public Terraria.Entity WrappedEntity { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Npc"/> class wrapping the specified Terraria NPC instance.
		/// </summary>
		/// <param name="terrariaNpc">The Terraria NPC instance to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaNpc"/> is null.</exception>
		public Npc(Terraria.NPC terrariaNpc)
		{
			if (terrariaNpc == null)
			{
				throw new ArgumentNullException(nameof(terrariaNpc));
			}

			WrappedNpc = terrariaNpc;
			WrappedEntity = terrariaNpc;
		}

		/// <inheritdoc/>
		public void Kill()
		{
			Health = 0;
			WrappedNpc.checkDead();
		}

		/// <inheritdoc/>
		public void SetDefaults(NpcType type) => WrappedNpc.SetDefaults((int)type);
	}
}
