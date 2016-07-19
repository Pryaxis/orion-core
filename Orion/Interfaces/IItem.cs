namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a wrapper around a Terraria item.
	/// </summary>
	public interface IItem : IEntity
	{
		/// <summary>
		/// Gets the item damage.
		/// </summary>
		int Damage { get; }

		/// <summary>
		/// Gets the item maximum stack size.
		/// </summary>
		int MaxStack { get; }

		/// <summary>
		/// Gets or sets the item prefix.
		/// </summary>
		byte Prefix { get; set; }

		/// <summary>
		/// Gets or sets the item stack size.
		/// </summary>
		int Stack { get; set; }

		/// <summary>
		/// Gets the item type ID.
		/// </summary>
		int Type { get; }

		/// <summary>
		/// Gets the wrapped Terraria item.
		/// </summary>
		Terraria.Item WrappedItem { get; }
	}
}
