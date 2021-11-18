using Basilisk.Graphics.SVG.Model;
using System.IO;

namespace Basilisk.Graphics.SVG
{
    /// <summary>
    /// Scalable Vector Graphic. Modeled after https://www.w3.org/TR/SVG11/ .
    /// </summary>
    public class Svg
    {
        private SvgSvgElement root = new();

        /// <summary>
        /// The root if the document
        /// </summary>
        public ISvgSvgElement Root { get { return root; } }

        /// <summary>
        /// Loads the SVG from the given file.
        /// </summary>
        /// <param name="file">the file to load from</param>
        public void Load(FileInfo file)
        {
            // TODO
        }
    }
}
