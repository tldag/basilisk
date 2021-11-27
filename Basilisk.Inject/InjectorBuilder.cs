using Basilisk.Inject.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Basilisk.Inject
{
    /// <summary>
    /// Injector builder.
    /// </summary>
    public abstract class InjectorBuilder : ServiceCollection, IInjectorBuilder
    {
        /// <inheritdoc/>
        public abstract IHostConfig HostConfig { get; }

        /// <inheritdoc/>
        public abstract IAppConfig AppConfig { get; }

        /// <inheritdoc/>
        public abstract ILogConfig LogConfig { get; }

        /// <inheritdoc/>
        public abstract IAutofacConfig AutofacConfig { get; }

        /// <inheritdoc/>
        public abstract IHostServices HostServices { get; }

        /// <summary>
        /// Protected c'tor. Use <see cref="Create"/> or create a sub-class.
        /// </summary>
        protected InjectorBuilder() { }

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        /// <returns>A new builder.</returns>
        public static IInjectorBuilder Create() => new Implementation.InjectorBuilder();

        // Build

        /// <summary>
        /// Creates the injector.
        /// </summary>
        /// <returns></returns>
        protected abstract IInjector BuildInjector();

        /// <summary>
        /// Builds the injector.
        /// </summary>
        /// <returns>The injector.</returns>
        public virtual IInjector Build() => BuildInjector();

        /// <inheritdoc/>
        IHost IHostBuilder.Build() => BuildInjector();

        ///// <summary>
        ///// Creates the injector. Called from either <see cref="Build"/> or <see cref="IHostBuilder.Build"/>
        ///// </summary>
        ///// <returns>The injector.</returns>
        //protected virtual IInjector CreateInjector()
        //{
        //    HostBuilderContext hostBuilderContext = Context.HostBuilderContext;
        //    IConfiguration appConfiguration = Context.AppConfiguration;

        //    hostBuilderContext.Configuration = appConfiguration;

        //    this.AddInstance(hostBuilderContext);
        //    this.AddInstance(appConfiguration);

        //    this.AddSingleton<IHostedServices, HostedServices>();
        //    this.AddSingleton<IHost, InjectorHost>();

        //    Logging.Populate(this);

        //    this.AddInstance(Context.HostBuilderContext);
        //    this.AddSingleton<IHostApplicationLifetime, ApplicationLifetime>();

        //    Context.Services.ToList().ForEach(d => this.Add(d));
        //    ServicePopulator.Create(ContainerBuilder).Populate(this);

        //    IContainer container = ContainerBuilder.Build();

        //    return new Injector(container);
        //}

        // IHostBuilder

        /// <inheritdoc/>
        public abstract IDictionary<object, object> Properties { get; }

        /// <inheritdoc/>
        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        { HostConfig.Add(configureDelegate); return this; }

        /// <inheritdoc/>
        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        { AppConfig.Add(configureDelegate); return this; }

        /// <inheritdoc/>
        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        { HostServices.Add(configureDelegate); return this; }

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

    }
}
