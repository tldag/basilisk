using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Basilisk.Injection.Configuration.Internal
{
    /// <summary>
    /// Default implementation of <see cref="IHostConfig"/>.
    /// </summary>
    public class HostConfig : IHostConfig
    {
        /// <summary>
        /// Configuration configurers
        /// </summary>
        protected List<Action<IConfigurationBuilder>> Configurers { get; } = new();


        /// <inheritdoc/>
        public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();

        /// <inheritdoc/>
        public void Add(Action<IConfigurationBuilder> configurer)
        {
            Configurers.Add(configurer);
        }


        /// <inheritdoc/>
        public void Apply(IConfigurationBuilder builder)
        {
            foreach (Action<IConfigurationBuilder> configurer in Configurers)
            {
                configurer(builder);
            }
        }
    }
}
