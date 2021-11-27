using Microsoft.Extensions.Logging;
using System;

namespace Basilisk.Inject.Configuration
{
    /// <summary>
    /// Logging configuration.
    /// </summary>
    public interface ILogConfig
    {
        /// <summary>
        /// Adds the given configurer.
        /// </summary>
        /// <param name="configurer"></param>
        public void Add(Action<ILoggingBuilder> configurer);

        /// <summary>
        /// Applies the added loggers to the given builder.
        /// </summary>
        /// <param name="builder"></param>
        public void Apply(ILoggingBuilder builder);
    }
}
