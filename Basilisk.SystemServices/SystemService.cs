using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.SystemServices
{
    /// <summary>
    /// System service.
    /// </summary>
    public class SystemService : BackgroundService, ISystemService, IDisposable
    {
        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;
        }
    }
}
