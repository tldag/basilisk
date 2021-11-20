using Basilisk.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.IO.SearchOption;

namespace Basilisk.Executables
{
    /// <summary>
    /// Support for executables.
    /// </summary>
    public static class Executable
    {
        /// <summary>
        /// Executable extensions for the current OS.
        /// </summary>
        public static string[] Extensions { get => new[] { Env.IsWindows ? ".exe" : "" }; }

        /// <summary>
        /// Enumerates all executables of the given name on the current <c>PATH</c>. A file name extension appropriate
        /// for the current OS is added automatically.
        /// </summary>
        /// <param name="name">The executable file name without extension.</param>
        /// <returns>The found executables.</returns>
        public static IEnumerable<FileInfo> EnumerateExecutables(string name)
        {
            foreach (string extension in Extensions)
            {
                string pattern = name + extension;

                foreach (DirectoryInfo directory in Env.SearchPath)
                {
                    foreach (FileInfo executable in directory.GetFiles(pattern, TopDirectoryOnly))
                        yield return executable;
                }
            }
        }
    }
}
