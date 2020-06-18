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
using System.Threading.Tasks;
using Orion.Core.Events;
using Serilog;

namespace Orion.Core.Framework.Events
{
    /// <summary>
    /// Handles event handler registrations, deregistrations, and event raisings.
    /// </summary>
    public sealed class EventManager
    {
        private static readonly MethodInfo _registerHandler =
            typeof(EventManager).GetMethod(nameof(RegisterHandler))!;
        private static readonly MethodInfo _registerAsyncHandler =
            typeof(EventManager).GetMethod(nameof(RegisterAsyncHandler))!;
        private static readonly MethodInfo _deregisterHandler =
            typeof(EventManager).GetMethod(nameof(DeregisterHandler))!;
        private static readonly MethodInfo _deregisterAsyncHandler =
            typeof(EventManager).GetMethod(nameof(DeregisterAsyncHandler))!;

        private readonly IDictionary<Type, object> _eventHandlerCollections = new Dictionary<Type, object>();
        private readonly IDictionary<object, IList<(Type eventType, object handler)>> _registrations =
            new Dictionary<object, IList<(Type, object)>>();
        private readonly IDictionary<object, IList<(Type eventType, object handler)>> _asyncRegistrations =
            new Dictionary<object, IList<(Type, object)>>();

        internal EventManager() { }

        /// <summary>
        /// Registers the given synchronous event <paramref name="handler"/> for events of type
        /// <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="handler">The synchronous event handler to register.</param>
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
        /// Registers the given asynchronous event <paramref name="handler"/> for events of type
        /// <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="handler">The asynchronous event handler to register.</param>
        /// <param name="log">The log to log the registration to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handler"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public void RegisterAsyncHandler<TEvent>(Func<TEvent, Task> handler, ILogger log) where TEvent : Event
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
            collection.RegisterAsyncHandler(handler, log);
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

            var registrations = GetOrCreateRegistrations();
            var asyncRegistrations = GetOrCreateAsyncRegistrations();
            foreach (var method in obj
                .GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.GetCustomAttribute<EventHandlerAttribute>() != null))
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 1)
                {
                    // Not localized because this string is developer-facing.
                    log.Error("Skipping method {Method} as it does not have exactly one parameter", method);
                    continue;
                }

                var eventType = parameters[0].ParameterType;
                if (!eventType.IsSubclassOf(typeof(Event)))
                {
                    // Not localized because this string is developer-facing.
                    log.Error("Skipping method {Method}: parameter type is not derived from `Event`", method);
                    continue;
                }

                var returnType = method.ReturnType;
                if (returnType == typeof(void))
                {
                    RegisterHandler();
                }
                else if (returnType == typeof(Task))
                {
                    RegisterAsyncHandler();
                }
                else
                {
                    // Not localized because this string is developer-facing.
                    log.Error("Skipping method {Method}: return type is {ReturnType}", method, returnType);
                }

                void RegisterHandler()
                {
                    var handlerType = typeof(Action<>).MakeGenericType(eventType);
                    var handler = method.CreateDelegate(handlerType, obj);
                    _registerHandler.MakeGenericMethod(eventType).Invoke(this, new object[] { handler, log });
                    registrations.Add((eventType, handler));
                }

                void RegisterAsyncHandler()
                {
                    var handlerType = typeof(Func<,>).MakeGenericType(eventType, typeof(Task));
                    var handler = method.CreateDelegate(handlerType, obj);
                    _registerAsyncHandler.MakeGenericMethod(eventType).Invoke(this, new object[] { handler, log });
                    asyncRegistrations.Add((eventType, handler));
                }
            }

            IList<(Type eventType, object handler)> GetOrCreateRegistrations()
            {
                if (!_registrations.TryGetValue(obj, out var registrations))
                {
                    registrations = new List<(Type, object)>();
                    _registrations[obj] = registrations;
                }

                return registrations;
            }

            IList<(Type eventType, object handler)> GetOrCreateAsyncRegistrations()
            {
                if (!_asyncRegistrations.TryGetValue(obj, out var asyncRegistrations))
                {
                    asyncRegistrations = new List<(Type, object)>();
                    _asyncRegistrations[obj] = asyncRegistrations;
                }

                return asyncRegistrations;
            }
        }

        /// <summary>
        /// Deregisters the given synchronous event <paramref name="handler"/> for events of type
        /// <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="handler">The synchronous event handler to deregister.</param>
        /// <param name="log">The log to log the deregistration to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handler"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public void DeregisterHandler<TEvent>(Action<TEvent> handler, ILogger log) where TEvent : Event
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
            collection.DeregisterHandler(handler, log);
        }

        /// <summary>
        /// Deregisters the given asynchronous event <paramref name="handler"/> for events of type
        /// <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="handler">The asynchronous event handler to deregister.</param>
        /// <param name="log">The log to log the deregistration to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handler"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public void DeregisterAsyncHandler<TEvent>(Func<TEvent, Task> handler, ILogger log) where TEvent : Event
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
            collection.DeregisterAsyncHandler(handler, log);
        }

        /// <summary>
        /// Deregisters all of the instance methods marked with <see cref="EventHandlerAttribute"/> as event handlers in
        /// the given <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The object whose instance methods should be deregistered.</param>
        /// <param name="log">The log to log the deregistrations to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public void DeregisterHandlers(object obj, ILogger log)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            DeregisterHandlers();
            DeregisterAsyncHandlers();

            void DeregisterHandlers()
            {
                if (_registrations.TryGetValue(obj, out var registrations))
                {
                    _registrations.Remove(obj);
                    foreach (var (eventType, handler) in registrations)
                    {
                        _deregisterHandler.MakeGenericMethod(eventType).Invoke(this, new object[] { handler, log });
                    }
                }
            }

            void DeregisterAsyncHandlers()
            {
                if (_asyncRegistrations.TryGetValue(obj, out var asyncRegistrations))
                {
                    _asyncRegistrations.Remove(obj);
                    foreach (var (eventType, handler) in asyncRegistrations)
                    {
                        _deregisterAsyncHandler.MakeGenericMethod(eventType).Invoke(this, new object[] { handler, log });
                    }
                }
            }
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
