using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;

namespace Basilisk.Injection
{
    /// <summary>
    /// Injector builder.
    /// </summary>
    public class InjectorBuilder : ServiceCollection
    {
        /// <summary>
        /// The Autofac builder.
        /// </summary>
        protected ContainerBuilder Builder { get; } = new();

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        /// <returns>A new builder.</returns>
        public static InjectorBuilder Create() => new();

        /// <summary>
        /// Builds the injector.
        /// </summary>
        /// <returns>The injector.</returns>
        public virtual IInjector Build()
        {
            Builder.Populate(this);

            IContainer container = Builder.Build();

            return new Injector(container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementor"></typeparam>
        /// <returns></returns>
        public InjectorBuilder RegisterSingleton<TService, TImplementor>()
            where TService : notnull
            where TImplementor : notnull
        {
            Builder.RegisterType<TImplementor>().As<TService>().SingleInstance();

            return this;
        }

        /// <summary>
        /// Returns the service descriptors implementing <c>IHostedService</c>.
        /// </summary>
        /// <returns>Service descriptors.</returns>
        protected virtual IEnumerable<ServiceDescriptor> GetHostedServices()
        {
            return this.Where(sd => sd.ServiceType.IsAssignableTo<IHostedService>());
        }
    }
}
