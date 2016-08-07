using System;
using System.Reflection;

namespace Orion.Framework
{
	/// <summary>
	/// Base class which describes a shared service.
	/// </summary>
	/// <remarks>
	/// Shared services live in singleton space and are instantiated once and passed
	/// around to all plugins and services who request them.  Therefore, they are stateless
	/// and must contain no instance-specific state.
	/// </remarks>
	public abstract class SharedService : ISharedService
	{
		/// <inheritdoc/>
		public string Author { get; } = "Anonymous";

		/// <inheritdoc/>
		public string Name { get; } = "Unnamed";

		/// <summary>
		/// Gets the parent <see cref="Orion"/> instance.
		/// </summary>
		protected Orion Orion { get; }

		/// <inheritdoc/>
		public virtual Version Version { get; } = new Version(1, 0, 0);

		/// <summary>
		/// Initializes a new instance of the <see cref="SharedService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		/// <remarks>
		/// This constructor populates the <see cref="Name"/> and <see cref="Author"/> properties if the
		/// <see cref="ServiceAttribute"/> attribute is on the derived class.
		/// </remarks>
		protected SharedService(Orion orion)
		{
			Orion = orion;

			var attr = GetType().GetCustomAttribute<ServiceAttribute>();
			if (attr != null)
			{
				Author = attr.Author;
				Name = attr.Name;
			}
		}
	}
}
