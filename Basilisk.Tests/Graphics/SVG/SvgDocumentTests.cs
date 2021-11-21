using Basilisk.Graphics.SVG;
using Basilisk.Graphics.SVG.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Basilisk.Tests.Graphics.SVG
{
    /// <summary>
    /// SvgDocumentTests
    /// </summary>
    [TestClass]
    public class SvgDocumentTests
    {
        /// <summary>
        /// TestSaveLoad
        /// </summary>
        [TestMethod]
        [RequiresUnreferencedCode("")]
        public void TestSaveLoad()
        {
            SvgDocument document = new();

            document.Root.Shapes.Add(new Path());

            string xml = document.Save();

            Debug.WriteLine(xml);

            document.LoadXml(xml);
        }
    }
}
