using Basilisk.Inject.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Collections.Generic;

namespace Basilisk.Inject
{
    /// <summary>
    /// IInjectorBuilder
    /// </summary>
    public interface IInjectorBuilder :
        IServiceCollection,
        IList<ServiceDescriptor>,
        ICollection<ServiceDescriptor>,
        IEnumerable<ServiceDescriptor>,
        IEnumerable,
        IHostBuilder
    {
        /// <summary>
        /// Host configuration
        /// </summary>
        public IHostConfig HostConfig { get; }

        /// <summary>
        /// Application specific configuration.
        /// </summary>
        public IAppConfig AppConfig { get; }

        /// <summary>
        /// Logger configuration.
        /// </summary>
        public ILogConfig LogConfig { get; }

        /// <summary>
        /// Autofac configuration
        /// </summary>
        public IAutofacConfig AutofacConfig { get; }

        /// <summary>
        /// Host service callbacks.
        /// </summary>
        public IHostServices HostServices { get; }

        /// <summary>
        /// Builds the injector.
        /// </summary>
        /// <returns>The injector.</returns>
        public new IInjector Build();
    }
}
