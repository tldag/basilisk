using Basilisk.Executables;
using Basilisk.IO;
using Basilisk.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Basilisk.Tests.Executables
{
    /// <summary>
    /// InkscapeTests
    /// </summary>
    [TestClass]
    public class InkscapeTests : TestsBase
    {
        private static readonly int[] Sizes = { 16, 24, 32, 48, 64, 96, 128, 256 };

        /// <summary>
        /// Test
        /// </summary>
        [TestMethod]
        public void Test()
        {
            FileInfo svgFile = TestDirectory.Combine("basilisk.svg");

            if (!svgFile.Exists)
                svgFile = BasiliskLogo.Create(svgFile);

            if (!Inkscape.Exists)
                return;

            foreach (int size in Sizes)
            {
                FileInfo pngFile = TestDirectory.Combine($"basilisk_{size}.png");

                if (pngFile.Exists)
                    continue;

                ExecutionResult result = Inkscape.Convert(svgFile, pngFile, size, size);

                result.Dump();
                Assert.AreEqual(0, result.ExitCode);
            }
        }
    }
}
