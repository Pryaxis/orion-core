using System.Collections.Generic;

namespace Orion.Interfaces
{
	/// <summary>
	/// Encapsulates a Terraria player.
	/// </summary>
	public interface IPlayer : IEntity
	{
		/// <summary>
		/// Gets the backing Terraria player.
		/// </summary>
		new Terraria.Player Backing { get; }

		/// <summary>
		/// Gets the defense.
		/// </summary>
		int Defense { get; }

		/// <summary>
		/// Gets or sets the HP.
		/// </summary>
		int HP { get; set; }

		/// <summary>
		/// Gets the inventory <see cref="IItemArray"/>. A new instance will be created if the underlying array is
		/// reassigned.
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

		// TODO: complete
	}
}
