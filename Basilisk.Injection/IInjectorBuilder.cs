using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Collections.Generic;

namespace Basilisk.Injection
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
        /// The embedded container builder.
        /// </summary>
        public ContainerBuilder ContainerBuilder { get; }

        /// <summary>
        /// Builds the injector.
        /// </summary>
        /// <returns>The injector.</returns>
        public new IInjector Build();
    }
}
