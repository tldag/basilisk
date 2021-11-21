using Basilisk.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Basilisk.Gallery
{
    /// <summary>
    /// Main program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <returns></returns>
        public static async Task<int> Main(string[] args)
        {
            await CreateHost(args).RunAsync();

            return 0;
        }

        /// <summary>
        /// Creates the service host.
        /// </summary>
        /// <param name="args">Startup arguments.</param>
        /// <returns>The host.</returns>
        public static IHost CreateHost(string[] args)
        {
            IHostBuilder builder = Host
                .CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<GalleryService>();
                });

            if (Env.IsWindows)
            {
                builder.UseWindowsService(options =>
                {
                    options.ServiceName = "Basilisk.Gallery";
                });
            }
            else
            {
                builder.UseSystemd();
            }

            return builder.Build();
        }
    }
}
