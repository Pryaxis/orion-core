using Orion.Interfaces;

namespace Orion.Data
{
	/// <summary>
	/// Wraps a Terraria NPC.
	/// </summary>
	public class Npc : Entity, INpc
	{
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

		/// <summary>
		/// Initializes a new instance of the <see cref="Npc"/> class wrapping the specified Terraria NPC.
		/// </summary>
		/// <param name="terrariaNpc">The Terraria NPC to wrap.</param>
		public Npc(Terraria.NPC terrariaNpc)
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
	}
}
