using System;

namespace Orion
{
	public abstract class OrionPlugin : IDisposable
	{
		public abstract Version Version { get; }
		public abstract string Name { get; }
		public abstract string Author { get; }
		public abstract int Order { get; }

		protected Orion Orion { get; private set; }

		public abstract void Initialize();

		protected OrionPlugin(Orion orion)
		{
			Orion = orion;
		}

		~OrionPlugin()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected abstract void Dispose(bool disposing);
	}
}