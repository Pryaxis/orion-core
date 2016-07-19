namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a wrapper around a Terraria player.
	/// </summary>
	public interface IPlayer : IEntity
	{
		/// <summary>
		/// Gets the player's defense.
		/// </summary>
		int Defense { get; }

		/// <summary>
		/// Gets or sets the player's HP.
		/// </summary>
		int HP { get; set; }

		/// <summary>
		/// Gets the player's inventory <see cref="IItemArray"/>.
		/// </summary>
		IItemArray Inventory { get; }

		/// <summary>
		/// Gets or sets the player's maximum HP.
		/// </summary>
		int MaxHP { get; set; }

		/// <summary>
		/// Gets or sets the player's maximum MP.
		/// </summary>
		int MaxMP { get; set; }

		/// <summary>
		/// Gets or sets the player's MP.
		/// </summary>
		int MP { get; set; }

		/// <summary>
		/// Gets the player's selected <see cref="IItem"/>.
		/// </summary>
		IItem SelectedItem { get; }

		/// <summary>
		/// Gets the wrapped Terraria player.
		/// </summary>
		Terraria.Player WrappedPlayer { get; }
	}
}
