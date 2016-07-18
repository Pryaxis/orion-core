namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Encapsulates a Terraria NPC.
	/// </summary>
	public class Npc : Entity, INpc
	{
		/// <summary>
		/// Gets the backing Terraria NPC.
		/// </summary>
		public new Terraria.NPC Backing { get; }

		/// <summary>
		/// Gets or sets the HP.
		/// </summary>
		public int HP
		{
			get { return Backing.life; }
			set { Backing.life = value; }
		}

		/// <summary>
		/// Gets or sets the maximum HP.
		/// </summary>
		public int MaxHP
		{
			get { return Backing.lifeMax; }
			set { Backing.lifeMax = value; }
		}

		/// <summary>
		/// Gets the type ID.
		/// </summary>
		public int Type => Backing.type;

		/// <summary>
		/// Initializes a new instance of the <see cref="Npc"/> class encapsulating the specified Terraria NPC.
		/// </summary>
		/// <param name="npc">The Terraria NPC.</param>
		public Npc(Terraria.NPC npc) : base(npc)
		{
			Backing = npc;
		}
	}
}
