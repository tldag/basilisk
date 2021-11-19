using Basilisk.Graphics.SVG.Model;

namespace Basilisk.Graphics.SVG.Builder
{
    /// <summary>
    /// Builder to create SVG document.
    /// </summary>
    public class SvgBuilder
    {
        private readonly Svg root = new();

        /// <summary>
        /// Adds a shape to the current container.
        /// </summary>
        /// <param name="shape">The shape to add.</param>
        /// <returns>This builder.</returns>
        public SvgBuilder Add(Shape shape)
        {
            root.Shapes.Add(shape);
            return this;
        }

        /// <summary>
        /// Begins a <code>path</code> element.
        /// </summary>
        /// <returns>A path builder linked to this builder.</returns>
        public PathBuilder BeginPath()
            => new(this);

        /// <summary>
        /// Builds the svg document.
        /// </summary>
        /// <returns>The svg document</returns>
        public SvgDocument Build()
            => new(root);
    }
}
