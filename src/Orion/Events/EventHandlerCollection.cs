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
    /// class is immutable.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of event arguments.</typeparam>
    public sealed class EventHandlerCollection<TEventArgs> where TEventArgs : EventArgs {
        private static readonly IComparer<Registration> _registrationComparer =
            Comparer<Registration>.Create((r1, r2) => r1.Priority.CompareTo(r2.Priority));

        private readonly ISet<Registration> _registrations;

        private EventHandlerCollection(ISet<Registration> registrations) {
            Debug.Assert(registrations != null, "registrations should not be null");

            _registrations = registrations;
        }

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
        /// Registers the given <paramref name="handler"/> to <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>The resulting collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
        public static EventHandlerCollection<TEventArgs> operator +(
                EventHandlerCollection<TEventArgs>? collection, EventHandler<TEventArgs> handler) {
            if (handler is null) {
                throw new ArgumentNullException(nameof(handler));
            }

            var attribute = handler.Method.GetCustomAttribute<EventHandlerAttribute>();
            var priority = attribute?.Priority ?? EventPriority.Normal;
            var registration = new Registration(handler, priority);
            var registrations = new SortedSet<Registration>(
                collection?._registrations ?? Enumerable.Empty<Registration>(), _registrationComparer) {registration};
            return new EventHandlerCollection<TEventArgs>(registrations);
        }

        /// <summary>
        /// Unregisters the given <paramref name="handler"/> from <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>The resulting collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
        public static EventHandlerCollection<TEventArgs>? operator -(
                EventHandlerCollection<TEventArgs>? collection, EventHandler<TEventArgs> handler) {
            if (handler is null) {
                throw new ArgumentNullException(nameof(handler));
            }

            var attribute = handler.Method.GetCustomAttribute<EventHandlerAttribute>();
            var priority = attribute?.Priority ?? EventPriority.Normal;
            var registration = new Registration(handler, priority);
            var registrations = new SortedSet<Registration>(
                collection?._registrations ?? Enumerable.Empty<Registration>(), _registrationComparer);
            registrations.Remove(registration);
            return registrations.Count == 0 ? null : new EventHandlerCollection<TEventArgs>(registrations);
        }

        // Keeps track of handlers along with their priorities.
        private class Registration {
            public EventHandler<TEventArgs> Handler { get; }
            public EventPriority Priority { get; }

            public Registration(EventHandler<TEventArgs> handler, EventPriority priority) {
                Debug.Assert(handler != null, "handler should not be null");

                Handler = handler;
                Priority = priority;
            }
        }
    }
}
