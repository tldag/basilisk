using System.Runtime.InteropServices;
using static Basilisk.Core.Exceptions;

namespace Basilisk.Core
{
    /// <summary>
    /// Contract support.
    /// </summary>
    public static class Contracts
    {
        /// <summary>
        /// State contracts.
        /// </summary>
        public static class State
        {
            /// <summary>
            /// Throws a <code>PlatformNotSupportedException</code> if the current platform is not in the list of the given supported platforms.
            /// </summary>
            /// <param name="supportedPlatforms">The supported platforms.</param>
            public static void PlatformRequired(params OSPlatform[] supportedPlatforms)
            {
                foreach (OSPlatform platform in supportedPlatforms)
                {
                    if (RuntimeInformation.IsOSPlatform(platform))
                        return;
                }

                throw PlatformNotSupported();
            }
        }
    }
}
