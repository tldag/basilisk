using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;
using static Basilisk.Injection.Support.ContentRootPathHelpers;

namespace Basilisk.Injection.Support
{
    /// <summary>
    /// <see cref="IInjectorBuilderFactory"/> default implementation.
    /// </summary>
    public class InjectorBuilderFactory : IInjectorBuilderFactory
    {
        /// <inheritdoc/>
        public IInjectorBuilderContext CreateContext()
            => new InjectorBuilderContext(this);

        /// <inheritdoc/>
        public IConfiguration CreateHostConfiguration(IEnumerable<Action<IConfigurationBuilder>> configurers)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();

            foreach (Action<IConfigurationBuilder> configurer in configurers)
            {
                configurer(configBuilder);
            }

            return configBuilder.Build();
        }

        /// <inheritdoc/>
        public IConfiguration CreateAppConfiguration(IHostEnvironment hostEnvironment, IConfiguration hostConfiguration,
            HostBuilderContext hostBuilderContext, IEnumerable<Action<HostBuilderContext, IConfigurationBuilder>> configurers)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddConfiguration(hostConfiguration, shouldDisposeConfiguration: true);

            foreach (Action<HostBuilderContext, IConfigurationBuilder> configurer in configurers)
            {
                configurer(hostBuilderContext, configBuilder);
            }

            return configBuilder.Build();
        }

        /// <inheritdoc/>
        public IHostEnvironment CreateHostEnvironment(IConfiguration hostConfiguration)
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

        /// <inheritdoc/>
        public HostBuilderContext CreateHostBuilderContext(
            IDictionary<object, object> properties, IHostEnvironment hostEnvironment, IConfiguration hostConfiguration)
        {
            HostBuilderContext context = new(properties);

            context.HostingEnvironment = hostEnvironment;
            context.Configuration = hostConfiguration;

            return context;
        }

        /// <inheritdoc/>
        public IServiceCollection CreateServices(HostBuilderContext hostBuilderContext,
            IEnumerable<Action<HostBuilderContext, IServiceCollection>> configurers)
        {
            IServiceCollection services = new ServiceCollection();

            foreach (Action<HostBuilderContext, IServiceCollection> configurer in configurers)
            {
                configurer(hostBuilderContext, services);
            }

            return services;
        }
    }
}
