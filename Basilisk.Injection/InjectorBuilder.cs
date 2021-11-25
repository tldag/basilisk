using Autofac;
using Basilisk.Injection.Services;
using Basilisk.Injection.Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
        public ContainerBuilder Builder { get; } = new();

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
            Populate();

            IContainer container = Builder.Build();

            return new Injector(container);
        }

        /// <summary>
        /// Populates the builder with the services.
        /// </summary>
        protected virtual void Populate()
        {
            ServicePopulator.Create(Builder).Populate(this);

            Builder.RegisterType<HostedServices>().As<IHostedServices>().SingleInstance();
            Builder.RegisterType<InjectorHost>().As<IHost>().SingleInstance();
        }
    }
}
