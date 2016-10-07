using Ninject;
using System;
using System.Web.Http.Dependencies;

namespace Orion.Rest.Owin
{
	/// <summary>
	/// Implements a <see cref="IDependencyResolver"/> so that the <see cref="OwinRestService"/>
	/// uses Ninject as it's dependency resolver.
	/// </summary>
	public class OwinNinjectDependencyResolver : IDependencyResolver
	{
		private IKernel _kernel;

		/// <summary>
		/// Initializes a new instance of the <see cref="OwinNinjectDependencyResolver"/> class
		/// </summary>
		/// <param name="kernel">The master Ninject kernel instance</param>
		public OwinNinjectDependencyResolver(IKernel kernel)
		{
			_kernel = kernel;
		}

		/// <summary>
		/// Returns the current resolution scope
		/// </summary>
		/// <returns>The dependency scope</returns>
		public IDependencyScope BeginScope()
		{
			return this;
		}

		/// <inheritdoc />
		/// <exception cref="ObjectDisposedException"/>
		public object GetService(Type serviceType)
		{
			if (_kernel == null)
				throw new ObjectDisposedException(nameof(OwinNinjectDependencyResolver));

			return _kernel.TryGet(serviceType);
		}

		/// <inheritdoc />
		/// <exception cref="ObjectDisposedException"/>
		public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
		{
			if (_kernel == null)
				throw new ObjectDisposedException(nameof(OwinNinjectDependencyResolver));

			return _kernel.GetAll(serviceType);
		}

		/// <summary>
		/// Disposes of the current instance
		/// </summary>
		public void Dispose()
		{
			_kernel = null;
		}
	}
}
