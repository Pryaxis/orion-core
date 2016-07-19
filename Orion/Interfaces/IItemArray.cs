using System;

namespace Orion.Interfaces
{
	/// <summary>
	/// Encapsulates an array of Terraria items.
	/// </summary>
	public interface IItemArray
	{
		/// <summary>
		/// Gets the backing Terraria item array.
		/// </summary>
		Terraria.Item[] Backing { get; }

		/// <summary>
		/// Gets or sets the <see cref="IItem"/> at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>
		/// The <see cref="IItem"/> at the specified index. A new instance will be created if the underlying item is
		/// reassigned.
		/// </returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		IItem this[int index] { get; set; }
	}
}
