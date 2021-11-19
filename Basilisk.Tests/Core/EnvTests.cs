using Basilisk.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Basilisk.Tests.Core
{
    /// <summary>
    /// Tests for <code>Basilisk.Core.Env</code>.
    /// </summary>
    [TestClass]
    public class EnvTests
    {
        /// <summary>
        /// TestCurrentDirectory.
        /// </summary>
        [TestMethod]
        public void TestCurrentDirectory()
        {
            string expected = new DirectoryInfo(".").FullName;
            string actual = Env.CurrentDirectory.FullName;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// TestSearchPath.
        /// </summary>
        [TestMethod]
        public void TestSearchPath()
        {
            string expected = new DirectoryInfo(".").FullName;
            List<string> actuals = Env.SearchPath.Select(s => s.FullName).ToList();

            Assert.IsTrue(actuals.Contains(expected));
            Assert.IsTrue(actuals.Count > 1);
        }
    }
}
