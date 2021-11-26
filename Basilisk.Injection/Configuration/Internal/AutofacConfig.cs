using Autofac;
using System;
using System.Collections.Generic;

namespace Basilisk.Injection.Configuration.Internal
{
    /// <summary>
    /// Default implementation of <see cref="IAutofacConfig"/>.
    /// </summary>
    public class AutofacConfig : IAutofacConfig
    {
        /// <summary>
        /// Configurers
        /// </summary>
        protected List<Action<ContainerBuilder>> Configurers { get; } = new();

        /// <inheritdoc/>
        public void Add(Action<ContainerBuilder> configurer)
            => Configurers.Add(configurer);

        /// <inheritdoc/>
        public void Apply(ContainerBuilder builder)
        {
            foreach (Action<ContainerBuilder> configurer in Configurers)
            {
                configurer(builder);
            }
        }
    }
}
