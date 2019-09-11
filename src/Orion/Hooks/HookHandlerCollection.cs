// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using Serilog;

namespace Orion.Hooks {
    /// <summary>
    /// Represents a collection of hook handlers. Provides the ability to register and unregister hook handlers. This
    /// class is immutable.
    /// </summary>
    /// <typeparam name="TArgs">The type of event arguments.</typeparam>
    public class HookHandlerCollection<TArgs> where TArgs : EventArgs {
        private static IComparer<Registration> RegistrationComparer =>
            Comparer<Registration>.Create((r1, r2) => r1.Priority.CompareTo(r2.Priority));

        private readonly ISet<Registration> _registrations;

        private HookHandlerCollection(ISet<Registration> registrations) {
            Debug.Assert(registrations != null, $"{nameof(registrations)} should not be null.");

            _registrations = registrations;
        }

        /// <summary>
        /// Invokes the collection of handlers in order of their priorities with the given arguments.
        /// </summary>
        /// <param name="sender">The sender. This is usually the object that initiated the event.</param>
        /// <param name="args">The event arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is <c>null</c>.</exception>
        public void Invoke(object sender, TArgs args) {
            if (args == null) throw new ArgumentNullException(nameof(args));

            Log.Debug("Calling {Hook} handlers", typeof(TArgs).Name);

            foreach (var handler in _registrations.Select(r => r.Handler)) {
                Log.Debug("Calling {Hook} handler registered by {Registrator}",
                          typeof(TArgs).Name, handler.Method.DeclaringType?.Name ?? "Unknown");

                try {
                    handler(sender, args);
                } catch (Exception ex) {
                    Log.Error(ex, "{Hook} handler threw exception", typeof(TArgs).Name);
                }
            }
        }

        /// <summary>
        /// Registers the given handler to the collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>The resulting collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <c>null</c>.</exception>
        public static HookHandlerCollection<TArgs> operator +(HookHandlerCollection<TArgs> collection,
                                                              HookHandler<TArgs> handler) {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            Log.Debug("Registering {Hook} handler from {Registrator}",
                      typeof(TArgs).Name, handler.Method.DeclaringType?.Name ?? "Unknown");

            var attribute = handler.Method.GetCustomAttribute<HookHandlerAttribute>();
            var priority = attribute?.Priority ?? HookPriority.Normal;
            var registration = new Registration(handler, priority);
            var registrations =
                new SortedSet<Registration>(collection?._registrations ?? Enumerable.Empty<Registration>(),
                                            RegistrationComparer) {registration};
            return new HookHandlerCollection<TArgs>(registrations);
        }

        /// <summary>
        /// Unregisters the given handler from the collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>The resulting collection.</returns>
        /// <exception cref="ArgumentException"><paramref name="handler"/> is not registered.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <c>null</c>.</exception>
        public static HookHandlerCollection<TArgs> operator -(HookHandlerCollection<TArgs> collection,
                                                              HookHandler<TArgs> handler) {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var attribute = handler.Method.GetCustomAttribute<HookHandlerAttribute>();
            var priority = attribute?.Priority ?? HookPriority.Normal;
            var registration = new Registration(handler, priority);
            var registrations =
                new SortedSet<Registration>(collection?._registrations ?? Enumerable.Empty<Registration>(),
                                            RegistrationComparer);
            if (!registrations.Contains(registration)) {
                throw new ArgumentException("Handler is not registered in the collection.", nameof(handler));
            }

            Log.Debug("Unregistering {Hook} handler from {Registrator}",
                      typeof(TArgs).Name, handler.Method.DeclaringType?.Name ?? "Unknown");

            registrations.Remove(registration);
            return new HookHandlerCollection<TArgs>(registrations);
        }


        private class Registration {
            public HookHandler<TArgs> Handler { get; }
            public HookPriority Priority { get; }

            public Registration(HookHandler<TArgs> handler, HookPriority priority) {
                Debug.Assert(handler != null, $"{nameof(handler)} should not be null.");

                Handler = handler;
                Priority = priority;
            }
        }
    }
}
