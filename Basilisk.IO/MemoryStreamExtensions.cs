using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.IO
{
    /// <summary>
    /// <c>MemoryStream</c> extensions.
    /// </summary>
    public static class MemoryStreamExtensions
    {
        /// <summary>
        /// Converts the contents of the given stream into a string.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string AsString(this MemoryStream stream, Encoding encoding)
            => encoding.GetString(stream.ToArray());
    }
}
