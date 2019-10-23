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
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Destructurama.Attributed;
using Orion.Utils;
using Serilog;
using Serilog.Events;

namespace Orion.Events {
    internal sealed class EventHandlerCollection<TEvent> where TEvent : Event {
        private readonly string _name;
        private readonly LogEventLevel _logLevel;

        // Registrations sorted by priority.
        private readonly ISet<Registration> _registrations =
            new SortedSet<Registration>(Comparer<Registration>.Create((r1, r2) => r1.Priority.CompareTo(r2.Priority)));

        private readonly IDictionary<Action<TEvent>, Registration> _handlerToRegistration =
            new Dictionary<Action<TEvent>, Registration>();

        public EventHandlerCollection() {
            var attribute = typeof(TEvent).GetCustomAttribute<EventAttribute?>();
            _name = attribute?.Name ?? typeof(TEvent).Name;
            _logLevel = (attribute?.IsVerbose ?? false) ? LogEventLevel.Verbose : LogEventLevel.Debug;
        }

        public void RegisterHandler(Action<TEvent> handler, ILogger log) {
            Debug.Assert(handler != null, "handler should not be null");
            Debug.Assert(log != null, "log should not be null");

            var registration = new Registration(handler);
            _registrations.Add(registration);
            _handlerToRegistration[handler] = registration;

            // Not localized because this string is developer-facing.
            log.Debug("Registering {EventName} with {@Registration}", _name, registration);
        }

        [SuppressMessage(
            "Design", "CA1031:Do not catch general exception types",
            Justification = "Catching Exception for fail-safe")]
        public void Raise(TEvent e, ILogger log) {
            Debug.Assert(e != null, "event should not be null");
            Debug.Assert(log != null, "log should not be null");

            // Not localized because this string is developer-facing.
            log.Write(_logLevel, "Raising {EventName} with {@Event}", _name, e);

            var cancelable = e as ICancelable;
            foreach (var registration in _registrations) {
                if (cancelable?.IsCanceled() == true && registration.IgnoreCanceled) {
                    continue;
                }

                try {
                    registration.Handler(e);
                } catch (Exception ex) {
                    // Not localized because this string is developer-facing.
                    log.Error(ex,
                        "Unhandled exception occurred from {RegistrationName} in {EventName}",
                        registration.Name, _name);
                }
            }

            var dirtiable = e as IDirtiable;
            if (cancelable?.IsCanceled() == true) {
                // Not localized because this string is developer-facing.
                log.Write(_logLevel,
                    "Canceled {EventName} for {CancellationReason}",
                    _name, cancelable.CancellationReason);
            } else if (dirtiable?.IsDirty == true) {
                // Not localized because this string is developer-facing.
                log.Write(_logLevel, "Altered {EventName} to {@Event}", _name, e);
            }
        }

        public void UnregisterHandler(Action<TEvent> handler, ILogger log) {
            Debug.Assert(handler != null, "handler should not be null");
            Debug.Assert(log != null, "log should not be null");

            if (!_handlerToRegistration.TryGetValue(handler, out var registration)) {
                return;
            }

            _registrations.Remove(registration);
            _handlerToRegistration.Remove(handler);

            // Not localized because this string is developer-facing.
            log.Debug("Unregistering {EventName} with {@Registration}", _name, registration);
        }

        private sealed class Registration {
            [NotLogged]
            public Action<TEvent> Handler { get; }

            public EventPriority Priority { get; }
            public bool IgnoreCanceled { get; }
            public string Name { get; }

            public Registration(Action<TEvent> handler) {
                Handler = handler;

                var attribute = handler.Method.GetCustomAttribute<EventHandlerAttribute?>();
                Priority = attribute?.Priority ?? EventPriority.Normal;
                IgnoreCanceled = attribute?.IgnoreCanceled ?? false;
                Name = attribute?.Name ?? handler.Method.Name;
            }
        }
    }
}
