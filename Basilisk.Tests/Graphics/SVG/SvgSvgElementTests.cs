using Basilisk.Graphics.SVG.Model;
using Basilisk.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Basilisk.Tests.Graphics.SVG
{
    /// <summary>
    /// SvgSvgElementTests
    /// </summary>
    [TestClass]
    public class SvgSvgElementTests
    {
        /// <summary>
        /// Tests whether the namespaces property exists.
        /// </summary>
        [TestMethod]
        [RequiresUnreferencedCode("")]
        public void TestSerialize()
        {
            Svg svg = new();

            Debug.WriteLine(svg.SerializeXml());
        }
    }
}
