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
            Populate();

            IContainer container = Builder.Build();

            return new Injector(container);
        }

        /// <summary>
        /// Configure the internal <see cref="ContainerBuilder"/>
        /// </summary>
        /// <param name="configurer"></param>
        /// <returns></returns>
        public InjectorBuilder Configure(Action<ContainerBuilder> configurer)
        {
            configurer(Builder);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementor"></typeparam>
        /// <returns></returns>
        public InjectorBuilder AddSingleton<TService, TImplementor>()
            where TService : notnull
            where TImplementor : notnull
        {
            Builder.RegisterType<TImplementor>().As<TService>().SingleInstance();
            return this;
        }

        /// <summary>
        /// Adds the given instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public InjectorBuilder AddInstance<T>(T instance)
            where T : class
        {
            Builder.RegisterInstance(instance);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TImplementor"></typeparam>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public InjectorBuilder AddHostedService<TImplementor>(ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TImplementor : IHostedService
        {
            Builder.RegisterType<TImplementor>().As<IHostedService>().ConfigureLifecycle(lifetime);

            return this;
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
