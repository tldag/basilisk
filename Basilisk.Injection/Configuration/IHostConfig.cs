using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Basilisk.Injection.Configuration
{
    /// <summary>
    /// IHostConfig
    /// </summary>
    public interface IHostConfig
    {
        /// <summary>
        /// Properties
        /// </summary>
        public IDictionary<object, object> Properties { get; }

        /// <summary>
        /// Adds the given configurer.
        /// </summary>
        /// <param name="configurer"></param>
        public void Add(Action<IConfigurationBuilder> configurer);

        /// <summary>
        /// Applies the configurers to the given builder.
        /// </summary>
        /// <param name="builder"></param>
        public void Apply(IConfigurationBuilder builder);
    }
}
