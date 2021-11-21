using System.Collections.Generic;

namespace Basilisk.Graphics.SVG.Model
{
    /// <summary>
    /// SVG Model.
    /// </summary>
    public static partial class SvgModel
    {
        /// <summary>
        /// https://www.w3.org/TR/SVG11/struct.html#InterfaceSVGSVGElement
        /// </summary>
        public class Svg
        {
            /// <summary>
            /// Width.
            /// </summary>
            public string? Width { get; set; }

            /// <summary>
            /// Height.
            /// </summary>
            public string? Height { get; set; }

            /// <summary>
            /// The shapes within this svg element.
            /// </summary>
            public List<Shape> Shapes { get; set; } = new();
        }
    }
}
