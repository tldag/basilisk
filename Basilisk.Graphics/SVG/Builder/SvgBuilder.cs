using static Basilisk.Graphics.SVG.Model.SvgModel;

namespace Basilisk.Graphics.SVG.Builder
{
    /// <summary>
    /// Builder to create SVG document.
    /// </summary>
    public class SvgBuilder
    {
        private readonly Svg root = new();

        /// <summary>
        /// Creates a new <c>SvgBuilder</c>.
        /// </summary>
        /// <returns>The builder.</returns>
        public static SvgBuilder Create()
            => new();

        /// <summary>
        /// Sets the size of the current container.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <returns>this</returns>
        public SvgBuilder Size(string? width, string? height)
        {
            root.Width = width;
            root.Height = height;

            return this;
        }

        /// <summary>
        /// Sets the size of the current container.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <returns>this</returns>
        public SvgBuilder Size(int width, int height)
            => Size(width.ToString(), height.ToString());

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
        /// Begins a <c>path</c> element.
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
