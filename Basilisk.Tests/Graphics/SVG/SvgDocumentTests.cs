using Basilisk.Graphics.SVG;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using static Basilisk.Graphics.SVG.Model.SvgModel;

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

            document.Root.Shapes.Add(new Path());

            string xml = document.Save();

            Debug.WriteLine(xml);
        }
    }
}
