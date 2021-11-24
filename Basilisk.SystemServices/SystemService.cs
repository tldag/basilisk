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
        public virtual void Dispose()
        {
            base.Dispose();
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;
        }
    }
}
