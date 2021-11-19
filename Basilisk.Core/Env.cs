using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Basilisk.Core
{
    /// <summary>
    /// Environment support.
    /// </summary>
    public static class Env
    {
        /// <summary>
        /// Whether the current OS platform is Windows.
        /// </summary>
        public static bool IsWindows { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary>
        /// Shorthand for <c>Environment.GetEnvironmentVariable(key) ?? string.Empty</c>
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
        /// Environment variable <c>PATH</c> converted to enumerable. Contains existing directories only.
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

        /// <summary>
        /// <c>Program Files</c> directory. Windows only.
        /// </summary>
        public static DirectoryInfo ProgramFiles
        {
            get => new(Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        }
    }
}
