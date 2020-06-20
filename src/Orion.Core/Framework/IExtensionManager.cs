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
using System.Reflection;

namespace Orion.Core.Framework
{
    /// <summary>
    /// Manages extensions (services and plugins).
    /// </summary>
    public interface IExtensionManager
    {
        /// <summary>
        /// Gets a mapping from plugin names to currently loaded plugins.
        /// </summary>
        /// <value>A mapping from plugin names to currently loaded plugins.</value>
        IReadOnlyDictionary<string, OrionExtension> Plugins { get; }

        /// <summary>
        /// Loads extensions from the given <paramref name="assembly"/>. This includes service definitions/bindings and
        /// plugins.
        /// </summary>
        /// <param name="assembly">The assembly to load from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
        void Load(Assembly assembly);

        /// <summary>
        /// Initializes the loaded extensions.
        /// </summary>
        void Initialize();

        // TODO: implement unloading of assemblies.
    }
}
