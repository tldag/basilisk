using Basilisk.Graphics.SVG;
using Basilisk.Graphics.SVG.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

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
        public void TestSaveLoad()
        {
            SvgDocument document = new();

            document.Root.Shapes.Add(new SvgPathElement());

            string xml = document.Save();

            Debug.WriteLine(xml);

            document.LoadXml(xml);
        }
    }
}
