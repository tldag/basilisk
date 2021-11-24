using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.Injection.Support
{
    /// <summary>
    /// <see cref="IHost"/> implementation. 
    /// </summary>
    public class InjectorHost : IHost
    {
        /// <inheritdoc/>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Hosted services.
        /// </summary>
        public IHostedServices HostedServices { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="services"></param>
        /// <param name="hostedServices"></param>
        public InjectorHost(IServiceProvider services, IHostedServices hostedServices)
        {
            Services = services;
            HostedServices = hostedServices;
        }

        /// <inheritdoc/>
        public void Dispose() { GC.SuppressFinalize(this); }

        /// <inheritdoc/>
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await HostedServices.StartAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await HostedServices.StopAsync(cancellationToken);
        }
    }
}
