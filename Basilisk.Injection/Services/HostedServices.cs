using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.Injection.Services
{
    /// <summary>
    /// Implementation of <see cref="IHostedServices"/>.
    /// </summary>
    public class HostedServices : IHostedServices
    {
        private readonly List<IHostedService> hostedServices;

        /// <inheritdoc/>
        public IEnumerable<IHostedService> Services => hostedServices;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="hostedServices"></param>
        public HostedServices(IEnumerable<IHostedService> hostedServices)
        {
            this.hostedServices = new(hostedServices);
        }

        /// <inheritdoc/>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (IHostedService hostedService in hostedServices)
            {
                await hostedService.StartAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            List<Exception> exceptions = new();

            foreach (IHostedService hostedService in hostedServices.AsEnumerable().Reverse())
            {
                try
                {
                    await hostedService.StopAsync(cancellationToken).ConfigureAwait(false);
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
    }
}
