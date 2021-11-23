using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace Basilisk.Injection
{
    /// <summary>
    /// ServiceScopeFactory. Modeled after internal class <c>AutofacServiceScopeFactory</c>.
    /// </summary>
    public class ServiceScopeFactory : IServiceScopeFactory
    {
        /// <inheritdoc/>
        protected ILifetimeScope LifetimeScope { get; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="lifetimeScope">Enclosing scope.</param>
        public ServiceScopeFactory(ILifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope;
        }

        /// <inheritdoc/>
        public IServiceScope CreateScope()
        {
            return new ServiceScope(LifetimeScope);
        }
    }
}
