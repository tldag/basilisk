using System.Collections.Generic;

namespace Basilisk.Collections
{
    /// <summary>
    /// <c>IDictionary</c> extensions.
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Creates a shallow clone of the given dictionary.
        /// </summary>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="source">Source.</param>
        /// <returns>Shallow clone.</returns>
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(IDictionary<TKey, TValue> source) where TKey : notnull
            => new(source);
    }
}
