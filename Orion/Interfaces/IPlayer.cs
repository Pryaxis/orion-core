using System.Collections.Generic;

namespace Orion.Interfaces
{
	/// <summary>
	/// Encapsulates a Terraria player.
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
		/// Gets or sets the maximum HP.
		/// </summary>
		int MaxHP { get; set; }

		/// <summary>
		/// Gets or sets the MP.
		/// </summary>
		int MP { get; set; }

		/// <summary>
		/// Gets or sets the maximum MP.
		/// </summary>
		int MaxMP { get; set; }

		/// <summary>
		/// Gets the inventory. This only includes the main inventory and mouse cursor.
		/// </summary>
		IReadOnlyList<IItem> Inventory { get; }

		// TODO: complete
	}
}
