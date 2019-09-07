using System.Collections.Generic;

namespace Orion {
    internal class AnnotatableObject : IAnnotatable {
        private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();

        public T GetAnnotation<T>(string key, T defaultValue = default) =>
            _annotations.TryGetValue(key, out var value) ? (T)value : defaultValue;

        public void SetAnnotation<T>(string key, T value) => _annotations[key] = value;
    }
}
