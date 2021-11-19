using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using static System.IO.SearchOption;
using static Basilisk.Core.Exceptions;

namespace Basilisk.IO
{
    /// <summary>
    /// Support for finding files and directories.
    /// </summary>
    public static class Finder
    {
        /// <summary>
        /// Finds all files matching the given pattern in the directories above the given start directory.
        /// </summary>
        /// <param name="start">The start directory.</param>
        /// <param name="pattern">The search pattern.</param>
        /// <param name="includeStart">Whether to include the start directory into the search.</param>
        /// <returns>The matching files, nearest first.</returns>
        public static IEnumerable<FileInfo> FindFilesAbove(DirectoryInfo start, string pattern, bool includeStart)
        {
            DirectoryInfo? directory = includeStart ? start : start.Parent;

            while (directory is not null)
            {
                foreach (FileInfo file in directory.EnumerateFiles(pattern, TopDirectoryOnly))
                    yield return file;

                directory = directory.Parent;
            }
        }

        /// <summary>
        /// Tries to find a file matching the given pattern in the directories above the given start directory.
        /// </summary>
        /// <param name="start">The start directory.</param>
        /// <param name="pattern">The search pattern.</param>
        /// <param name="includeStart">Whether to include the start directory into the search.</param>
        /// <param name="file">The nearest file found matching the pattern or <c>null</c> if not found.</param>
        /// <returns><c>true</c>, if found.</returns>
        public static bool TryFindFileAbove(DirectoryInfo start, string pattern, bool includeStart, [NotNullWhen(true)] out FileInfo? file)
        {
            file = FindFilesAbove(start, pattern, includeStart).FirstOrDefault();
            return file is not null;
        }

        /// <summary>
        /// Find a file matching the given pattern in the directories above the given start directory.
        /// </summary>
        /// <param name="start">The start directory.</param>
        /// <param name="pattern">The search pattern.</param>
        /// <param name="includeStart">Whether to include the start directory into the search.</param>
        /// <returns>The nearest file found matching the pattern.</returns>
        /// <exception cref="FileNotFoundException">If file not found.</exception>
        public static FileInfo FindFileAbove(DirectoryInfo start, string pattern, bool includeStart)
            => TryFindFileAbove(start, pattern, includeStart, out FileInfo? file) ? file : throw FileNotFound(pattern);
    }
}
