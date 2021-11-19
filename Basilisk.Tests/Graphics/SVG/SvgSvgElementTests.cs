using Basilisk.Graphics.SVG.Model;
using Basilisk.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

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
        public void TestNamespaces()
        {
            List<PropertyInfo> properties = PropertyFinder.Create(typeof(SvgSvgElement))
                .RequireType(typeof(XmlSerializerNamespaces))
                .RequireRead()
                .RequireAttribute(typeof(XmlNamespaceDeclarationsAttribute))
                .Find().ToList();

            properties.ForEach(p => { Debug.WriteLine(p); });
        }
    }
}
