namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Encapsulates a Terraria player.
	/// </summary>
	public class Player : Entity, IPlayer
	{
		private ItemArray _inventory;
		private Terraria.Item[] _previousInventory;

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
		/// Gets the inventory <see cref="IItemArray"/>. A new instance will be created if the underlying array is
		/// reassigned.
		/// </summary>
		public IItemArray Inventory
		{
			get
			{
				if (!ReferenceEquals(Backing.inventory, _previousInventory))
				{
					// If Backing.inventory has been reassigned, then update.
					_previousInventory = Backing.inventory;
					_inventory = new ItemArray(Backing.inventory);
				}
				return _inventory;
			}
		}

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
		/// Gets the selected <see cref="IItem"/>.
		/// </summary>
		public IItem SelectedItem => Inventory[Backing.selectedItem];

		/// <summary>
		/// Initializes a new instance of the <see cref="Player"/> class encapsulating the specified Terraria player.
		/// </summary>
		/// <param name="player">The Terraria player.</param>
		public Player(Terraria.Player player) : base(player)
		{
			Backing = player;
			_inventory = new ItemArray(player.inventory);
			_previousInventory = player.inventory;
		}
	}
}
