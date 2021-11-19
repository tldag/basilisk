using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Basilisk.Graphics.SVG.Model
{
    /// <summary>
    /// https://www.w3.org/TR/SVG11/struct.html#InterfaceSVGSVGElement
    /// </summary>
    [XmlRoot("svg", Namespace = "http://www.w3.org/2000/svg")]
    public class SvgSvgElement
    {
        private static readonly XmlSerializerNamespaces namespaces
            = new(new[] { new XmlQualifiedName("", "http://www.w3.org/2000/svg") });

        /// <summary>
        /// The namespace(s) of this element.
        /// </summary>
        [XmlNamespaceDeclarations]
        public virtual XmlSerializerNamespaces Namespaces { get => namespaces; }

        /// <summary>
        /// Width.
        /// </summary>
        [XmlAttribute("width")]
        public string? Width { get; set; }

        /// <summary>
        /// The shapes within this svg element.
        /// </summary>
        [XmlElement("path", typeof(SvgPathElement))]
        public List<SvgShapeElement> Shapes { get; set; } = new();
    }
}
 