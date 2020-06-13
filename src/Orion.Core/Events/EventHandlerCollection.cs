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

namespace Orion.Core.Events {
    // A collection of event handlers sorted by priority.
    internal sealed class EventHandlerCollection<TEvent> where TEvent : Event {
        private readonly string _eventName;
        private readonly LogEventLevel _eventLoggingLevel;

        private readonly ISet<Registration> _registrations =
            new SortedSet<Registration>(Comparer<Registration>.Create((r1, r2) => r1.Priority.CompareTo(r2.Priority)));
        private readonly IDictionary<Action<TEvent>, Registration> _handlerToRegistration =
            new Dictionary<Action<TEvent>, Registration>();

        public EventHandlerCollection() {
            // Retrieve and cache information about the event. If `EventAttribute` is not present on the event, then
            // reasonable defaults are chosen.
            var attribute = typeof(TEvent).GetCustomAttribute<EventAttribute?>();
            _eventName = attribute?.Name ?? typeof(TEvent).Name;
            _eventLoggingLevel = attribute?.LoggingLevel ?? LogEventLevel.Information;
        }

        public void RegisterHandler(Action<TEvent> handler, ILogger log) {
            Debug.Assert(handler != null);
            Debug.Assert(log != null);

            var registration = _handlerToRegistration[handler] = new Registration(handler);
            _registrations.Add(registration);

            // Not localized because this string is developer-facing.
            log.Debug("Registering {EventName} with {@Registration}", _eventName, registration);
        }

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
            log.Debug("Deregistering {EventName} with {@Registration}", _eventName, registration);
            return true;
        }

        public void Raise(TEvent evt, ILogger log) {
            Debug.Assert(evt != null);
            Debug.Assert(log != null);

            // Not localized because this string is developer-facing.
            log.Write(_eventLoggingLevel, "Raising {EventName} with {@Event}", _eventName, evt);

            foreach (var registration in _registrations) {
                if (evt.IsCanceled && registration.IgnoreCanceled) {
                    continue;
                }

                try {
                    registration.Handler(evt);
                } catch (Exception ex) {
                    // Not localized because this string is developer-facing.
                    log.Error(ex, "Unhandled exception from {@Registration} in {EventName}", registration, _eventName);
                }
            }

            if (evt.IsCanceled) {
                // Not localized because this string is developer-facing.
                log.Write(_eventLoggingLevel, "Canceled {EventName} for {CancellationReason}", _eventName,
                          evt.CancellationReason);
            }
        }

        private sealed class Registration {
            [NotLogged] public Action<TEvent> Handler { get; }
            public EventPriority Priority { get; }
            public string Name { get; }
            public bool IgnoreCanceled { get; }

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
