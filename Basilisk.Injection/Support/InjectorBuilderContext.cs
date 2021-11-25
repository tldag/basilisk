using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Basilisk.Injection.Support
{
    /// <summary>
    /// <see cref="IInjectorBuilderContext"/> default implementation.
    /// </summary>
    public class InjectorBuilderContext : IInjectorBuilderContext
    {
        /// <summary>
        /// The factory.
        /// </summary>
        protected IInjectorBuilderFactory Factory { get; }

        /// <summary>
        /// Host configurers.
        /// </summary>
        protected List<Action<IConfigurationBuilder>> HostConfigurers { get; } = new();

        /// <summary>
        /// App configurers
        /// </summary>
        protected List<Action<HostBuilderContext, IConfigurationBuilder>> AppConfigurers { get; } = new();

        /// <inheritdoc/>
        public ContainerBuilder ContainerBuilder { get; } = new();

        /// <inheritdoc/>
        public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();

        private IConfiguration? hostConfiguration = null;

        /// <inheritdoc/>
        public IConfiguration HostConfiguration { get => hostConfiguration ??= Factory.CreateHostConfiguration(HostConfigurers); }

        private IConfiguration? appConfiguration = null;

        /// <inheritdoc/>
        public IConfiguration AppConfiguration
        { get => appConfiguration ??= Factory.CreateAppConfiguration(HostEnvironment, HostConfiguration, HostBuilderContext, AppConfigurers); }

        private IHostEnvironment? hostEnvironment = null;

        /// <inheritdoc/>
        public IHostEnvironment HostEnvironment { get => hostEnvironment ??= Factory.CreateHostEnvironment(HostConfiguration); }

        private HostBuilderContext? hostBuilderContext = null;

        /// <inheritdoc/>
        public HostBuilderContext HostBuilderContext
        { get => hostBuilderContext ??= Factory.CreateHostBuilderContext(Properties, HostEnvironment, HostConfiguration); }

        /// <summary>
        /// C'tor.
        /// </summary>
        /// <param name="factory"></param>
        public InjectorBuilderContext(IInjectorBuilderFactory factory)
        {
            Factory = factory;
        }

        /// <inheritdoc/>
        public void AddHostConfigurer(Action<IConfigurationBuilder> configurer)
        {
            HostConfigurers.Add(configurer);
        }

        /// <inheritdoc/>
        public void AddAppConfigurer(Action<HostBuilderContext, IConfigurationBuilder> configurer)
        {
            AppConfigurers.Add(configurer);
        }
    }
}
