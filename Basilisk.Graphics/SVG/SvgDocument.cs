using Basilisk.Graphics.SVG.Model;
using Basilisk.IO;
using System.IO;

namespace Basilisk.Graphics.SVG
{
    /// <summary>
    /// Scalable Vector Graphic. Modeled after https://www.w3.org/TR/SVG11/ .
    /// </summary>
    public class SvgDocument
    {
        private Svg root;

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
        /// Loads the SVG from the given file.
        /// </summary>
        /// <param name="file">the file to load from</param>
        public void Load(FileInfo file)
        {
            root = Xml.DeserializeXml<Svg>(file);
        }

        /// <summary>
        /// Loads the SVG from the given XML content.
        /// </summary>
        /// <param name="xml">The XML content.</param>
        public void LoadXml(string xml)
        {
            root = Xml.DeserializeXml<Svg>(xml);
        }

        /// <summary>
        /// Saves the document to the given file.
        /// </summary>
        /// <param name="file">The file to save to.</param>
        public void Save(FileInfo file)
        {
            Xml.SerializeXml(root, file);
        }

        /// <summary>
        /// Saves the document into an XML content.
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            return Xml.SerializeXml(root);
        }
    }
}
