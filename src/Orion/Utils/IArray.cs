using System;

namespace Orion.Utils {
    /// <summary>
    /// Provides access to a strongly-typed array of elements.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    public interface IArray<T> {
        /// <summary>
        /// Gets or sets the element at the given index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The element at the index.</returns>
        /// <exception cref="IndexOutOfRangeException"><paramref name="index"/> is out of range.</exception>
        T this[int index] { get; set; }

        /// <summary>
        /// Gets the count of elements.
        /// </summary>
        int Count { get; }
    }
}
