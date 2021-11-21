using System.Xml.Serialization;

namespace Basilisk.Graphics.SVG.Model
{
    public static partial class SvgModel
    {
        /// <summary>
        /// https://www.w3.org/TR/SVG11/paths.html#InterfaceSVGPathElement
        /// </summary>
        public class Path : Shape
        {
            /// <summary>
            /// The path data.
            /// </summary>
            public string Data { get; set; } = string.Empty;
        }
    }
}
