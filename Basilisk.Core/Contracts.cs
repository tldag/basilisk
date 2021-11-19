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
            /// Throws a <c>ApplicationException</c> if the given value is <c>null</c>.
            /// </summary>
            /// <typeparam name="T">Value type.</typeparam>
            /// <param name="value">Value.</param>
            /// <param name="message">Error message.</param>
            /// <returns>The value, if not <c>null</c>.</returns>
            public static T NotNull<T>(T? value, string message)
                => value ?? throw InvalidState(message);

            /// <summary>
            /// Throws a <c>PlatformNotSupportedException</c> if the current platform is not in the list of the given supported platforms.
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
