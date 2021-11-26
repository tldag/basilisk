using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Basilisk.Injection.Configuration.Internal
{
    /// <summary>
    /// Default <see cref="IAppConfig"/> implementation.
    /// </summary>
    public class AppConfig : IAppConfig
    {
        /// <summary>
        /// SimpleConfigurers
        /// </summary>
        protected List<Action<IConfigurationBuilder>> SimpleConfigurers { get; } = new();

        /// <summary>
        /// ContextualConfigurers
        /// </summary>
        protected List<Action<HostBuilderContext, IConfigurationBuilder>> ContextualConfigurers { get; } = new();

        /// <inheritdoc/>
        public void Add(Action<IConfigurationBuilder> configurer)
            => SimpleConfigurers.Add(configurer);

        /// <inheritdoc/>
        public void Add(Action<HostBuilderContext, IConfigurationBuilder> configurer)
            => ContextualConfigurers.Add(configurer);

        /// <inheritdoc/>
        public void Apply(HostBuilderContext hostBuilderContext, IConfigurationBuilder builder)
        {
            foreach (Action<IConfigurationBuilder> configurer in SimpleConfigurers)
            {
                configurer(builder);
            }

            foreach (Action<HostBuilderContext, IConfigurationBuilder> configurer in ContextualConfigurers)
            {
                configurer(hostBuilderContext, builder);
            }
        }
    }
}
