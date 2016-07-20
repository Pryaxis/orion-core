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
		/// Gets the NPC's name.
		/// </summary>
		public string Name => WrappedNpc.name;

		/// <summary>
		/// Gets or sets the NPC's position.
		/// </summary>
		public Vector2 Position
		{
			get { return WrappedNpc.position; }
			set { WrappedNpc.position = value; }
		}

		/// <summary>
		/// Gets the NPC's type ID.
		/// </summary>
		public int Type => WrappedNpc.type;

		/// <summary>
		/// Gets or sets the NPC's velocity.
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
