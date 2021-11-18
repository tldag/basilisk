using Basilisk.IO;
using System.IO;

namespace Basilisk.Cryptography
{
    /// <summary>
    /// Support for <code>.snk</code> files.
    /// </summary>
    public static class SnkFile
    {
        /// <summary>
        /// Creates a new <code>.snk</code> file.
        /// </summary>
        /// <param name="file">The file to create.</param>
        public static void CreateSnkFile(FileInfo file)
        {
            file.WriteAllBytes(Keys.NewRsaKeyPair(1024).ToBytes(true));
        }
    }
}
