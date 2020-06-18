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
using Orion.Core.Events.Server;
using Orion.Core.Framework.Events;
using Orion.Core.Framework.Extensions;
using Serilog;

namespace Orion.Core
{
    /// <summary>
    /// Represents Orion's core logic. Provides access to Orion extensions and events and publishes server-related
    /// events.
    /// </summary>
    /// <remarks>
    /// The Orion kernel is responsible for publishing the following server-related events:
    /// <list type="bullet">
    /// <item><description><see cref="ServerInitializeEvent"/></description></item>
    /// <item><description><see cref="ServerStartEvent"/></description></item>
    /// <item><description><see cref="ServerTickEvent"/></description></item>
    /// <item><description><see cref="ServerCommandEvent"/></description></item>
    /// </list>
    /// </remarks>
    public sealed class OrionKernel : IDisposable
    {
        private readonly ILogger _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionKernel"/> class with the specified <paramref name="log"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <exception cref="ArgumentNullException"><paramref name="log"/> is <see langword="null"/>.</exception>
        public OrionKernel(ILogger log)
        {
            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _log = log.ForContext("Name", "orion-kernel");

            // Load from this assembly so that we load Orion's service interfaces and service bindings.
            Extensions = new ExtensionManager(this, log);
            Extensions.Load(typeof(OrionKernel).Assembly);

            OTAPI.Hooks.Game.PreInitialize = PreInitializeHandler;
            OTAPI.Hooks.Game.Started = StartedHandler;
            OTAPI.Hooks.Game.PreUpdate = PreUpdateHandler;
            OTAPI.Hooks.Command.Process = ProcessHandler;
        }

        /// <summary>
        /// Gets the kernel's extension manager.
        /// </summary>
        /// <value>The kernel's extension manager.</value>
        public ExtensionManager Extensions { get; }

        /// <summary>
        /// Gets the kernel's event manager.
        /// </summary>
        /// <value>The kernel's event manager.</value>
        public EventManager Events { get; } = new EventManager();

        /// <summary>
        /// Disposes the kernel, releasing any resources associated with it.
        /// </summary>
        public void Dispose()
        {
            Extensions.Dispose();

            OTAPI.Hooks.Game.PreInitialize = null;
            OTAPI.Hooks.Game.Started = null;
            OTAPI.Hooks.Game.PreUpdate = null;
            OTAPI.Hooks.Command.Process = null;
        }

        // =============================================================================================================
        // OTAPI hooks
        //

        private void PreInitializeHandler()
        {
            var evt = new ServerInitializeEvent();
            Events.Raise(evt, _log);
        }

        private void StartedHandler()
        {
            var evt = new ServerStartEvent();
            Events.Raise(evt, _log);
        }

        private void PreUpdateHandler(ref Microsoft.Xna.Framework.GameTime gameTime)
        {
            var evt = new ServerTickEvent();
            Events.Raise(evt, _log);
        }

        private OTAPI.HookResult ProcessHandler(string lowered, string input)
        {
            var evt = new ServerCommandEvent(input);
            Events.Raise(evt, _log);
            return evt.IsCanceled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }
    }
}
