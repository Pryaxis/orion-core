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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Serilog;

namespace Orion.Events {
    /// <summary>
    /// Represents a collection of event handlers. Provides the ability to register and unregister event handlers. This
    /// class is thread-safe.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of event arguments.</typeparam>
    /// <remarks>
    /// The <see cref="EventHandlerCollection{TEventArgs}"/> class is a more featured version of
    /// <see cref="EventHandler{TEventArgs}"/> with the ability to specify the priority of each event handler. This
    /// allows consumers much more control over their event handlers. <para/>
    /// 
    /// Event handlers are registered and unregistered using the
    /// <see cref="RegisterHandler(EventHandler{TEventArgs}, ILogger)"/> and
    /// <see cref="UnregisterHandler(EventHandler{TEventArgs})"/> methods, and may be annotated using the
    /// <see cref="EventHandlerAttribute"/> attribute.
    /// </remarks>
    public sealed class EventHandlerCollection<TEventArgs> where TEventArgs : EventArgs {
        private readonly object _lock = new object();

        // Registrations sorted by priority.
        private readonly ISet<Registration> _registrations =
            new SortedSet<Registration>(Comparer<Registration>.Create((r1, r2) => r1.Priority.CompareTo(r2.Priority)));

        // Mapping from handler -> registration. This allows us to easily unregister handlers.
        private readonly IDictionary<EventHandler<TEventArgs>, Registration> _handlerToRegistration =
            new Dictionary<EventHandler<TEventArgs>, Registration>();

        /// <summary>
        /// Gets the event's name. This is used for logging.
        /// </summary>
        /// <value>
        /// The event's name. This is taken from the <see cref="EventArgsAttribute.Name"/> property on the
        /// <see cref="EventArgsAttribute"/> attribute annotating <typeparamref name="TEventArgs"/>. <para/>
        /// 
        /// If the attribute is missing, then the event name will default to the type name of
        /// <typeparamref name="TEventArgs"/>.
        /// </value>
        /// <remarks>
        /// The name is taken from the <see cref="EventArgsAttribute.Name"/> property on the
        /// <see cref="EventArgsAttribute"/> attribute annotating the <typeparamref name="TEventArgs"/> class. <para/>
        /// 
        /// If the attribute is missing, then the event name will default to the type name of
        /// <typeparamref name="TEventArgs"/>.
        /// </remarks>
        public string EventName { get; } =
            typeof(TEventArgs).GetCustomAttribute<EventArgsAttribute?>()?.Name ?? typeof(TEventArgs).Name;

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => EventName;

        /// <summary>
        /// Invokes the collection of handlers in the <see cref="EventHandlerCollection{TEventArgs}"/> in order of their
        /// priorities using the given <paramref name="sender"/> and <paramref name="args"/>.
        /// </summary>
        /// <param name="sender">The sender. This is the object that is initiating the event.</param>
        /// <param name="args">The event arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// While this method itself is thread-safe, care must be taken to make the handlers thread-safe if this method
        /// is expected to be called simultaneously on different thread. <para/>
        /// 
        /// Exceptions that occur in the handlers will be logged (if possible) and swallowed.
        /// </remarks>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types",
            Justification = "catching Exception for fail-safe")]
        public void Invoke(object? sender, TEventArgs args) {
            if (args is null) {
                throw new ArgumentNullException(nameof(args));
            }

            IList<Registration> registrations;
            lock (_lock) {
                registrations = _registrations.ToList();
            }

            foreach (var registration in registrations) {
                try {
                    registration.Handler(sender, args);
                } catch (Exception ex) {
                    // Not localized because this string is developer-facing.
                    registration.Log?.Error(ex,
                        "Unhandled exception occurred from {RegistrationName} in {Event}",
                        registration.Name, this);
                }
            }
        }

        /// <summary>
        /// Registers the given <paramref name="handler"/>, optionally with the specified <paramref name="log"/>.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="log">The log, or <see langword="null"/> for no logging.</param>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// <paramref name="log"/> will be used to provide logging for the
        /// <see cref="EventHandlerCollection{TEventArgs}"/>. It should be included if possible as it greatly helps with
        /// debugging.
        /// </remarks>
        public void RegisterHandler(EventHandler<TEventArgs> handler, ILogger? log = null) {
            if (handler is null) {
                throw new ArgumentNullException(nameof(handler));
            }

            var registration = new Registration(handler, log);
            lock (_lock) {
                _handlerToRegistration[handler] = registration;
                _registrations.Add(registration);
            }

            // Not localized because this string is developer-facing.
            log?.Debug("Registered {RegistrationName} onto {Event}", registration.Name, this);
        }

        /// <summary>
        /// Unregisters the given <paramref name="handler"/>.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="handler"/> was unregistered; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// All handler registrations should have a corresponding handler unregistration. Neglecting unregistrations can
        /// result in memory leaks and incorrect unloading behavior.
        /// </remarks>
        public bool UnregisterHandler(EventHandler<TEventArgs> handler) {
            if (handler is null) {
                throw new ArgumentNullException(nameof(handler));
            }

            Registration? registration = null;
            var result = false;
            lock (_lock) {
                if (!_handlerToRegistration.TryGetValue(handler, out registration)) {
                    return false;
                }

                result = _handlerToRegistration.Remove(handler) & _registrations.Remove(registration);
            }
            
            // Not localized because this string is developer-facing.
            registration.Log?.Debug("Unregistered {RegistrationName} from {Event}", registration.Name, this);
            return result;
        }

        // Keeps track of handlers along with extra metadata.
        private class Registration {
            public EventHandler<TEventArgs> Handler { get; }
            public ILogger? Log { get; }
            public EventPriority Priority { get; }
            public string Name { get; }

            public Registration(EventHandler<TEventArgs> handler, ILogger? log) {
                Handler = handler;
                Log = log;

                var attribute = handler.Method.GetCustomAttribute<EventHandlerAttribute?>();
                Priority = attribute?.Priority ?? EventPriority.Normal;
                Name = attribute?.Name ?? handler.Method.Name;
            }
        }
    }
}
