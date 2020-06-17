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
using System.Linq;
using System.Reflection;
using Serilog;

namespace Orion.Core.Events
{
    /// <summary>
    /// Handles event handler registrations, deregistrations and event raising.
    /// </summary>
    public sealed class EventManager
    {
        private static readonly MethodInfo _registerHandler = typeof(EventManager).GetMethod(nameof(RegisterHandler))!;
        private static readonly MethodInfo _deregisterHandler = typeof(EventManager).GetMethod(nameof(DeregisterHandler))!;

        private readonly IDictionary<Type, object> _eventHandlerCollections = new Dictionary<Type, object>();

        private readonly IDictionary<object, IList<(Type eventType, object handler)>> _registrations =
            new Dictionary<object, IList<(Type, object)>>();

        /// <summary>
        /// Registers the given event <paramref name="handler"/> for events of type <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="handler">The event handler to register.</param>
        /// <param name="log">The log to log the registration to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handler"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public void RegisterHandler<TEvent>(Action<TEvent> handler, ILogger log) where TEvent : Event
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            var collection = GetEventHandlerCollection<TEvent>();
            collection.RegisterHandler(handler, log);
        }

        /// <summary>
        /// Registers all of the instance methods marked with <see cref="EventHandlerAttribute"/> as event handlers in
        /// the given <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The object whose instance methods should be registered.</param>
        /// <param name="log">The log to log the registrations to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public void RegisterHandlers(object obj, ILogger log)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            // Try to retrieve the existing registrations for `obj`. This allows consumers to call `RegisterHandlers` on
            // an object multiple times and get the expected behavior of multiple registrations.
            if (!_registrations.TryGetValue(obj, out var registrations))
            {
                registrations = new List<(Type eventType, object handler)>();
                _registrations[obj] = registrations;
            }

            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            foreach (var method in obj
                .GetType()
                .GetMethods(flags)
                .Where(m => m.GetCustomAttribute<EventHandlerAttribute>() != null))
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 1)
                {
                    // Not localized because this string is developer-facing.
                    log.Error("Skipping method {MethodName} as it does not have exactly one parameter", method.Name);
                    continue;
                }

                var eventType = parameters[0].ParameterType;
                if (!eventType.IsSubclassOf(typeof(Event)))
                {
                    // Not localized because this string is developer-facing.
                    log.Error("Skipping method {MethodName}: parameter type is not derived from `Event`", method.Name);
                    continue;
                }

                if (method.ReturnType != typeof(void))
                {
                    // Not localized because this string is developer-facing.
                    log.Error("Skipping method {MethodName}: return type is not `void`", method.Name);
                    continue;
                }

                var handlerType = typeof(Action<>).MakeGenericType(eventType);
                var handler = method.CreateDelegate(handlerType, obj);
                _registerHandler.MakeGenericMethod(eventType).Invoke(this, new object[] { handler, log });
                registrations.Add((eventType, handler));
            }
        }

        /// <summary>
        /// Deregisters the given event <paramref name="handler"/> for events of type <typeparamref name="TEvent"/>.
        /// Returns a value indicating success.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="handler">The event handler to deregister.</param>
        /// <param name="log">The log to log the deregistration to.</param>
        /// <returns>
        /// <see langword="true"/> if the event <paramref name="handler"/> was successfully deregistered; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handler"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public bool DeregisterHandler<TEvent>(Action<TEvent> handler, ILogger log) where TEvent : Event
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            var collection = GetEventHandlerCollection<TEvent>();
            return collection.DeregisterHandler(handler, log);
        }

        /// <summary>
        /// Deregisters all of the instance methods marked with <see cref="EventHandlerAttribute"/> as event handlers in
        /// the given <paramref name="obj"/>. Returns a value indicating success.
        /// </summary>
        /// <param name="obj">The object whose instance methods should be deregistered.</param>
        /// <param name="log">The log to log the registrations to.</param>
        /// <returns>
        /// <see langword="true"/> if the event handlers in <paramref name="obj"/> were successfully deregistered;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public bool DeregisterHandlers(object obj, ILogger log)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            if (!_registrations.TryGetValue(obj, out var registrations))
            {
                return false;
            }

            _registrations.Remove(obj);
            foreach (var (eventType, handler) in registrations)
            {
                _deregisterHandler.MakeGenericMethod(eventType).Invoke(this, new object[] { handler, log });
            }

            return true;
        }

        /// <summary>
        /// Raises the given <paramref name="evt"/>, executing all of the event handlers which apply to events of type
        /// <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="evt">The event to raise.</param>
        /// <param name="log">The log to log the raising to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="evt"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public void Raise<TEvent>(TEvent evt, ILogger log) where TEvent : Event
        {
            if (evt is null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            var collection = GetEventHandlerCollection<TEvent>();
            collection.Raise(evt, log);
        }

        private EventHandlerCollection<TEvent> GetEventHandlerCollection<TEvent>() where TEvent : Event
        {
            var type = typeof(TEvent);
            if (!_eventHandlerCollections.TryGetValue(type, out var collection))
            {
                collection = new EventHandlerCollection<TEvent>();
                _eventHandlerCollections[type] = collection;
            }

            return (EventHandlerCollection<TEvent>)collection;
        }
    }
}
