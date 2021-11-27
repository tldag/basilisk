using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Basilisk.Reflect
{
    /// <summary>
    /// Assembly extensions.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Returns a qualified name of the assembly.
        /// </summary>
        /// <param name="assembly">The assembly to inspect.</param>
        /// <returns>The name or <c>"Unknown"</c>, if not retrievable.</returns>
        public static string GetQualifiedName(this Assembly assembly)
            => assembly.GetName().Name ?? "Unknown";

        /// <summary>
        /// Returns the Company attribute value of the given assembly.
        /// </summary>
        /// <param name="assembly">The assembly to inspect.</param>
        /// <returns>The company name if the attribute exists, the empty string otherwise.</returns>
        public static string GetCompany(this Assembly assembly)
        {
            if (assembly.GetCustomAttribute<AssemblyCompanyAttribute>() is AssemblyCompanyAttribute attribute)
                return attribute.Company;

            return string.Empty;
        }

        /// <summary>
        /// Returns the Configuration attribute value of the given assembly.
        /// </summary>
        /// <param name="assembly">The assembly to inspect.</param>
        /// <returns>The configuration if the attribute exists, the empty string otherwise.</returns>
        public static string GetConfiguration(this Assembly assembly)
        {
            if (assembly.GetCustomAttribute<AssemblyConfigurationAttribute>() is AssemblyConfigurationAttribute attribute)
                return attribute.Configuration;

            return string.Empty;
        }

        /// <summary>
        /// Gets the value of the first metadata attribute found with the given key.
        /// </summary>
        /// <param name="assembly">The assembly to inspect.</param>
        /// <param name="key">The key to search for.</param>
        /// <returns>The first value found. Empty string if no such metadata attribute found.</returns>
        public static string GetMetadata(this Assembly assembly, string key)
            => assembly.GetMetadatas(key).FirstOrDefault() ?? string.Empty;

        /// <summary>
        /// Gets the metadata attribute values of the given assembly with the given key.
        /// </summary>
        /// <param name="assembly">The assembly to inspect.</param>
        /// <param name="key">The key to search for.</param>
        /// <returns>Enumeration of non-null values found.</returns>
        public static IEnumerable<string> GetMetadatas(this Assembly assembly, string key)
            => assembly.GetCustomAttributes<AssemblyMetadataAttribute>().Where(a => key.Equals(a.Key)).Select(a => a.Value).OfType<string>();
    }
}
