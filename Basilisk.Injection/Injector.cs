using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;

namespace Basilisk.Injection
{
    /// <summary>
    /// Injector
    /// </summary>
    public class Injector : IInjector, IDisposable
    {
        /// <summary>
        /// The service provider.
        /// </summary>
        protected AutofacServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Constructs a new injector
        /// </summary>
        /// <param name="container">The Autofac container</param>
        public Injector(IContainer container)
        {
            ServiceProvider = new(container);
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~Injector()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool _)
        {
            ServiceProvider.Dispose();
        }

        /// <inheritdoc/>
        public object? GetService(Type serviceType)
            => ServiceProvider.GetService(serviceType);

        /// <inheritdoc/>
        public object GetRequiredService(Type serviceType)
            => ServiceProvider.GetRequiredService(serviceType);

        /// <inheritdoc/>
        public bool IsService(Type serviceType)
            => ServiceProvider.IsService(serviceType);
    }
}
