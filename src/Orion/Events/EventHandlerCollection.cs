// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using System.Linq;
using System.Reflection;

namespace Orion.Events {
    /// <summary>
    /// Represents a collection of event handlers. Provides the ability to register and unregister event handlers. This
    /// class is thread-safe.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of event arguments.</typeparam>
    public sealed class EventHandlerCollection<TEventArgs> where TEventArgs : EventArgs {
        private readonly object _lock = new object();

        private readonly ISet<Registration> _registrations =
            new SortedSet<Registration>(Comparer<Registration>.Create((r1, r2) => r1.Priority.CompareTo(r2.Priority)));

        private readonly IDictionary<EventHandler<TEventArgs>, Registration> _handlerToRegistration =
            new Dictionary<EventHandler<TEventArgs>, Registration>();

        /// <summary>
        /// Invokes the collection of handlers in order of their priorities using the given <paramref name="sender"/>
        /// and <paramref name="args"/>.
        /// </summary>
        /// <param name="sender">The sender. This is the object that is initiating the event.</param>
        /// <param name="args">The event arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is <see langword="null"/>.</exception>
        public void Invoke(object? sender, TEventArgs args) {
            if (args is null) {
                throw new ArgumentNullException(nameof(args));
            }

            foreach (var handler in _registrations.Select(r => r.Handler)) {
                handler(sender, args);
            }
        }

        /// <summary>
        /// Registers the given <paramref name="handler"/>.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
        public void RegisterHandler(EventHandler<TEventArgs> handler) {
            if (handler is null) {
                throw new ArgumentNullException(nameof(handler));
            }

            var priority = handler.Method.GetCustomAttribute<EventHandlerAttribute>()?.Priority ?? EventPriority.Normal;
            lock (_lock) {
                var registration = new Registration(handler, priority);
                _handlerToRegistration[handler] = registration;
                _registrations.Add(registration);
            }
        }

        /// <summary>
        /// Unregisters the given <paramref name="handler"/>.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="handler"/> was unregistered; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
        public bool UnregisterHandler(EventHandler<TEventArgs> handler) {
            if (handler is null) {
                throw new ArgumentNullException(nameof(handler));
            }

            lock (_lock) {
                if (!_handlerToRegistration.TryGetValue(handler, out var registration)) {
                    return false;
                }

                return _handlerToRegistration.Remove(handler) & _registrations.Remove(registration);
            }
        }

        // Keeps track of handlers along with their priorities.
        private class Registration {
            public EventHandler<TEventArgs> Handler { get; }
            public EventPriority Priority { get; }

            public Registration(EventHandler<TEventArgs> handler, EventPriority priority) {
                Handler = handler;
                Priority = priority;
            }
        }
    }
}
