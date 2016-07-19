using System;

namespace Orion.Interfaces
{
	/// <summary>
	/// Wraps a <see cref="Terraria.Item"/> array.
	/// </summary>
	public interface IItemArray
	{
		/// <summary>
		/// Gets the wrapped <see cref="Terraria.Item"/> array.
		/// </summary>
		Terraria.Item[] WrappedItemArray { get; }

		/// <summary>
		/// Gets or sets the <see cref="IItem"/> at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The <see cref="IItem"/> at the specified index.</returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		IItem this[int index] { get; set; }
	}
}
