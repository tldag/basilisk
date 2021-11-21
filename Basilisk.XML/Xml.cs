using System.IO;
using System.Text;
using System.Xml;

namespace Basilisk.XML
{
    /// <summary>
    /// XML support.
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// Default settings with indentation. Creates a new instance on every access.
        /// </summary>
        public static XmlWriterSettings IndentedSettings { get => new() { Indent = true, IndentChars = "  " }; }

        /// <summary>
        /// Creates an XML writer for the given stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="settings">The settings. Usses <c>PrettySettings</c>, if <c>null</c>.</param>
        /// <returns></returns>
        public static XmlWriter CreateXmlWriter(this Stream stream, XmlWriterSettings? settings = null)
            => XmlWriter.Create(stream, settings ?? IndentedSettings);
    }
}
