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
using System.Reflection;
using Destructurama.Attributed;
using Serilog;
using Serilog.Events;

namespace Orion.Events {
    /// <summary>
    /// Represents a collection of event handlers.
    /// </summary>
    /// <typeparam name="TEvent">The type of event.</typeparam>
    internal sealed class EventHandlerCollection<TEvent> where TEvent : Event {
        private readonly string _eventName;
        private readonly LogEventLevel _eventLoggingLevel;

        private readonly ISet<Registration> _registrations =
            new SortedSet<Registration>(Comparer<Registration>.Create((r1, r2) => r1.Priority.CompareTo(r2.Priority)));
        private readonly IDictionary<Action<TEvent>, Registration> _handlerToRegistration =
            new Dictionary<Action<TEvent>, Registration>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerCollection{TEvent}"/> class.
        /// </summary>
        public EventHandlerCollection() {
            // Retrieve and cache information about the event. If `EventAttribute` is not present on the event, then
            // reasonable defaults are chosen.
            var attribute = typeof(TEvent).GetCustomAttribute<EventAttribute?>();
            _eventName = attribute?.Name ?? typeof(TEvent).Name;
            _eventLoggingLevel = attribute?.LoggingLevel ?? LogEventLevel.Information;
        }

        /// <summary>
        /// Registers an event <paramref name="handler"/> to the collection.
        /// </summary>
        /// <param name="handler">The event handler to register.</param>
        /// <param name="log">The logger to log to.</param>
        public void RegisterHandler(Action<TEvent> handler, ILogger log) {
            Debug.Assert(handler != null);
            Debug.Assert(log != null);

            var registration = new Registration(handler);
            _registrations.Add(registration);
            _handlerToRegistration[handler] = registration;

            // Not localized because this string is developer-facing.
            log.Debug("Registering {@Registration} to {EventName}", registration, _eventName);
        }

        /// <summary>
        /// Deregisters an event <paramref name="handler"/> from the collection. Returns a value indicating success.
        /// </summary>
        /// <param name="handler">The event handler to deregister.</param>
        /// <param name="log">The logger to log to.</param>
        /// <returns>
        /// <see langword="true"/> if the event handler was successfully deregistered; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public bool DeregisterHandler(Action<TEvent> handler, ILogger log) {
            Debug.Assert(handler != null);
            Debug.Assert(log != null);

            if (!_handlerToRegistration.TryGetValue(handler, out var registration)) {
                // Not localized because this string is developer-facing.
                log.Warning("Failed to deregister from {EventName}", _eventName);
                return false;
            }

            _registrations.Remove(registration);
            _handlerToRegistration.Remove(handler);

            // Not localized because this string is developer-facing.
            log.Debug("Deregistering {@Registration} from {EventName}", registration, _eventName);
            return true;
        }

        /// <summary>
        /// Raises <paramref name="evt"/> with the event handler collection.
        /// </summary>
        /// <param name="evt">The event to raise.</param>
        /// <param name="log">The logger to log to.</param>
        public void Raise(TEvent evt, ILogger log) {
            Debug.Assert(evt != null);
            Debug.Assert(log != null);

            // Not localized because this string is developer-facing.
            log.Write(_eventLoggingLevel, "Raising {EventName} with {@Event}", _eventName, evt);

            // Try casting the event as `ICancelable` and `IDirtiable`. This is a little hacky, but is better than
            // making `Event` implement `ICancelable` and `IDirtiable`.
            var cancelable = evt as ICancelable;
            var dirtiable = evt as IDirtiable;

            foreach (var registration in _registrations) {
                if (cancelable?.IsCanceled() == true && registration.IgnoreCanceled) {
                    continue;
                }

                try {
                    registration.Handler(evt);
                } catch (Exception ex) {
                    // Not localized because this string is developer-facing.
                    log.Error(ex, "Unhandled exception from {@Registration} in {EventName}", registration, _eventName);
                }
            }

            if (cancelable?.IsCanceled() == true) {
                // Not localized because this string is developer-facing.
                log.Write(_eventLoggingLevel, "Canceled {EventName} for {CancellationReason}", _eventName,
                          cancelable.CancellationReason);
            } else if (dirtiable?.IsDirty == true) {
                // Not localized because this string is developer-facing.
                log.Write(_eventLoggingLevel, "Altered {EventName} to {@Event}", _eventName, evt);
            }
        }

        /// <summary>
        /// Stores information about an event handler registration in an <see cref="EventHandlerCollection{TEvent}"/>.
        /// </summary>
        private sealed class Registration {
            /// <summary>
            /// Gets the event handler.
            /// </summary>
            /// <value>The event handler.</value>
            [NotLogged]
            public Action<TEvent> Handler { get; }

            /// <summary>
            /// Gets the event handler's priority.
            /// </summary>
            /// <value>The event handler's priority.</value>
            public EventPriority Priority { get; }

            /// <summary>
            /// Gets the event handler's name.
            /// </summary>
            /// <value>The event handler's name.</value>
            public string Name { get; }

            /// <summary>
            /// Gets a value indicating whether the event handler should ignore canceled events.
            /// </summary>
            /// <value>
            /// <see langword="true"/> if the event handler should ignore canceled events; otherwise,
            /// <see langword="false"/>.
            /// </value>
            public bool IgnoreCanceled { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Registration"/> class with the specified event
            /// <paramref name="handler"/>.
            /// </summary>
            /// <param name="handler">The event handler.</param>
            public Registration(Action<TEvent> handler) {
                Handler = handler;

                // Retrieve and cache information about the event handler. If `EventHandlerAttribute` is not present on
                // the event, then reasonable defaults are chosen.
                var attribute = handler.Method.GetCustomAttribute<EventHandlerAttribute?>();
                Priority = attribute?.Priority ?? EventPriority.Normal;
                Name = attribute?.Name ?? handler.Method.Name;
                IgnoreCanceled = attribute?.IgnoreCanceled ?? true;
            }
        }
    }
}
