using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Basilisk.Extensions
{
    /// <summary>
    /// <c>IHost</c> extensions.
    /// </summary>
    public static class IHostExtensions
    {
        /// <summary>
        /// Get service of type T from the host.Services.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <param name="host">The host to get the servce from.</param>
        /// <returns>A service object of type T.</returns>
        public static T GetRequiredService<T>(this IHost host) where T : notnull
            => host.Services.GetRequiredService<T>();
    }
}
