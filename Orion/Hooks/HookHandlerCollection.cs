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
        /// <param name="sender">The sender. This is usually the service instance which initiated the event.</param>
        /// <param name="args">The event arguments.</param>
        /// <remarks>
        /// All exceptions are consumed and logged so that a faulty handler does not bring down the entire server.
        /// </remarks>
        public void Invoke(object sender, TArgs args) {
            foreach (var handler in _registrations.Select(r => r.Handler)) {
                Log.Debug("Calling {Hook} handler registered by {Registrator}",
                          typeof(TArgs).Name, handler.Method.DeclaringType?.Name);

                try {
                    handler(sender, args);
                } catch (Exception ex) {
                    Log.Error(ex, "Handler threw exception");
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
            if (handler == null) {
                throw new ArgumentNullException(nameof(handler));
            }

            Log.Debug("Registering {Hook} handler from {Registrator}",
                      typeof(TArgs).Name, handler.Method.DeclaringType?.Name);

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
            if (handler == null) {
                throw new ArgumentNullException(nameof(handler));
            }

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
                      typeof(TArgs).Name, handler.Method.DeclaringType?.Name);

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
