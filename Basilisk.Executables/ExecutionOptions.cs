using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Basilisk.Executables
{
    /// <summary>
    /// Executable options.
    /// </summary>
    public abstract class ExecutionOptions
    {
        /// <summary>
        /// The executable file.
        /// </summary>
        public FileInfo? Executable { get; set; } = null;

        /// <summary>
        /// Arguments passed to the executable.
        /// </summary>
        public List<string> Arguments { get; } = new();

        /// <summary>
        /// Whether to not create a separate window.
        /// </summary>
        public bool? NoWindow { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether to use the operating system shell to start the process.
        /// </summary>
        public bool? ShellExecute { get; set; } = null;

        /// <summary>
        /// Working directory.
        /// </summary>
        public DirectoryInfo? Directory { get; set; } = null;

        /// <summary>
        /// Whether to redirect regular output.
        /// </summary>
        public bool? RedirectOutput { get; set; } = null;

        /// <summary>
        /// Whether to redirect error output.
        /// </summary>
        public bool? RedirectError { get; set; } = null;

        /// <summary>
        /// Constructs options with default settings.
        /// </summary>
        protected ExecutionOptions() { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="options">Options to copy.</param>
        protected ExecutionOptions(ExecutionOptions options)
        {
            Executable = options.Executable;
            Arguments.AddRange(options.Arguments);
            NoWindow = options.NoWindow;
            ShellExecute = options.ShellExecute;
            Directory = options.Directory;
            RedirectOutput = options.RedirectOutput;
            RedirectError = options.RedirectError;
        }

        /// <summary>
        /// Deep cloes this options.
        /// </summary>
        /// <returns>The clone</returns>
        public abstract ExecutionOptions Clone();

        /// <summary>
        /// Apply the configured options to the given start info.
        /// </summary>
        /// <param name="startInfo">The start info to apply the options to.</param>
        protected abstract void ApplyOptions(ProcessStartInfo startInfo);

        /// <summary>
        /// Apply the configured options to the given start info.
        /// </summary>
        /// <param name="startInfo">The start info to apply the options to.</param>
        public void ApplyTo(ProcessStartInfo startInfo)
        {
            if (Executable is not null)
                startInfo.FileName = Executable.FullName;

            foreach (string argument in Arguments)
                startInfo.ArgumentList.Add(argument);

            if (NoWindow is not null)
                startInfo.CreateNoWindow = (bool)NoWindow;

            if (ShellExecute is not null)
                startInfo.UseShellExecute = (bool)ShellExecute;

            if (Directory is not null)
                startInfo.WorkingDirectory = Directory.FullName;

            if (RedirectOutput is not null)
                startInfo.RedirectStandardOutput = (bool)RedirectOutput;

            if (RedirectError is not null)
                startInfo.RedirectStandardError = (bool)RedirectError;

            ApplyOptions(startInfo);
        }
    }

    /// <summary>
    /// Executable options.
    /// </summary>
    /// <typeparam name="T">Sub-class of this class.</typeparam>
    public abstract class ExecutionOptions<T> : ExecutionOptions
        where T : ExecutionOptions<T>
    {
        /// <summary>
        /// This
        /// </summary>
        protected abstract T This { get; }

        /// <summary>
        /// Constructs options with default settings.
        /// </summary>
        protected ExecutionOptions() { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="options">Options to copy.</param>
        protected ExecutionOptions(ExecutionOptions options) : base(options) { }

        /// <summary>
        /// Sets the executable file.
        /// </summary>
        /// <param name="executable">The executable file.</param>
        /// <returns>This</returns>
        public T SetExecutable(FileInfo? executable) { Executable = executable; return This; }

        /// <summary>
        /// Adds the given argument to the argument list.
        /// </summary>
        /// <param name="argument">Argument to add</param>
        /// <returns>This</returns>
        public T AddArgument(string argument) { Arguments.Add(argument); return This; }

        /// <summary>
        /// Sets whether to not create a separate window.
        /// </summary>
        /// <param name="noWindow">Whether to not create a separate window. <c>null</c> to use default value.</param>
        /// <returns>This</returns>
        public T CreateNoWindow(bool? noWindow) { NoWindow = noWindow; return This; }

        /// <summary>
        /// Sets whether to use the operating system shell to start the process.
        /// </summary>
        /// <param name="shellExecute">Whether to use the operating system shell to start the process. <c>null</c> to use default value.</param>
        /// <returns>This</returns>
        public T UseShellExecute(bool? shellExecute) { ShellExecute = shellExecute; return This; }

        /// <summary>
        /// Sets the working directory.
        /// </summary>
        /// <param name="directory">The working directory. <c>null</c> to leave unconfigured.</param>
        /// <returns>This</returns>
        public T WorkingDirectory(DirectoryInfo? directory) { Directory = directory; return This; }

        /// <summary>
        /// Sets whether to redirect regular output.
        /// </summary>
        /// <param name="redirect">Whether to redirect. <c>null</c> to leave unconfigured.</param>
        /// <returns>This</returns>
        public T RedirectStandardOutput(bool? redirect) { RedirectOutput = redirect; return This; }

        /// <summary>
        /// Sets whether to redirect error output.
        /// </summary>
        /// <param name="redirect">Whether to redirect. <c>null</c> to leave unconfigured.</param>
        /// <returns>This</returns>
        public T RedirectStandardError(bool? redirect) { RedirectError = redirect; return This; }
    }
}
