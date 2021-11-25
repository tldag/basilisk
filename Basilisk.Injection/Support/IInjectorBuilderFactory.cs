using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Basilisk.Injection.Support
{
    /// <summary>
    /// IInjectorBuilderFactory
    /// </summary>
    public interface IInjectorBuilderFactory
    {
        /// <summary>
        /// Creates the context used in the <see cref="InjectorBuilder"/>.
        /// </summary>
        /// <returns>The context.</returns>
        public IInjectorBuilderContext CreateContext();

        /// <summary>
        /// Creates a configuration using the given configurers.
        /// </summary>
        /// <param name="configurers"></param>
        /// <returns></returns>
        public IConfiguration CreateHostConfiguration(IEnumerable<Action<IConfigurationBuilder>> configurers);

        /// <summary>
        /// Creates the app configuration.
        /// </summary>
        /// <param name="hostEnvironment"></param>
        /// <param name="hostConfiguration"></param>
        /// <param name="hostBuilderContext"></param>
        /// <param name="configurers"></param>
        /// <returns></returns>
        public IConfiguration CreateAppConfiguration(IHostEnvironment hostEnvironment, IConfiguration hostConfiguration,
            HostBuilderContext hostBuilderContext, IEnumerable<Action<HostBuilderContext, IConfigurationBuilder>> configurers);

        /// <summary>
        /// Creates the host environment.
        /// </summary>
        /// <param name="hostConfiguration"></param>
        /// <returns></returns>
        public IHostEnvironment CreateHostEnvironment(IConfiguration hostConfiguration);

        /// <summary>
        /// Creates the host builder context.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="hostEnvironment"></param>
        /// <param name="hostConfiguration"></param>
        /// <returns></returns>
        public HostBuilderContext CreateHostBuilderContext(
            IDictionary<object, object> properties, IHostEnvironment hostEnvironment, IConfiguration hostConfiguration);
    }
}
