namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Encapsulates a Terraria player.
	/// </summary>
	public class Player : Entity, IPlayer
	{
		/// <summary>
		/// Gets the backing Terraria player.
		/// </summary>
		public new Terraria.Player Backing { get; }

		/// <summary>
		/// Gets the defense.
		/// </summary>
		public int Defense => Backing.statDefense;

		/// <summary>
		/// Gets or sets the HP.
		/// </summary>
		public int HP
		{
			get { return Backing.statLife; }
			set { Backing.statLife = value; }
		}

		/// <summary>
		/// Gets the inventory array. This only includes the main inventory and mouse cursor.
		/// </summary>
		public IItem[] Inventory { get; }

		/// <summary>
		/// Gets or sets the maximum HP.
		/// </summary>
		public int MaxHP
		{
			get { return Backing.statLifeMax; }
			set { Backing.statLifeMax = value; }
		}

		/// <summary>
		/// Gets or sets the maximum MP.
		/// </summary>
		public int MaxMP
		{
			get { return Backing.statManaMax; }
			set { Backing.statManaMax = value; }
		}

		/// <summary>
		/// Gets or sets the MP.
		/// </summary>
		public int MP
		{
			get { return Backing.statMana; }
			set { Backing.statMana = value; }
		}

		/// <summary>
		/// Gets the selected item.
		/// </summary>
		public IItem SelectedItem => Inventory[Backing.selectedItem];

		/// <summary>
		/// Initializes a new instance of the <see cref="Player"/> class with the specified backing Terraria player.
		/// </summary>
		/// <param name="player">The backing Terraria player.</param>
		public Player(Terraria.Player player) : base(player)
		{
			Backing = player;
			Inventory = new IItem[player.inventory.Length];
			for (int i = 0; i < Inventory.Length; ++i)
			{
				Inventory[i] = new Item(player.inventory[i]);
			}
		}
	}
}
