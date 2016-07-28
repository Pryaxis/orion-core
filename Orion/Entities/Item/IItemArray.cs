using System;

namespace Orion.Entities.Item
{
	/// <summary>
	/// Provides a wrapper around an array of Terraria item instances.
	/// </summary>
	public interface IItemArray
	{
		/// <summary>
		/// Gets the array's length.
		/// </summary>
		int Length { get; }

		/// <summary>
		/// Gets the wrapped array of Terraria item instances.
		/// </summary>
		Terraria.Item[] WrappedItemArray { get; }

		/// <summary>
		/// Gets or sets the <see cref="IItem"/> instance at the specified index in the array.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <exception cref="ArgumentNullException"><paramref name="value"/> was null.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		IItem this[int index] { get; set; }
	}
}
