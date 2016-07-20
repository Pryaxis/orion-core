using System;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a wrapper around a Terraria item array.
	/// </summary>
	public interface IItemArray
	{
		/// <summary>
		/// Gets the wrapped Terraria item array.
		/// </summary>
		Terraria.Item[] WrappedItemArray { get; }

		/// <summary>
		/// Gets or sets the <see cref="IItem"/> at the specified index.
		/// </summary>
		/// <param name="index">The index to retrieve or modify.</param>
		/// <returns>The <see cref="IItem"/> at <paramref name="index"/>.</returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		IItem this[int index] { get; set; }
	}
}
