using System;
using System.IO;
using Basilisk.Core;
using static Basilisk.Core.Contracts;
using static Basilisk.IO.Resources.IOResources;

namespace Basilisk.IO
{
    /// <summary>
    /// FileInfo extensions.
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Returns the directory of the given file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The directory. Never <c>null</c>.</returns>
        /// <exception cref="ApplicationException">If file has no directory.</exception>
        public static DirectoryInfo GetDirectory(this FileInfo file)
            => State.NotNull(file.Directory, FileHasNoDirectoryFormat.Format(file));

        /// <summary>
        /// Shorthand for <c>File.WriteAllBytes(...)</c>
        /// </summary>
        /// <param name="file">The file to write to.</param>
        /// <param name="bytes">The bytes to write.</param>
        public static void WriteAllBytes(this FileInfo file, byte[] bytes)
        {
            File.WriteAllBytes(file.FullName, bytes);
        }
    }
}
