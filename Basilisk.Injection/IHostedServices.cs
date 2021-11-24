using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.Injection
{
    /// <summary>
    /// Collection of <see cref="IHostedService"/>.
    /// </summary>
    public interface IHostedServices
    {
        /// <summary>
        /// The services.
        /// </summary>
        public IEnumerable<IHostedService> Services { get; }

        /// <summary>
        /// Calls <see cref="IHostedService.StartAsync(CancellationToken)"/> on all hosted services.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls <see cref="IHostedService.StopAsync(CancellationToken)"/> on all hosted services.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Waits for the <see cref="BackgroundService"/>s to terminate.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task WaitAsync(CancellationToken cancellationToken = default);
    }
}
