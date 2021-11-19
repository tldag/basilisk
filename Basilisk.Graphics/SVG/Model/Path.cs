using System.Xml.Serialization;

namespace Basilisk.Graphics.SVG.Model
{
    /// <summary>
    /// https://www.w3.org/TR/SVG11/paths.html#InterfaceSVGPathElement
    /// </summary>
    public class Path : Shape
    {
        /// <summary>
        /// The path data.
        /// </summary>
        [XmlAttribute("d")]
        public string D { get; set; } = string.Empty;
    }
}
