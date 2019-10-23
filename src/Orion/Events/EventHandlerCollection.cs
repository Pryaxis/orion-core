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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Orion.Utils;
using Serilog;

namespace Orion.Events {
    internal sealed class EventHandlerCollection<TEvent> where TEvent : Event {
        private readonly string _name;
        private readonly bool _isVerbose;

        // Registrations sorted by priority.
        private readonly ISet<Registration> _registrations =
            new SortedSet<Registration>(Comparer<Registration>.Create((r1, r2) => r1.Priority.CompareTo(r2.Priority)));

        private readonly IDictionary<Action<TEvent>, Registration> _handlerToRegistration =
            new Dictionary<Action<TEvent>, Registration>();

        public EventHandlerCollection(EventAttribute? attribute) {
            _name = attribute?.Name ?? typeof(TEvent).Name;
            _isVerbose = attribute?.IsVerbose ?? false;
        }

        public void RegisterHandler(Action<TEvent> handler, ILogger? log) {
            Debug.Assert(handler != null, "handler should not be null");

            var registration = new Registration(handler, log);
            _registrations.Add(registration);
            _handlerToRegistration[handler] = registration;

            // Not localized because this string is developer-facing.
            log?.Debug("Registered {RegistrationName} onto {EventName}", registration.Name, _name);
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types",
            Justification = "Catching Exception for fail-safe")]
        public void Raise(TEvent e, ILogger? log = null) {
            Debug.Assert(e != null, "event should not be null");

            log?.Debug("Raising {EventName} with {@Event}", _name, e);

            foreach (var registration in _registrations) {
                try {
                    registration.Handler(e);
                } catch (Exception ex) {
                    // Not localized because this string is developer-facing.
                    registration.Log?.Error(ex,
                        "Unhandled exception occurred from {RegistrationName} in {EventName}",
                        registration.Name, _name);
                }
            }

            var cancelable = e as ICancelable;
            var dirtiable = e as IDirtiable;
            if (cancelable?.IsCanceled() == true) {
                log?.Debug("Canceled {EventName} for {CancellationReason}", _name, cancelable.CancellationReason);
            } else if (dirtiable?.IsDirty == true) {
                log?.Debug("Altered {EventName} to   {@Event}", _name, e);
            }
        }

        public void UnregisterHandler(Action<TEvent> handler) {
            Debug.Assert(handler != null, "handler should not be null");

            if (!_handlerToRegistration.TryGetValue(handler, out var registration)) {
                return;
            }

            _registrations.Remove(registration);
            _handlerToRegistration.Remove(handler);
            
            // Not localized because this string is developer-facing.
            registration.Log?.Debug("Unregistered {RegistrationName} from {EventName}", registration.Name, _name);
        }

        private sealed class Registration {
            public Action<TEvent> Handler { get; }
            public ILogger? Log { get; }
            public EventPriority Priority { get; }
            public string Name { get; }

            public Registration(Action<TEvent> handler, ILogger? log) {
                Handler = handler;
                Log = log;

                var attribute = handler.Method.GetCustomAttribute<EventHandlerAttribute?>();
                Priority = attribute?.Priority ?? EventPriority.Normal;
                Name = attribute?.Name ?? handler.Method.Name;
            }
        }
    }
}
