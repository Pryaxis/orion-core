using System;
using System.Reflection;

namespace Orion.Framework
{
	/// <summary>
	/// Provides the base class for a shared service.
	/// </summary>
	public abstract class SharedService : ISharedService
	{
		/// <inheritdoc/>
		public string Author { get; }

		/// <inheritdoc/>
		public string Name { get; }

		/// <summary>
		/// Gets the Orion instance that this shared service belongs to.
		/// </summary>
		protected Orion Orion { get; }

		/// <inheritdoc/>
		public Version Version { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SharedService"/> class.
		/// </summary>
		/// <param name="orion">The Orion instance that this shared service belongs to.</param>
		protected SharedService(Orion orion)
		{
			Orion = orion;

			Type type = GetType();
			var attribute = type.GetCustomAttribute<ServiceAttribute>();
			Author = attribute?.Author ?? "Anonymous";
			Name = attribute?.Name ?? type.Name;
			Version = Assembly.GetAssembly(type).GetName().Version;
		}
	}
}
