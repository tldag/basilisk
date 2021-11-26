using Autofac;
using System;

namespace Basilisk.Injection.Configuration
{
    /// <summary>
    /// IAutofacConfig
    /// </summary>
    public interface IAutofacConfig
    {
        /// <summary>
        /// AddConfigurer
        /// </summary>
        /// <param name="configurer"></param>
        public void Add(Action<ContainerBuilder> configurer);

        /// <summary>
        /// Applies the collected configurers to the given builder.
        /// </summary>
        /// <param name="builder"></param>
        public void Apply(ContainerBuilder builder);
    }
}
