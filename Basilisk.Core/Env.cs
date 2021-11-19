using System;
using System.Collections.Generic;
using System.IO;

namespace Basilisk.Core
{
    /// <summary>
    /// Environment support.
    /// </summary>
    public static class Env
    {
        /// <summary>
        /// Shorthand for <code>Environment.GetEnvironmentVariable(key) ?? string.Empty</code>
        /// </summary>
        /// <param name="variableName">The variable name to search for.</param>
        /// <returns>The value of the environment variable or the empty string if there is no such value.</returns>
        public static string GetEnvironmentVariable(string variableName)
            => Environment.GetEnvironmentVariable(variableName) ?? string.Empty;

        /// <summary>
        /// Gets the expanded environment variable for the given variable name.
        /// </summary>
        /// <param name="variableName">The variable name to search for.</param>
        /// <returns>The expanded value of the environment variable or the empty string if there is no such value.</returns>
        public static string GetExpandedEnvironmentVariable(string variableName)
            => Environment.ExpandEnvironmentVariables(GetEnvironmentVariable(variableName));

        /// <summary>
        /// Current/working directory.
        /// </summary>
        public static DirectoryInfo CurrentDirectory
        {
            get => new(Environment.CurrentDirectory);
        }

        /// <summary>
        /// Environment variable <code>PATH</code> converted to enumerable. Contains existing directories only.
        /// </summary>
        public static IEnumerable<DirectoryInfo> SearchPath
        {
            get
            {
                yield return CurrentDirectory;

                foreach (string entry in GetExpandedEnvironmentVariable("PATH").Split(Path.PathSeparator))
                {
                    if (Directory.Exists(entry))
                        yield return new(entry);
                }
            }
        }
    }
}
