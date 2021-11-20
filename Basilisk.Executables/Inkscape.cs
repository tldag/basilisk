using Basilisk.Core;
using Basilisk.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using static Basilisk.Core.Exceptions;

namespace Basilisk.Executables
{
    /// <summary>
    /// InkScape
    /// </summary>
    public static class Inkscape
    {
        /// <summary>
        /// Whether Inkscape can be found on this machine. 
        /// </summary>
        public static bool Exists { get => EnumerateInkscapes().Any(); }

        /// <summary>
        /// Tries to find an executable Inkscape
        /// </summary>
        /// <param name="executable">The found executable or <c>null</c>, if none found.</param>
        /// <returns>Whether a Inkscape has been found.</returns>
        public static bool TryGetInkscape([NotNullWhen(true)] out FileInfo? executable)
        {
            executable = EnumerateInkscapes().FirstOrDefault();

            return executable is not null;
        }

        /// <summary>
        /// Returns the path to the first Inkscape executable on this machine.
        /// </summary>
        /// <returns>The executable.</returns>
        /// <exception cref="FileNotFoundException">If not found.</exception>
        public static FileInfo GetInkscape()
            => TryGetInkscape(out FileInfo ? executable) ? executable : throw FileNotFound("inkscape");

        /// <summary>
        /// Finds all Inkscapes. Includes the one installed in <c>%ProgramFiles%/Inkscape</c> on windows.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<FileInfo> EnumerateInkscapes()
        {
            if (Env.IsWindows)
            {
                FileInfo executable = Env.ProgramFiles.Combine("Inkscape", "bin", "inkscape.exe");

                if (executable.Exists)
                    yield return executable;
            }

            foreach (FileInfo executable in Executable.EnumerateExecutables("inkscape"))
                yield return executable;
        }

        /// <summary>
        /// Converts the given SVG file to the given exportFile.
        /// </summary>
        /// <param name="svgFile">The SVG file.</param>
        /// <param name="exportFile">The export file. Its extension determines the conversion type.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>Result of the execution.</returns>
        public static ExecutionResult Convert(FileInfo svgFile, FileInfo exportFile, int width, int height)
        {
            FileInfo executable = GetInkscape();

            InkscapeOptions options = InkscapeOptions.Create()
                .SetExecutable(executable)
                .CreateNoWindow(true).UseShellExecute(false)
                .SetImportFile(svgFile).SetExportFile(exportFile)
                .SetExportSize(width, height);

            return Execution.Execute(executable, options);
        }
    }
}
