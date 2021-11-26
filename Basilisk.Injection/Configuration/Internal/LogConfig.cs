using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Basilisk.Injection.Configuration.Internal
{
    /// <summary>
    /// Default <see cref="ILogConfig"/> implementation.
    /// </summary>
    public class LogConfig : ILogConfig
    {
        /// <summary>
        /// Configurers
        /// </summary>
        protected List<Action<ILoggingBuilder>> Configurers { get; } = new();

        /// <inheritdoc/>
        public void Add(Action<ILoggingBuilder> configurer)
        {
            Configurers.Add(configurer);
        }

        /// <inheritdoc/>
        public void Apply(ILoggingBuilder builder)
        {
            foreach (Action<ILoggingBuilder> configurer in Configurers)
            {
                configurer(builder);
            }
        }
    }
}
