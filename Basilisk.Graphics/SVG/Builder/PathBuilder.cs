using static Basilisk.Graphics.SVG.Model.SvgModel;

namespace Basilisk.Graphics.SVG.Builder
{
    /// <summary>
    /// Builder to create <c>SvgPathElement</c>.
    /// </summary>
    public class PathBuilder
    {
        private readonly SvgBuilder svgBuilder;
        private readonly Path path = new();

        /// <summary>
        /// Constructs a path builder for the given svg builder.
        /// </summary>
        /// <param name="svgBuilder">The enclosing svg builder</param>
        public PathBuilder(SvgBuilder svgBuilder)
        {
            this.svgBuilder = svgBuilder;
        }

        private PathBuilder Add(string step)
        {
            path.Data += step;
            return this;
        }

        /// <summary>
        /// Move to given absolute position.
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>This builder.</returns>
        public PathBuilder MoveTo(int x, int y)
            => Add($"M{x},{y}");

        /// <summary>
        /// Line to given absolute position.
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>This builder.</returns>
        public PathBuilder LineTo(int x, int y)
            => Add($"L{x},{y}");

        /// <summary>
        /// Closes the path.
        /// </summary>
        /// <returns>This builder.</returns>
        public PathBuilder Close()
            => Add($"z");

        /// <summary>
        /// Ends the path and returns the enclosing svg builder.
        /// </summary>
        /// <returns>The enclosing svg builder.</returns>
        public SvgBuilder EndPath()
        {
            svgBuilder.Add(path);
            return svgBuilder;
        }
    }
}
