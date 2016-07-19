namespace Orion.Interfaces
{
	/// <summary>
	/// Wraps a <see cref="Terraria.Item"/>.
	/// </summary>
	public interface IItem : IEntity
	{
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

		/// <summary>
		/// Gets the wrapped <see cref="Terraria.Item"/>.
		/// </summary>
		Terraria.Item WrappedItem { get; }
	}
}
