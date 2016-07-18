namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Encapsulates a Terraria item.
	/// </summary>
	public class Item : Entity, IItem
	{
		/// <summary>
		/// Gets the backing Terraria item.
		/// </summary>
		public new Terraria.Item Backing { get; }

		/// <summary>
		/// Gets the damage.
		/// </summary>
		public int Damage => Backing.damage;

		/// <summary>
		/// Gets the maximum stack size.
		/// </summary>
		public int MaxStack => Backing.maxStack;

		/// <summary>
		/// Gets or sets the prefix.
		/// </summary>
		public byte Prefix
		{
			get { return Backing.prefix; }
			set { Backing.prefix = value; }
		}

		/// <summary>
		/// Gets or sets the stack size.
		/// </summary>
		public int Stack
		{
			get { return Backing.stack; }
			set { Backing.stack = value; }
		}

		/// <summary>
		/// Gets the type ID.
		/// </summary>
		public int Type => Backing.netID;

		/// <summary>
		/// Initializes a new instance of the <see cref="Item"/> class encapsulating the specified Terraria item.
		/// </summary>
		/// <param name="item">The Terraria item.</param>
		public Item(Terraria.Item item) : base(item)
		{
			Backing = item;
		}
	}
}
