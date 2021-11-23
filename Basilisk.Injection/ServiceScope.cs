using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Basilisk.Injection
{
    /// <summary>
    /// ServiceScope. Modeled after internal <c>AutofacServiceScope</c>.
    /// </summary>
    public class ServiceScope : IServiceScope, IDisposable
    {
        private readonly AutofacServiceProvider serviceProvider;

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider => serviceProvider;

        /// <summary>
        /// C'tor.
        /// </summary>
        /// <param name="lifetimeScope">Enclosing scope</param>
        public ServiceScope(ILifetimeScope lifetimeScope)
        {
            serviceProvider = new AutofacServiceProvider(lifetimeScope);
        }

        /// <summary>
        /// D'tor.
        /// </summary>
        ~ServiceScope()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool _)
        {
            serviceProvider.Dispose();
        }
    }
}
