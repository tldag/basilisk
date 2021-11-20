using System.Collections.Generic;

namespace Basilisk.Executables
{
    /// <summary>
    /// Execution result.
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        /// The exit code.
        /// </summary>
        public int ExitCode { get; set; }

        /// <summary>
        /// The collected standard output.
        /// </summary>
        public List<string> Outputs { get; } = new();

        /// <summary>
        /// The collected error output.
        /// </summary>
        public List<string> Errors { get; } = new();
    }
}
