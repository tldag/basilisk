using Autofac;
using Basilisk.Injection.Services;
using Basilisk.Injection.Support;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Basilisk.Injection.Support.ContentRootPathHelpers;

namespace Basilisk.Injection
{
    /// <summary>
    /// Injector builder.
    /// </summary>
    public class InjectorBuilder : ServiceCollection, IInjectorBuilder
    {
        private IInjectorBuilderFactory? factory = null;

        /// <summary>
        /// The factory used to create various elements.
        /// </summary>
        protected IInjectorBuilderFactory Factory { get => factory ??= CreateInjectorBuilderFactory(); }

        private IInjectorBuilderContext? context = null;

        /// <summary>
        /// The context used to create various elements.
        /// </summary>
        protected IInjectorBuilderContext Context { get => context ??= Factory.CreateContext(); }

        /// <summary>
        /// The Autofac builder.
        /// </summary>
        public ContainerBuilder ContainerBuilder { get => Context.ContainerBuilder; }

        /// <summary>
        /// Protected c'tor. Use <see cref="Create"/> or create a sub-class.
        /// </summary>
        protected InjectorBuilder() { }

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        /// <returns>A new builder.</returns>
        public static IInjectorBuilder Create() => new InjectorBuilder();

        /// <summary>
        /// Builds the injector.
        /// </summary>
        /// <returns>The injector.</returns>
        public virtual IInjector Build() => CreateInjector();

        /// <summary>
        /// Creates the factory used to create various elements.
        /// </summary>
        /// <returns></returns>
        protected virtual IInjectorBuilderFactory CreateInjectorBuilderFactory()
            => new InjectorBuilderFactory();

        /// <summary>
        /// Creates the injector. Called from either <see cref="Build"/> or <see cref="IHostBuilder.Build"/>
        /// </summary>
        /// <returns>The injector.</returns>
        protected virtual IInjector CreateInjector()
        {
            HostBuilderContext hostBuilderContext = Context.HostBuilderContext;
            IConfiguration appConfiguration = Context.AppConfiguration;

            hostBuilderContext.Configuration = appConfiguration;

            this.AddInstance(hostBuilderContext);
            this.AddInstance(appConfiguration);

            this.AddLogging();
            this.AddInstance(Context.HostBuilderContext);
            this.AddSingleton<IHostApplicationLifetime, ApplicationLifetime>();

            ContainerBuilder.RegisterType<HostedServices>().As<IHostedServices>().SingleInstance();
            ContainerBuilder.RegisterType<InjectorHost>().As<IHost>().SingleInstance();

            Context.Services.ToList().ForEach(d => this.Add(d));
            ServicePopulator.Create(ContainerBuilder).Populate(this);

            IContainer container = ContainerBuilder.Build();

            return new Injector(container);
        }

        // IHostBuilder

        /// <inheritdoc/>
        public IDictionary<object, object> Properties { get => Context.Properties; }

        /// <inheritdoc/>
        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        { Context.AddHostConfigurer(configureDelegate); return this; }

        /// <inheritdoc/>
        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        { Context.AddAppConfigurer(configureDelegate); return this; }

        /// <inheritdoc/>
        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        { Context.AddServiceConfigurer(configureDelegate); return this; }

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
