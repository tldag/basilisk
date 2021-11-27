using System;

namespace Basilisk.Reflect
{
    /// <summary>
    /// <c>Type</c> extensions.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Full name of the given type.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <returns>The full name or <c>"Unknown"</c>, if not retrievable.</returns>
        public static string GetFullName(this Type type)
            => type.FullName ?? "Unknown";
    }
}
