using Orion.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace Orion.Data
{
	/// <summary>
	/// Wraps a Terraria NPC.
	/// </summary>
	public class Npc : Entity, INpc
	{
		private static readonly ConditionalWeakTable<Terraria.NPC, Npc> Cache
			= new ConditionalWeakTable<Terraria.NPC, Npc>();

		/// <summary>
		/// Gets or sets the NPC's HP.
		/// </summary>
		public int HP
		{
			get { return WrappedNpc.life; }
			set { WrappedNpc.life = value; }
		}

		/// <summary>
		/// Gets or sets the NPC's maximum HP.
		/// </summary>
		public int MaxHP
		{
			get { return WrappedNpc.lifeMax; }
			set { WrappedNpc.lifeMax = value; }
		}

		/// <summary>
		/// Gets the NPC's type ID.
		/// </summary>
		public int Type => WrappedNpc.type;

		/// <summary>
		/// Gets the wrapped Terraria entity.
		/// </summary>
		public override Terraria.Entity WrappedEntity => WrappedNpc;

		/// <summary>
		/// Gets the wrapped Terraria NPC.
		/// </summary>
		public Terraria.NPC WrappedNpc { get; }
		
		private Npc(Terraria.NPC terrariaNpc)
		{
			WrappedNpc = terrariaNpc;
		}

		/// <summary>
		/// Kills the NPC.
		/// </summary>
		public void Kill()
		{
			HP = 0;
			WrappedNpc.checkDead();
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Npc"/> class wrapping the specified Terraria NPC. If this method is
		/// called multiple times on the same Terraria NPC, then the same <see cref="Npc"/> will be returned.
		/// </summary>
		/// <param name="terrariaNpc">The Terraria NPC to wrap.</param>
		/// <returns>An <see cref="Npc"/> that wraps <paramref name="terrariaNpc"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaNpc"/> was null.</exception>
		public static Npc Wrap(Terraria.NPC terrariaNpc)
		{
			if (terrariaNpc == null)
			{
				throw new ArgumentNullException(nameof(terrariaNpc));
			}

			Npc npc;
			if (!Cache.TryGetValue(terrariaNpc, out npc))
			{
				npc = new Npc(terrariaNpc);
				Cache.Add(terrariaNpc, npc);
			}
			return npc;
		}
	}
}
