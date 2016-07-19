namespace Orion.Interfaces
{
	/// <summary>
	/// Wraps a <see cref="Terraria.Player"/>.
	/// </summary>
	public interface IPlayer : IEntity
	{
		/// <summary>
		/// Gets the defense.
		/// </summary>
		int Defense { get; }

		/// <summary>
		/// Gets or sets the HP.
		/// </summary>
		int HP { get; set; }

		/// <summary>
		/// Gets the inventory <see cref="IItemArray"/>.
		/// </summary>
		IItemArray Inventory { get; }

		/// <summary>
		/// Gets or sets the maximum HP.
		/// </summary>
		int MaxHP { get; set; }

		/// <summary>
		/// Gets or sets the maximum MP.
		/// </summary>
		int MaxMP { get; set; }

		/// <summary>
		/// Gets or sets the MP.
		/// </summary>
		int MP { get; set; }

		/// <summary>
		/// Gets the selected <see cref="IItem"/>.
		/// </summary>
		IItem SelectedItem { get; }

		/// <summary>
		/// Gets the wrapped <see cref="Terraria.Player"/>.
		/// </summary>
		Terraria.Player WrappedPlayer { get; }
	}
}
