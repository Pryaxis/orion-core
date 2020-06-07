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

namespace Orion.Framework {
    /// <summary>
    /// Specifies information about a plugin.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class PluginAttribute : Attribute {
        private string _author = "Anonymous";

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginAttribute"/> class with the specified plugin
        /// <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The plugin name.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        public PluginAttribute(string name) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Gets the plugin name.
        /// </summary>
        /// <value>The plugin name.</value>
        /// <remarks>The naming convention for plugins is <c>kebab-case</c>.</remarks>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the plugin's author.
        /// </summary>
        /// <value>The plugin's author. The default value is <c>Anonymous</c>.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Author {
            get => _author;
            set => _author = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
