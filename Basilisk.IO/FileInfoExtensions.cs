using System.IO;

namespace Basilisk.IO
{
    /// <summary>
    /// FileInfo extensions.
    /// </summary>
    public static class FileInfoExtensions
    {
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
