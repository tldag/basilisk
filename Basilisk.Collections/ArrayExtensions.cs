using System.Linq;

namespace Basilisk.Collections
{
    /// <summary>
    /// Array extensions.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Creates a shallow copy of the given array.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Source array</param>
        /// <returns>Shallow copy.</returns>
        public static T[] Copy<T>(T[] source)
            => source.AsEnumerable().ToArray();
    }
}
