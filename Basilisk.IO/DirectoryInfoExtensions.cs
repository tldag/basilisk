using System.IO;

namespace Basilisk.IO
{
    /// <summary>
    /// <c>DirectoryInfo</c> extensions.
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Returns a <c>DirectoryInfo</c> for the created directory.
        /// </summary>
        /// <param name="directory">The directory to create.</param>
        /// <returns>A <c>DirectoryInfo</c> with <c>Exists == true</c>.</returns>
        public static DirectoryInfo Created(this DirectoryInfo directory)
        {
            if (directory.Exists)
                return directory;

            directory.Create();

            return new(directory.FullName);
        }

        /// <summary>
        /// Uses <c>Path.Combine</c> to create a sub-<c>DirectoryInfo</c> of the given directory.
        /// </summary>
        /// <param name="directory">The start directory.</param>
        /// <param name="parts">The additional paths to pass to <c>Path.Combine</c></param>
        /// <returns>The sub-directory.</returns>
        public static DirectoryInfo CombineDirectory(this DirectoryInfo directory, params string[] parts)
            => new(CombinePath(directory, parts));

        private static string CombinePath(DirectoryInfo directory, params string[] parts)
        {
            string[] paths = new string[parts.Length + 1];

            paths[0] = directory.FullName;

            for (int i = 0, n = parts.Length; i < n; ++i)
                paths[i + 1] = parts[i];

            return Path.Combine(paths);
        }
    }
}
