using Microsoft.Xna.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a wrapper around a Terraria item.
	/// </summary>
	public interface IItem
	{
		/// <summary>
		/// Gets the item's damage.
		/// </summary>
		int Damage { get; }

		/// <summary>
		/// Gets the item's maximum stack size.
		/// </summary>
		int MaxStack { get; }

		/// <summary>
		/// Gets the item's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets or sets the item's position in the world.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets or sets the item's prefix.
		/// </summary>
		byte Prefix { get; set; }

		/// <summary>
		/// Gets or sets the item's stack size.
		/// </summary>
		int Stack { get; set; }

		/// <summary>
		/// Gets the item's type ID.
		/// </summary>
		int Type { get; }

		/// <summary>
		/// Gets or sets the item's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria item.
		/// </summary>
		Terraria.Item WrappedItem { get; }
	}
}
