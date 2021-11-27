using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Basilisk.Inject.Configuration
{
    /// <summary>
    /// IHostServices
    /// </summary>
    public interface IHostServices
    {
        /// <summary>
        /// Adds the given configurer.
        /// </summary>
        /// <param name="configurer"></param>
        public void Add(Action<HostBuilderContext, IServiceCollection> configurer);

        /// <summary>
        /// Applies the configurers to the given services.
        /// </summary>
        /// <param name="hostBuilderContext"></param>
        /// <param name="services"></param>
        public void Apply(HostBuilderContext hostBuilderContext, IServiceCollection services);
    }
}
