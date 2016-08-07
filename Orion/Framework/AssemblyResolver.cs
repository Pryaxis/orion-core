using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework
{
	public static class AssemblyResolver
	{

		/// <summary>
		/// Enumerates all the assemvlies in the provided paths and loads them into the current
		/// AppDomain.
		/// </summary>
		/// <param name="paths">
		/// The paths relative to Orion's working directory or absolute, to load .NET assemblies from.
		/// </param>
		public static IEnumerable<Assembly> LoadAssemblies(params string[] paths)
		{
			foreach (string path in paths)
			{
				foreach (string assemblyPath in Directory.EnumerateFiles(path, "*.dll"))
				{
					Assembly assembly;
					try
					{
						assembly = Assembly.LoadFrom(assemblyPath);
					}
					catch (BadImageFormatException)
					{
						continue;
					}

					yield return assembly;
				}
			}
		}
	}
}
