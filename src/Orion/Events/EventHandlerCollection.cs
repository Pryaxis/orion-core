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
using System.Linq;
using System.Reflection;
using Orion.Properties;
using Serilog;

namespace Orion.Events {
    /// <summary>
    /// Represents a collection of event handlers. Provides the ability to register and unregister event handlers. This
    /// class is thread-safe.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of event arguments.</typeparam>
    public sealed class EventHandlerCollection<TEventArgs> where TEventArgs : EventArgs {
        private static readonly string _eventName = InitializeEventName();

        private readonly object _lock = new object();
        private readonly ILogger _log;

        private readonly ISet<Registration> _registrations =
            new SortedSet<Registration>(Comparer<Registration>.Create((r1, r2) => r1.Priority.CompareTo(r2.Priority)));

        private readonly IDictionary<EventHandler<TEventArgs>, Registration> _handlerToRegistration =
            new Dictionary<EventHandler<TEventArgs>, Registration>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerCollection{TEventArgs}"/> class with the specified
        /// log.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <exception cref="ArgumentNullException"><paramref name="log"/> is <see langword="null"/>.</exception>
        public EventHandlerCollection(ILogger log) {
            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            _log = log;
        }
        
        private static string InitializeEventName() {
            var attribute = typeof(TEventArgs).GetCustomAttribute<EventArgsAttribute?>();

            // We're assuming that TEventArgs actually ends in "EventArgs", in which case we just chop that off.
            return attribute?.Name ?? typeof(TEventArgs).Name[0..^9];
        }


        /// <summary>
        /// Invokes the collection of handlers in order of their priorities using the given <paramref name="sender"/>
        /// and <paramref name="args"/>.
        /// 
        /// <para/>
        /// 
        /// While this method itself is thread-safe, care must be taken to make the handlers thread-safe if this method
        /// is expected to be called simultaneously on different threads.
        /// </summary>
        /// <param name="sender">The sender. This is the object that is initiating the event.</param>
        /// <param name="args">The event arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is <see langword="null"/>.</exception>
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
                _log.Debug(Resources.EventHandlerCollection_Invoke, _eventName, registration.Name, args);

                try {
                    registration.Handler(sender, args);
                } catch (Exception ex) {
                    _log.Error(ex, Resources.EventHandlerCollection_InvokeException, _eventName);
                }
            }

            if (args is ICancelable cancelable) {
                if (cancelable.IsCanceled()) {
                    var cancellationReason = cancelable.CancellationReason;
                    _log.Debug(Resources.EventHandlerCollection_InvokeCanceled, _eventName, cancellationReason);
                }
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

            var registration = new Registration(handler);
            lock (_lock) {
                _handlerToRegistration[handler] = registration;
                _registrations.Add(registration);
            }

            _log.Debug(Resources.EventHandlerCollection_Register, _eventName, registration.Name);
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

            Registration? registration = null;
            var result = false;
            lock (_lock) {
                if (!_handlerToRegistration.TryGetValue(handler, out registration)) {
                    return false;
                }

                result = _handlerToRegistration.Remove(handler) & _registrations.Remove(registration);
            }
            
            _log.Debug(Resources.EventHandlerCollection_Unregister, _eventName, registration.Name);
            return result;
        }

        // Keeps track of handlers along with their priorities.
        private class Registration {
            private readonly EventHandlerAttribute? _attribute;

            public EventHandler<TEventArgs> Handler { get; }
            public EventPriority Priority => _attribute?.Priority ?? EventPriority.Normal;
            public string Name => _attribute?.Name ?? Handler.Method.Name;

            public Registration(EventHandler<TEventArgs> handler) {
                Handler = handler;
                _attribute = handler.Method.GetCustomAttribute<EventHandlerAttribute?>();
            }
        }
    }
}
