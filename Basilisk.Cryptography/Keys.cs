using System.Security.Cryptography;

namespace Basilisk.Cryptography
{
    /// <summary>
    /// Support for cryptographic keys
    /// </summary>
    public static class Keys
    {
        /// <summary>
        /// Creates a new RSA key pair.
        /// </summary>
        /// <param name="keySize">Key size in bits.</param>
        /// <returns>Blob containing public and private key</returns>
        public static RSACryptoServiceProvider NewRsaKeyPair(int keySize = 1024)
            => new (keySize);

        /// <summary>
        /// Exports the Blob of the given key pair.
        /// </summary>
        /// <param name="keyPair">The key pair</param>
        /// <param name="includePrivateParameters">true to include the private key; otherwise, false.</param>
        /// <returns>The blob.</returns>
        public static byte[] ToBytes(this RSACryptoServiceProvider keyPair, bool includePrivateParameters)
            => keyPair.ExportCspBlob(includePrivateParameters);

    }
}
