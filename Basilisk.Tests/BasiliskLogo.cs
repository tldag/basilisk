using Basilisk.Graphics.SVG;
using Basilisk.Graphics.SVG.Builder;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Basilisk.Tests
{
    internal static class BasiliskLogo
    {
        internal static SvgDocument Create()
        {
            return SvgBuilder.Create()
                .Size(256, 256)
                .BeginPath()
                .MoveTo(80, 40).LineTo(128, 0) // head
                .LineTo(224, 32).LineTo(256, 128) // neck
                .LineTo(256, 256).LineTo(128, 256) // front
                .LineTo(80, 240).LineTo(64, 192) // back
                .LineTo(80, 144).LineTo(128, 128) // tail
                .LineTo(0, 128).LineTo(0, 0).LineTo(128, 128) // wing
                .LineTo(64, 192) // tail
                .LineTo(128, 256) // back
                .LineTo(256, 128) // front
                .LineTo(128, 0).LineTo(128, 80).Close() // head
                .EndPath()
                .Build();
        }

        [RequiresUnreferencedCode("")]
        internal static FileInfo Create(FileInfo file)
        {
            Create().Save(file);

            return new(file.FullName);
        }
    }
}
