using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Basilisk.Injection.Configuration.Internal
{
    /// <summary>
    /// Default <see cref="IHostServices"/> implementation.
    /// </summary>
    public class HostServices : IHostServices
    {
        /// <summary>
        /// Servive configurers
        /// </summary>
        protected List<Action<HostBuilderContext, IServiceCollection>> Configurers { get; } = new();

        /// <inheritdoc/>
        public void Add(Action<HostBuilderContext, IServiceCollection> configurer)
        {
            Configurers.Add(configurer);
        }

        /// <inheritdoc/>
        public void Apply(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            foreach (Action<HostBuilderContext, IServiceCollection> configurer in Configurers)
            {
                configurer(hostBuilderContext, services);
            }
        }
    }
}
