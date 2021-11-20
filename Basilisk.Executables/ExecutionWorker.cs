using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Basilisk.Executables
{
    /// <summary>
    /// Execution worker.
    /// </summary>
    public class ExecutionWorker
    {
        /// <summary>
        /// Event args.
        /// </summary>
        public class DataReceivedArgs : EventArgs
        {
            /// <summary>
            /// The data received.
            /// </summary>
            public string Data { get; }

            /// <summary>
            /// Constructs new event args.
            /// </summary>
            /// <param name="data"></param>
            public DataReceivedArgs(string data)
            {
                Data = data;
            }
        }

        /// <summary>
        /// Event handler for received data (regular/error output).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event data.</param>
        public delegate void DataReceivedHandler(ExecutionWorker sender, DataReceivedArgs e);

        /// <summary>
        /// Event raised on any output.
        /// </summary>
        public event DataReceivedHandler? DataReceived = null;

        /// <summary>
        /// Event raised on regular output.
        /// </summary>
        public event DataReceivedHandler? OutputDataReceived = null;

        /// <summary>
        /// Event raised on error output.
        /// </summary>
        public event DataReceivedHandler? ErrorDataReceived = null;

        /// <summary>
        /// Cloned startup info.
        /// </summary>
        protected ProcessStartInfo StartInfo { get; set; }

        /// <summary>
        /// Constructs a new worker.
        /// </summary>
        /// <param name="executable"></param>
        /// <param name="startInfo"></param>
        public ExecutionWorker(FileInfo executable, ProcessStartInfo startInfo)
        {
            StartInfo = startInfo.Clone();
            StartInfo.FileName = executable.FullName;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;
        }

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <returns></returns>
        public async Task<ExecutionResult> Start()
        {
            Process process = new();
            ExecutionResult result = new();

            process.StartInfo = StartInfo;

            process.OutputDataReceived += (_, e) => OnOutputDataReceived(e, result);
            process.ErrorDataReceived += (_, e) => OnErrorDataReceived(e, result);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();

            result.ExitCode = process.ExitCode;

            return result;
        }

        /// <summary>
        /// Processes regular output. Raises appropriate events.
        /// </summary>
        /// <param name="args">The event raised by the process.</param>
        /// <param name="result">The result to add the output to.</param>
        protected virtual void OnOutputDataReceived(DataReceivedEventArgs args, ExecutionResult result)
        {
            string data = args.Data ?? string.Empty;

            result.Outputs.Add(data);

            if (DataReceived is not null || OutputDataReceived is not null)
            {
                DataReceivedArgs e = new(data);

                DataReceived?.Invoke(this, e);
                OutputDataReceived?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Processes error output. Raises appropriate events.
        /// </summary>
        /// <param name="args">The event raised by the process.</param>
        /// <param name="result">The result to add the output to.</param>
        protected virtual void OnErrorDataReceived(DataReceivedEventArgs args, ExecutionResult result)
        {
            string data = args.Data ?? string.Empty;

            result.Errors.Add(data);

            if (DataReceived is not null || ErrorDataReceived is not null)
            {
                DataReceivedArgs e = new(data);

                DataReceived?.Invoke(this, e);
                ErrorDataReceived?.Invoke(this, e);
            }
        }
    }
}
