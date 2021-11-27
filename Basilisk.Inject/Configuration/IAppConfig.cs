using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Basilisk.Inject.Configuration
{
    /// <summary>
    /// Application specific configuration.
    /// </summary>
    public interface IAppConfig
    {
        /// <summary>
        /// Adds the given configurer not needing a host builder context.
        /// </summary>
        /// <param name="configurer"></param>
        public void Add(Action<IConfigurationBuilder> configurer);

        /// <summary>
        /// Adds the given configurer needing a host builder context.
        /// </summary>
        /// <param name="configurer"></param>
        public void Add(Action<HostBuilderContext, IConfigurationBuilder> configurer);

        /// <summary>
        /// Applies the added configurers to the given builder.
        /// </summary>
        /// <param name="hostBuilderContext"></param>
        /// <param name="builder"></param>
        public void Apply(HostBuilderContext hostBuilderContext, IConfigurationBuilder builder);
    }
}
