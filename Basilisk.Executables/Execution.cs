using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Basilisk.Executables
{
    /// <summary>
    /// Execution.
    /// </summary>
    public class Execution
    {
        /// <summary>
        /// The executable.
        /// </summary>
        protected FileInfo Executable { get; }

        /// <summary>
        /// The (cloned) options.
        /// </summary>
        protected ExecutionOptions Options { get; }

        /// <summary>
        /// Constructs a new Execution for the given executable with the given options.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="options">The options.</param>
        public Execution(FileInfo executable, ExecutionOptions options)
        {
            Executable = executable;
            Options = options.Clone();
        }

        /// <summary>
        /// Starts this execution.
        /// </summary>
        /// <returns>Task</returns>
        public Task<ExecutionResult> Start()
        {
            ProcessStartInfo startInfo = new();

            Options.ApplyTo(startInfo);

            ExecutionWorker worker = new(Executable, startInfo);

            return worker.Start();
        }

        /// <summary>
        /// Executes the execution.
        /// </summary>
        /// <returns>The result.</returns>
        public ExecutionResult Execute()
            => Start().Result;

        /// <summary>
        /// Creates a new Execution for the given executable with the given options.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Execution Create(FileInfo executable, ExecutionOptions options)
            => new(executable, options);

        /// <summary>
        /// Executes the given executable with the given options.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static ExecutionResult Execute(FileInfo executable, ExecutionOptions options)
            => Create(executable, options).Execute();
    }
}
