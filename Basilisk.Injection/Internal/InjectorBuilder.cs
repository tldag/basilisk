using Autofac;
using Basilisk.Injection.Host;
using Basilisk.Injection.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Reflection;
using static Basilisk.Injection.Support.ContentRootPathHelpers;

namespace Basilisk.Injection.Internal
{
    /// <summary>
    /// Internal InjectorBuilder.
    /// </summary>
    public partial class InjectorBuilder : Injection.InjectorBuilder
    {
        /// <inheritdoc/>
        protected override IInjector BuildInjector()
        {
            ContainerBuilder builder = new();
            HostBuilderContext hostBuilderContext = BuildHostBuilderContext();

            BuildComponents();
            BuildLogging();

            AutofacConfig.Apply(builder);
            HostServices.Apply(hostBuilderContext, this);
            ServicePopulator.Create(builder).Populate(this);

            return CreateInjector(builder);
        }

        /// <summary>
        /// Registers additional components.
        /// </summary>
        protected virtual void BuildComponents()
        {
            this.AddSingleton<IHost, InjectorHost>();
            this.AddSingleton<IHostedServices, HostedServices>();

            this.AddSingleton<IHostApplicationLifetime, ApplicationLifetime>();
        }

        /// <summary>
        /// Build logging infrastructure.
        /// </summary>
        protected virtual void BuildLogging()
        {
            this.AddLogging(LogConfig.Apply);
        }

        /// <summary>
        /// Creates the HostBuilderContext.
        /// </summary>
        /// <returns></returns>
        protected virtual HostBuilderContext BuildHostBuilderContext()
        {
            IConfiguration hostConfiguration = CreateHostConfiguration();
            IHostEnvironment hostEnvironment = CreateHostEnvironment(hostConfiguration);

            HostBuilderContext hostBuilderContext = new(Properties)
            {
                HostingEnvironment = hostEnvironment,
                Configuration = hostConfiguration
            };

            IConfiguration appConfiguration = CreateAppConfiguration(hostEnvironment, hostConfiguration, hostBuilderContext);

            hostBuilderContext.Configuration = appConfiguration;

            this.AddInstance(hostBuilderContext);
            this.AddInstance(appConfiguration);

            return hostBuilderContext;
        }

        /// <summary>
        /// Creates the host configuration.
        /// </summary>
        /// <returns></returns>
        protected virtual IConfiguration CreateHostConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();

            HostConfig.Apply(builder);

            return builder.Build();
        }

        /// <summary>
        /// Creates the host environment.
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
        /// Creates the app configuration. Doesn't overwrite the given environment's config value.
        /// </summary>
        /// <param name="hostEnvironment"></param>
        /// <param name="hostConfiguration"></param>
        /// <param name="hostBuilderContext"></param>
        /// <returns></returns>
        protected virtual IConfiguration CreateAppConfiguration(IHostEnvironment hostEnvironment, IConfiguration hostConfiguration,
            HostBuilderContext hostBuilderContext)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddConfiguration(hostConfiguration, shouldDisposeConfiguration: true);

            AppConfig.Apply(hostBuilderContext, builder);

            return builder.Build();
        }

        /// <summary>
        /// Creates the final injector from the given, fully configured, builder.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected virtual IInjector CreateInjector(ContainerBuilder builder)
        {
            IContainer container = builder.Build();

            return new Injector(container);
        }
    }
}
