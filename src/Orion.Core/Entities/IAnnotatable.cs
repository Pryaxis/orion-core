// Copyright (c) 2020 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Orion.Core.Entities
{
    /// <summary>
    /// Provides annotation support.
    /// </summary>
    /// <remarks>
    /// <para>Implementations must be thread-safe.</para>
    /// <para>
    /// This interface allows consumers to attach custom state to objects without having to rely on the
    /// <see cref="ConditionalWeakTable{TKey, TValue}"/> class.
    /// </para>
    /// </remarks>
    public interface IAnnotatable
    {
        /// <summary>
        /// Gets the annotation with the given <paramref name="key"/>, using the specified
        /// <paramref name="initializer"/> to initialize the annotation if it does not exist.
        /// </summary>
        /// <typeparam name="TAnnotation">The type of annotation.</typeparam>
        /// <param name="key">The annotation key to get.</param>
        /// <param name="initializer">
        /// The initializer. If <see langword="null"/>, then a default initializer is used.
        /// </param>
        /// <returns>The annotation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        public TAnnotation GetAnnotation<TAnnotation>(
            AnnotationKey<TAnnotation> key, Func<TAnnotation>? initializer = null);

        /// <summary>
        /// Sets the annotation with the given <paramref name="key"/> to the specified <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TAnnotation">The type of annotation.</typeparam>
        /// <param name="key">The annotation key to set.</param>
        /// <param name="value">The value.</param>
        public void SetAnnotation<TAnnotation>(AnnotationKey<TAnnotation> key, TAnnotation value);

        /// <summary>
        /// Removes the annotation with the given <paramref name="key"/>. Returns a value indicating success.
        /// </summary>
        /// <typeparam name="TAnnotation">The type of annotation.</typeparam>
        /// <param name="key">The annotation key to remove.</param>
        /// <returns>
        /// <see langword="true"/> if the annotation was removed; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        public bool RemoveAnnotation<TAnnotation>(AnnotationKey<TAnnotation> key);

        /// <summary>
        /// Clears all annotations.
        /// </summary>
        public void ClearAnnotations();
    }

    /// <summary>
    /// Represents a strongly-typed annotation key.
    /// </summary>
    /// <typeparam name="TAnnotation">The type of annotation.</typeparam>
    public sealed class AnnotationKey<TAnnotation>
    {
    }

    /// <summary>
    /// Represents an annotatable object. Provides the base class for implementations of interfaces derived from
    /// <see cref="IAnnotatable"/>.
    /// </summary>
    public class AnnotatableObject : IAnnotatable
    {
        private readonly object _lock = new object();
        private readonly Dictionary<object, object?> _annotations = new Dictionary<object, object?>();

        /// <inheritdoc/>
        public TAnnotation GetAnnotation<TAnnotation>(
            AnnotationKey<TAnnotation> key, Func<TAnnotation>? initializer = null)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            lock (_lock)
            {
                if (!_annotations.TryGetValue(key, out var annotation))
                {
                    annotation = initializer is null ? default : initializer();
                    _annotations[key] = annotation;
                }

                Debug.Assert(annotation is TAnnotation);
                return (TAnnotation)annotation;
            }
        }

        /// <inheritdoc/>
        public void SetAnnotation<TAnnotation>(AnnotationKey<TAnnotation> key, TAnnotation value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            lock (_lock)
            {
                _annotations[key] = value;
            }
        }

        /// <inheritdoc/>
        public bool RemoveAnnotation<TAnnotation>(AnnotationKey<TAnnotation> key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            lock (_lock)
            {
                return _annotations.Remove(key);
            }
        }

        /// <inheritdoc/>
        public void ClearAnnotations()
        {
            lock (_lock)
            {
                _annotations.Clear();
            }
        }
    }
}
