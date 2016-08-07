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
		/// Gets or sets the service's author. Defaults to "Anonymous".
		/// </summary>
		public string Author { get; set; } = "Anonymous";

		/// <summary>
		/// Gets the service's name.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceAttribute"/> class.
		/// </summary>
		/// <param name="name">The service's name.</param>
		public ServiceAttribute(string name)
		{
			Name = name;
		}
	}
}
