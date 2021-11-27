using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.Inject.Host
{
    /// <summary>
    /// Implementation of <see cref="IHostedServices"/>.
    /// </summary>
    public class HostedServices : IHostedServices
    {
        private readonly List<IHostedService> services;

        /// <inheritdoc/>
        public IEnumerable<IHostedService> Services => services;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="services"></param>
        public HostedServices(IEnumerable<IHostedService> services)
        {
            this.services = new(services);
        }

        /// <inheritdoc/>
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            foreach (IHostedService service in services)
            {
                await service.StartAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            List<Exception> exceptions = new();

            foreach (IHostedService service in services.AsEnumerable().Reverse())
            {
                try
                {
                    await service.StopAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        /// <inheritdoc/>
        public async Task WaitAsync(CancellationToken cancellationToken = default)
        {
            List<Exception> exceptions = new();

            foreach (BackgroundService service in services.OfType<BackgroundService>())
            {
                Task task = service.ExecuteTask;

                if (task is not null)
                {
                    try
                    {
                        await task.ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        if (task.IsCanceled && ex is OperationCanceledException)
                            continue;

                        exceptions.Add(ex);
                    }
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
