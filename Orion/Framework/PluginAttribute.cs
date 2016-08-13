using System;

namespace Orion.Framework
{
	/// <summary>
	/// Provides information about a plugin.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PluginAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the plugin's author.
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// Gets the plugin's name.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PluginAttribute"/> class.
		/// </summary>
		/// <param name="name">The plugin's name.</param>
		public PluginAttribute(string name)
		{
			Name = name;
		}
	}
}
