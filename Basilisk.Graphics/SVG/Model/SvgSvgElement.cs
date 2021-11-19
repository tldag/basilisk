using System.Xml;
using System.Xml.Serialization;

namespace Basilisk.Graphics.SVG.Model
{
    /// <summary>
    /// https://www.w3.org/TR/SVG11/struct.html#InterfaceSVGSVGElement
    /// </summary>
    [XmlRoot("svg")]
    public class SvgSvgElement : ISvgSvgElement // TODO: add missing interfaces
    {
        private static readonly XmlSerializerNamespaces namespaces
            = new(new[] { new XmlQualifiedName("", "http://www.w3.org/2000/svg") });

        /// <summary>
        /// The namespace(s) of this element.
        /// </summary>
        [XmlNamespaceDeclarations]
        public virtual XmlSerializerNamespaces Namespaces { get => namespaces; }
    }
}
 