using System;

namespace Orion.Items
{
	/// <summary>
	/// Provides a wrapper around an array of Terraria item instances.
	/// </summary>
	public interface IItemArray
	{
		/// <summary>
		/// Gets or sets the item at the specified index in the array.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is negative or greater than or equal to <see cref="Length"/>.
		/// </exception>
		IItem this[int index] { get; set; }

		/// <summary>
		/// Gets the array's length.
		/// </summary>
		int Length { get; }

		/// <summary>
		/// Gets the wrapped array of Terraria item instances.
		/// </summary>
		Terraria.Item[] WrappedItemArray { get; }
	}
}
