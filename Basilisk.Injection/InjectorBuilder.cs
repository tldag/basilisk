using Autofac;
using Basilisk.Injection.Services;
using Basilisk.Injection.Support;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Basilisk.Injection
{
    /// <summary>
    /// Injector builder.
    /// </summary>
    public class InjectorBuilder : ServiceCollection, IHostBuilder
    {
        /// <summary>
        /// Actions to build the configuration.
        /// </summary>
        protected List<Action<IConfigurationBuilder>> ConfigureHostConfigActions { get; } = new();

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
            IConfiguration hostConfiguration = CreateHostConfiguration();
            IHostEnvironment hostEnvironment = CreateHostEnvironment(hostConfiguration);
            HostBuilderContext hostBuilderContext = CreateHostBuilderContext(hostEnvironment, hostConfiguration);

            this.AddLogging();
            this.AddInstance(hostBuilderContext);
            this.AddSingleton<IHostApplicationLifetime, ApplicationLifetime>();

            Builder.RegisterType<HostedServices>().As<IHostedServices>().SingleInstance();
            Builder.RegisterType<InjectorHost>().As<IHost>().SingleInstance();

            ServicePopulator.Create(Builder).Populate(this);

            IContainer container = Builder.Build();

            return new Injector(container);
        }

        /// <summary>
        /// Creates the <see cref="HostBuilderContext"/>.
        /// </summary>
        /// <returns></returns>
        protected virtual HostBuilderContext CreateHostBuilderContext(IHostEnvironment hostEnvironment, IConfiguration hostConfiguration)
        {
            HostBuilderContext context = new(Properties);

            context.HostingEnvironment = hostEnvironment;
            context.Configuration = hostConfiguration;

            return context;
        }

        /// <summary>
        /// Creates the <see cref="IHostEnvironment"/> instance.
        /// </summary>
        /// <param name="hostConfiguration"></param>
        /// <returns></returns>
        protected virtual IHostEnvironment CreateHostEnvironment(IConfiguration hostConfiguration)
        {
            HostingEnvironment environment = new()
            {
                EnvironmentName = hostConfiguration[HostDefaults.EnvironmentKey] ?? Environments.Production,
                ApplicationName = hostConfiguration[HostDefaults.ApplicationKey],
                ContentRootPath = ResolveContentRootPath(hostConfiguration[HostDefaults.ContentRootKey], AppContext.BaseDirectory)
            };

            if (string.IsNullOrEmpty(environment.ApplicationName))
                environment.ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name;

            environment.ContentRootFileProvider = new PhysicalFileProvider(environment.ContentRootPath);

            return environment;
        }

        /// <summary>
        /// Resolves the content root path required to create the host environment.
        /// </summary>
        /// <param name="contentRootPath"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        protected virtual string ResolveContentRootPath(string contentRootPath, string basePath)
        {
            if (string.IsNullOrWhiteSpace(contentRootPath))
                return basePath;

            if (Path.IsPathRooted(contentRootPath))
                return contentRootPath;

            return Path.Combine(Path.GetFullPath(basePath), contentRootPath);
        }

        /// <summary>
        /// Creates the host configuration.
        /// </summary>
        /// <returns></returns>
        protected virtual IConfiguration CreateHostConfiguration()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();

            foreach (Action<IConfigurationBuilder> action in ConfigureHostConfigActions)
            {
                action(configBuilder);
            }

            return configBuilder.Build();
        }

        // IHostBuilder

        /// <inheritdoc/>
        public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();

        /// <inheritdoc/>
        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            ConfigureHostConfigActions.Add(configureDelegate);
            return this;
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
