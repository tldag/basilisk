using Basilisk.IO;
using System.IO;

namespace Basilisk.Cryptography
{
    /// <summary>
    /// Support for <c>.snk</c> files.
    /// </summary>
    public static class SnkFile
    {
        /// <summary>
        /// Creates a new <c>.snk</c> file.
        /// </summary>
        /// <param name="file">The file to create.</param>
        public static void CreateSnkFile(FileInfo file)
        {
            file.WriteAllBytes(Keys.NewRsaKeyPair(1024).ToBytes(true));
        }
    }
}
