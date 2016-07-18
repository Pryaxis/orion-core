namespace Orion.Interfaces
{
	/// <summary>
	/// Encapsulates a Terraria item.
	/// </summary>
	public interface IItem : IEntity
	{
		/// <summary>
		/// Gets the damage value.
		/// </summary>
		int Damage { get; }

		/// <summary>
		/// Gets or sets the prefix.
		/// </summary>
		int Prefix { get; set; }

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
