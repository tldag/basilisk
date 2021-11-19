using Basilisk.Reflection;

namespace Basilisk.Testing
{
    /// <summary>
    /// Base class for tests.
    /// </summary>
    public class TestsBase
    {
        private string? solutionFileName = null;

        /// <summary>
        /// File name of the solution as defined in the projects <c>AssemblyAttribute</c>.
        /// </summary>
        public string SolutionFileName { get => solutionFileName ??= GetType().Assembly.GetMetadata("SolutionFileName"); }
    }
}
