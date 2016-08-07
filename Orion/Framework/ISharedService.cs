using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework
{
	public interface ISharedService
	{
		/// <summary>
		/// Gets the service author.
		/// </summary>
		string Author { get; }

		/// <summary>
		/// Gets the service name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the service version.
		/// </summary>
		Version Version { get; }
	}
}
