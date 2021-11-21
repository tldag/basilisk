using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.Gallery
{
    /// <summary>
    /// Gallery service.
    /// </summary>
    public class GalleryService : BackgroundService
    {
        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
