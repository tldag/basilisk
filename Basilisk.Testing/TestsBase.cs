using Basilisk.Core;
using Basilisk.IO;
using Basilisk.Reflection;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using static Basilisk.Core.Contracts;

namespace Basilisk.Testing
{
    /// <summary>
    /// Base class for tests.
    /// </summary>
    public class TestsBase
    {
        /// <summary>
        /// Whether the test runs on the CI server.
        /// </summary>
        public virtual bool IsCI { get => "true".Equals(Env.GetEnvironmentVariable("CI").ToLowerInvariant()); }

        private string? solutionFileName = null;

        /// <summary>
        /// File name of the solution as defined in the projects <c>AssemblyAttribute</c>.
        /// </summary>
        public string SolutionFileName { get => solutionFileName ??= GetType().Assembly.GetMetadata("SolutionFileName"); }

        /// <summary>
        /// Solution file as defined in the projects <c>AssemblyAttribute</c>.
        /// </summary>
        public FileInfo SolutionFile { get => Env.CurrentDirectory.FindFileAbove(SolutionFileName, true); }

        /// <summary>
        /// Solution directory as defined in the projects <c>AssemblyAttribute</c>.
        /// </summary>
        public DirectoryInfo SolutionDirectory { get => SolutionFile.GetDirectory(); }

        /// <summary>
        /// Directory <c>"TestOutput"</c> within solution directory.
        /// </summary>
        public DirectoryInfo TestOutputDirectory { get => SolutionDirectory.CombineDirectory("TestOutput").Created(); }

        /// <summary>
        /// The configuration of this test.
        /// </summary>
        public string Configuration { get => GetType().Assembly.GetConfiguration(); }

        /// <summary>
        /// Output directory for the calling test method.
        /// </summary>
        public DirectoryInfo TestDirectory
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                StackFrame stackFrame = new(1, true);
                MethodBase method = State.NotNull(stackFrame.GetMethod(), "Not called from test method"); // TODO: move text into resource
                Type type = State.NotNull(method.DeclaringType, "Not called from test method"); // TODO: move text into resource
                Assembly assembly = type.Assembly;
                string assemblyName = assembly.GetQualifiedName();
                string typeName = type.GetFullName();
                string methodName = method.Name;

                return TestOutputDirectory.CombineDirectory(assemblyName, Configuration, typeName, methodName).Created();
            }
        }
    }
}
