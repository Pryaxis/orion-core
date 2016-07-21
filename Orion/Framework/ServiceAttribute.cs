using System;

namespace Orion.Framework
{
	/// <summary>
	/// Provides information about a service.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ServiceAttribute : Attribute
	{
		/// <summary>
		/// Gets the service name.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Gets or sets the service author. Defaults to "Anonymous".
		/// </summary>
		public string Author { get; set; } = "Anonymous";

		/// <summary>
		/// Gets or sets the service version. Defaults to "1.0.0".
		/// </summary>
		public string Version { get; set; } = "1.0.0";

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceAttribute"/> class.
		/// </summary>
		/// <param name="name">The service name.</param>
		public ServiceAttribute(string name)
		{
			Name = name;
		}
	}
}
