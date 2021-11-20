using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        /// <summary>
        /// Dumps this result to the <c>Console</c> and to <c>Debug</c>.
        /// </summary>
        public void Dump()
        {
            Console.WriteLine($"ExitCode: {ExitCode}");
            Debug.WriteLine($"ExitCode: {ExitCode}");

            if (Errors.Any())
            {
                Console.WriteLine("Errors:");
                Debug.WriteLine("Errors:");

                foreach (string error in Errors)
                {
                    Console.WriteLine($"  {error}");
                    Debug.WriteLine($"  {error}");
                }
            }

            if (Outputs.Any())
            {
                Console.WriteLine("Output:");
                Debug.WriteLine("Output:");

                foreach (string output in Outputs)
                {
                    Console.WriteLine($"  {output}");
                    Debug.WriteLine($"  {output}");
                }
            }
        }
    }
}
