using Microsoft.Extensions.Hosting;

namespace Basilisk.Extensions
{
    /// <summary>
    /// Factory to create <c>IHost</c>
    /// </summary>
    public class HostFactory
    {
        /// <summary>
        /// Creates a new factory instance.
        /// </summary>
        /// <returns></returns>
        public static HostFactory Create() => new();

        /// <summary>
        /// Builds the injection host.
        /// </summary>
        /// <returns></returns>
        public virtual IHost Build()
        {
            HostBuilder builder = new();

            return builder.Build();
        }
    }
}
