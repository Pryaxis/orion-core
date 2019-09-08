using System.Collections.Generic;

namespace Orion {
    /// <summary>
    /// An object that implements <see cref="IAnnotatable"/>.
    /// </summary>
    public class AnnotatableObject : IAnnotatable {
        private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();

        /// <inheritdoc />
        public T GetAnnotation<T>(string key, T defaultValue = default) =>
            _annotations.TryGetValue(key, out var value) ? (T)value : defaultValue;

        /// <inheritdoc />
        public void SetAnnotation<T>(string key, T value) => _annotations[key] = value;
    }
}
