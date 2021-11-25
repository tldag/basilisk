using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Basilisk.Injection.Support
{
    /// <summary>
    /// IInjectorBuilderContext
    /// </summary>
    public interface IInjectorBuilderContext
    {
        /// <summary>
        /// The embedded <see cref="ContainerBuilder"/>.
        /// </summary>
        public ContainerBuilder ContainerBuilder { get; }

        /// <summary>
        /// Properties as used by <see cref="IHostBuilder"/>
        /// </summary>
        public IDictionary<object, object> Properties { get; }

        /// <summary>
        /// The host configuration.
        /// </summary>
        public IConfiguration HostConfiguration { get; }

        /// <summary>
        /// The app configuration.
        /// </summary>
        public IConfiguration AppConfiguration { get; }

        /// <summary>
        /// The host environment.
        /// </summary>
        public IHostEnvironment HostEnvironment { get; }

        /// <summary>
        /// Host builder context.
        /// </summary>
        public HostBuilderContext HostBuilderContext { get; }

        /// <summary>
        /// Adds the given host configurer.
        /// </summary>
        /// <param name="configurer"></param>
        public void AddHostConfigurer(Action<IConfigurationBuilder> configurer);

        /// <summary>
        /// Adds the given app configurer.
        /// </summary>
        /// <param name="configurer"></param>
        public void AddAppConfigurer(Action<HostBuilderContext, IConfigurationBuilder> configurer);
    }
}
