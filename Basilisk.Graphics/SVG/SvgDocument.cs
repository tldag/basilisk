using Basilisk.IO;
using Basilisk.XML;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using static Basilisk.Graphics.SVG.Model.SvgModel;

namespace Basilisk.Graphics.SVG
{
    /// <summary>
    /// Scalable Vector Graphic. Modeled after https://www.w3.org/TR/SVG11/ .
    /// </summary>
    public class SvgDocument
    {
        private readonly Svg root;

        /// <summary>
        /// The root if the document
        /// </summary>
        public Svg Root { get { return root; } }

        /// <summary>
        /// Constructs an empty svg document.
        /// </summary>
        public SvgDocument() : this(new()) { }

        internal SvgDocument(Svg root)
        {
            this.root = root;
        }

        /// <summary>
        /// Saves the document to the given file.
        /// </summary>
        /// <param name="file">The file to save to.</param>
        /// <param name="settings">Writer settings to use.</param>
        public void Save(FileInfo file, XmlWriterSettings? settings = null)
        {
            using FileStream stream = file.OpenWrite();
            using XmlWriter writer = stream.CreateXmlWriter(settings);

            SvgWriter.Create(writer).Write(root);
        }

        /// <summary>
        /// Saves the document into an XML content.
        /// </summary>
        /// <returns></returns>
        public string Save(XmlWriterSettings? settings = null)
        {
            settings ??= Xml.IndentedSettings;

            using MemoryStream stream = new();
            using XmlWriter writer = stream.CreateXmlWriter(settings);

            SvgWriter.Create(writer).Write(root);

            return new(stream.AsString(settings.Encoding));
        }
    }
}
