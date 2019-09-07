using System;

namespace Orion {
    /// <summary>
    /// Provides methods with which to retrieve and set annotations.
    /// </summary>
    public interface IAnnotatable {
        /// <summary>
        /// Gets the annotation of a type with the given key, returning a default value if the key does not exist.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The metadata, or a default value if the key does not exist.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        T GetAnnotation<T>(string key, T defaultValue = default);

        /// <summary>
        /// Sets the annotation of a type with the given key to the value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        void SetAnnotation<T>(string key, T value);
    }
}
