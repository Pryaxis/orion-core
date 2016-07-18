namespace Orion.Interfaces
{
	/// <summary>
	/// Encapsulates a Terraria item.
	/// </summary>
	public interface IItem : IEntity
	{
		/// <summary>
		/// Gets the backing Terraria item.
		/// </summary>
		new Terraria.Item Backing { get; }

		/// <summary>
		/// Gets the damage.
		/// </summary>
		int Damage { get; }

		/// <summary>
		/// Gets the maximum stack size.
		/// </summary>
		int MaxStack { get; }

		/// <summary>
		/// Gets or sets the prefix.
		/// </summary>
		byte Prefix { get; set; }

		/// <summary>
		/// Gets or sets the stack size.
		/// </summary>
		int Stack { get; set; }

		/// <summary>
		/// Gets the type ID.
		/// </summary>
		int Type { get; }

		// TODO: complete
	}
}
