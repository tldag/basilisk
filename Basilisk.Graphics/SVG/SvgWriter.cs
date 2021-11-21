using System.Collections.Generic;
using System.Xml;
using static Basilisk.Graphics.SVG.Model.SvgModel;

namespace Basilisk.Graphics.SVG
{
    /// <summary>
    /// SVG writer.
    /// </summary>
    public class SvgWriter
    {
        /// <summary>
        /// Default SVG namespace.
        /// </summary>
        public const string Namespace = "http://www.w3.org/2000/svg";

        /// <summary>
        /// The XML writer this SVG writer writes to.
        /// </summary>
        public XmlWriter Writer { get; }

        /// <summary>
        /// Constructs a SVG writer with the given XML writer.
        /// </summary>
        /// <param name="writer">The XML writer to write to.</param>
        public SvgWriter(XmlWriter writer) { Writer = writer; }

        /// <summary>
        /// Creates a new SVG writer. 
        /// </summary>
        /// <param name="writer">The XML writer to write to.</param>
        /// <returns></returns>
        public static SvgWriter Create(XmlWriter writer) => new(writer);

        /// <summary>
        /// Writes the given SVG into the current writer.
        /// </summary>
        /// <param name="svg"></param>
        public virtual void Write(Svg svg)
        {
            WriteSvg(svg, Namespace);
            Writer.Flush();
        }

        /// <summary>
        /// Writes the given Svg.
        /// </summary>
        /// <param name="svg">The Svg.</param>
        /// <param name="ns">Namespace. Required for top level element.</param>
        protected virtual void WriteSvg(Svg svg, string? ns)
        {
            Writer.WriteStartElement("svg", ns);

            if (svg.Width is not null)
                Writer.WriteAttributeString("width", svg.Width);

            if (svg.Height is not null)
                Writer.WriteAttributeString("height", svg.Height);

            WriteShapes(svg.Shapes);
            Writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the given shapes.
        /// </summary>
        /// <param name="shapes"></param>
        protected virtual void WriteShapes(IEnumerable<Shape> shapes)
        {
            foreach (Shape shape in shapes)
            {
                switch (shape)
                {
                    case Path path: WritePath(path); break;
                }
            }
        }

        /// <summary>
        /// Writes the given path.
        /// </summary>
        /// <param name="path">The path</param>
        protected virtual void WritePath(Path path)
        {
            Writer.WriteStartElement("path");
            Writer.WriteAttributeString("d", path.Data);
            Writer.WriteEndElement();
        }
    }
}
