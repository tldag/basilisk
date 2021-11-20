using Basilisk.Core;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Basilisk.Executables
{
    /// <summary>
    /// <c>ProcessStartInfo</c> extensions.
    /// </summary>
    public static class ProcessStartInfoExtensions
    {
        /// <summary>
        /// Clones the given <c>ProcessStartInfo</c>.
        /// </summary>
        /// <param name="info">Source.</param>
        /// <returns>The clone.</returns>
        public static ProcessStartInfo Clone(this ProcessStartInfo info)
        {
            ProcessStartInfo clone = new();

            foreach (string argument in info.ArgumentList)
                clone.ArgumentList.Add(argument);

            clone.Arguments = info.Arguments;
            clone.CreateNoWindow = info.CreateNoWindow;
            clone.ErrorDialog = info.ErrorDialog;
            clone.ErrorDialogParentHandle = info.ErrorDialogParentHandle;
            clone.FileName = info.FileName;
            clone.RedirectStandardError = info.RedirectStandardError;
            clone.RedirectStandardInput = info.RedirectStandardInput;
            clone.RedirectStandardOutput = info.RedirectStandardOutput;
            clone.StandardErrorEncoding = info.StandardErrorEncoding;
            clone.StandardInputEncoding = info.StandardInputEncoding;
            clone.StandardOutputEncoding = info.StandardOutputEncoding;
            clone.UseShellExecute = info.UseShellExecute;
            clone.Verb = info.Verb;
            clone.WindowStyle = info.WindowStyle;
            clone.WorkingDirectory = info.WorkingDirectory;

            if (info.Environment is IDictionary<string, string?> environment)
            {
                foreach (KeyValuePair<string, string?> kvp in environment)
                    clone.Environment[kvp.Key] = kvp.Value;
            }

            if (info.EnvironmentVariables is StringDictionary environmentVariables)
            {
                foreach (KeyValuePair<string, string?> kvp in environmentVariables)
                    clone.EnvironmentVariables[kvp.Key] = kvp.Value;
            }

            if (Env.IsWindows)
            {
#pragma warning disable CA1416 // Validate platform compatibility
                clone.Domain = info.Domain;
                clone.LoadUserProfile = info.LoadUserProfile;
                clone.Password = info.Password;
                clone.PasswordInClearText = info.PasswordInClearText;
#pragma warning restore CA1416 // Validate platform compatibility
            }

            return clone;
        }
    }
}
