using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Basilisk.Core.Resources.ExceptionResources;
using static System.Environment;

namespace Basilisk.Core
{
    /// <summary>
    /// Support for exceptions.
    /// </summary>
    public static class Exceptions
    {
        /// <summary>
        /// Finds the inner most exception of the given Exception.
        /// </summary>
        /// <param name="exception">Outer exception</param>
        /// <returns>Inner most exception.</returns>
        public static Exception InnerMost(this Exception exception)
            => exception.InnerException?.InnerMost() ?? exception;

        /// <summary>
        /// Creates a <code>NotImplementedException</code> for the calling method.
        /// </summary>
        /// <returns>New instance mentioning the calling method.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static NotImplementedException NotYetImplemented()
            => new(new StackFrame(1, true).ToString());

        /// <summary>
        /// Creates a <code>NotSupportedException</code> for the calling method.
        /// </summary>
        /// <returns>New instance mentioning the calling method.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static NotSupportedException NotSupported()
            => new(new StackFrame(1, true).ToString());

        /// <summary>
        /// Creates a <code>PlatformNotSupportedException</code>.
        /// </summary>
        /// <returns>New instance mentioning the current platform.</returns>
        public static PlatformNotSupportedException PlatformNotSupported()
            => new(RuntimeInformation.OSDescription);

        /// <summary>
        /// Creates a <code>ObjectDisposedException</code>.
        /// </summary>
        /// <param name="name">The name of the object that has been disposed.</param>
        /// <returns>New instance, optionally mentioning the name of the disposed object.</returns>
        public static ObjectDisposedException ObjectDisposed(string? name = null)
            => new(name);

        /// <summary>
        /// Creates a <code>FileNotFoundException</code>.
        /// </summary>
        /// <param name="path">Path to the file that was not found.</param>
        /// <returns>New instance mentioning the file that was not found.</returns>
        public static FileNotFoundException FileNotFound(string path)
            => new(null, path);

        /// <summary>
        /// Creates a <code>FileNotFoundException</code>.
        /// </summary>
        /// <param name="file">The file that was not found.</param>
        /// <returns>New instance mentioning the file that was not found.</returns>
        public static FileNotFoundException FileNotFound(FileInfo file)
            => new(null, file.FullName);

        /// <summary>
        /// Creates a <code>DirectoryNotFoundException</code>.
        /// </summary>
        /// <param name="path">Path to directory that was not found.</param>
        /// <returns>New instance mentioning the directory that was not found.</returns>
        public static DirectoryNotFoundException DirectoryNotFound(string path)
            => new(path);

        /// <summary>
        /// Creates a <code>DirectoryNotFoundException</code>.
        /// </summary>
        /// <param name="directory">The directory that was not found.</param>
        /// <returns>New instance mentioning the directory that was not found.</returns>
        public static DirectoryNotFoundException DirectoryNotFound(DirectoryInfo directory)
            => new(directory.FullName);

        /// <summary>
        /// Creates an <code>IOException</code>.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="path">Path to file with bad format.</param>
        /// <returns></returns>
        public static IOException BadFileFormat(string message, string path)
            => new(BadFileFormatFormat.Format(message, path));

        /// <summary>
        /// Creates an <code>IOException</code>.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="file">File with bad format.</param>
        /// <returns></returns>
        public static IOException BadFileFormat(string message, FileInfo file)
            => new(BadFileFormatFormat.Format(message, file.FullName));

        /// <summary>
        /// Creates an <code>ArgumentException</code>.
        /// </summary>
        /// <param name="paramName">Name of the erronous parameter.</param>
        /// <param name="message">Additional message.</param>
        /// <returns></returns>
        public static ArgumentException InvalidArgument(string paramName, string message)
            => new(message, paramName);

        /// <summary>
        /// Creates an <code>ArgumentOutOfRangeException</code>.
        /// </summary>
        /// <param name="paramName">Name of the erronous parameter.</param>
        /// <param name="actualValue">Actual value of the parameter.</param>
        /// <param name="message">Additional message.</param>
        /// <returns></returns>
        public static ArgumentOutOfRangeException OutOfRange(string paramName, object actualValue, string message)
            => new(paramName, actualValue, message);

        /// <summary>
        /// Creates an <code>ApplicationException</code> with the given message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApplicationException InvalidState(string message)
            => new(InvalidStateFormat.Format(message));

        /// <summary>
        /// Creates an <code>ApplicationException</code> denoting an execution failure.
        /// </summary>
        /// <param name="exitCode">Exit code of failing execution.</param>
        /// <param name="errors">Errors.</param>
        /// <returns></returns>
        public static ApplicationException ExecutionFailed(int exitCode, IEnumerable<string> errors)
            => new(ExecutionFailedFormat.Format(exitCode, NewLine, errors.Join(NewLine)));

        /// <summary>
        /// Creates an <code>ApplicationException</code> denoting an execution failure.
        /// </summary>
        /// <param name="exitCode">Exit code of failing execution.</param>
        /// <param name="errors">Errors.</param>
        /// <returns></returns>
        public static ApplicationException ExecutionFailed(int exitCode, params string[] errors)
            => ExecutionFailed(exitCode, errors.AsEnumerable());
    }
}
