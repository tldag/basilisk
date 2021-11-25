using Autofac;
using Basilisk.Injection.Services;
using Basilisk.Injection.Support;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;

namespace Basilisk.Injection
{
    /// <summary>
    /// Injector builder.
    /// </summary>
    public class InjectorBuilder : ServiceCollection, IHostBuilder
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
        public virtual IInjector Build() => CreateInjector();

        /// <summary>
        /// Creates the injector. Called from either <see cref="Build"/> or <see cref="IHostBuilder.Build"/>
        /// </summary>
        /// <returns>The injector.</returns>
        protected virtual IInjector CreateInjector()
        {
            this.AddLogging();

            this.AddSingleton<IHostApplicationLifetime, ApplicationLifetime>(); // Required to allow IHost.RunAsync

            Builder.RegisterType<HostedServices>().As<IHostedServices>().SingleInstance();
            Builder.RegisterType<InjectorHost>().As<IHost>().SingleInstance();

            ServicePopulator.Create(Builder).Populate(this);

            IContainer container = Builder.Build();

            return new Injector(container);
        }

        // IHostBuilder

        /// <inheritdoc/>
        public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();

        /// <inheritdoc/>
        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            // TODO: implement
            // return this;
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            // TODO: implement
            // return this;
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            // TODO: implement
            // return this;
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
            where TContainerBuilder : notnull
        {
            // TODO: implement
            // return this;
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
            where TContainerBuilder : notnull
        {
            // TODO: implement
            // return this;
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            // TODO: implement
            // return this;
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        IHost IHostBuilder.Build() => CreateInjector();
    }
}
