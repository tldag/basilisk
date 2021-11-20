using System.Diagnostics;
using System.IO;

namespace Basilisk.Executables
{
    /// <summary>
    /// Inkscape startup options. https://inkscape.org/doc/inkscape-man.html.
    /// </summary>
    public class InkscapeOptions : ExecutionOptions<InkscapeOptions>
    {
        /// <inheritdoc/>
        protected override InkscapeOptions This => this;

        /// <summary>
        /// The export file. Sets the <c>-o</c> argument.
        /// </summary>
        public FileInfo? ExportFile { get; set; } = null;

        /// <summary>
        /// The export width. Sets the <c>-w</c> argument.
        /// </summary>
        public int? ExportWidth { get; set; } = null;

        /// <summary>
        /// The export height. Sets the <c>-h</c> argument.
        /// </summary>
        public int? ExportHeight { get; set; } = null;

        /// <summary>
        /// The import file. Sets the last argument.
        /// </summary>
        public FileInfo? ImportFile { get; set; } = null;

        /// <summary>
        /// Creates a new <c>InkscapeOptions</c> instance.
        /// </summary>
        /// <returns>The new instance.</returns>
        public static InkscapeOptions Create() => new();

        /// <summary>
        /// Constructs options with default settings.
        /// </summary>
        public InkscapeOptions() { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="options">Options to copy.</param>
        protected InkscapeOptions(InkscapeOptions options) : base(options)
        {
            ExportFile = options.ExportFile;
            ExportWidth = options.ExportWidth;
            ExportHeight = options.ExportHeight;
            ImportFile = options.ImportFile;
        }

        /// <inheritdoc/>
        public override InkscapeOptions Clone()
            => new(this);

        /// <inheritdoc/>
        protected override void ApplyOptions(ProcessStartInfo startInfo)
        {
            if (ExportFile is not null)
            {
                startInfo.ArgumentList.Add("-o");
                startInfo.ArgumentList.Add(ExportFile.FullName);
            }

            if (ExportWidth is not null)
            {
                startInfo.ArgumentList.Add("-w");
                startInfo.ArgumentList.Add(ExportWidth.Value.ToString());
            }

            if (ExportHeight is not null)
            {
                startInfo.ArgumentList.Add("-h");
                startInfo.ArgumentList.Add(ExportHeight.Value.ToString());
            }

            if (ImportFile is not null)
                startInfo.ArgumentList.Add(ImportFile.FullName);
        }

        /// <summary>
        /// Sets the export file. Sets the <c>-o</c> argument.
        /// </summary>
        /// <param name="exportFile">The export file.</param>
        /// <returns>this</returns>
        public InkscapeOptions SetExportFile(FileInfo? exportFile) { ExportFile = exportFile; return this; }

        /// <summary>
        /// Sets the export width. Sets the <c>-w</c> argument.
        /// </summary>
        /// <param name="exportWidth">The export width.</param>
        /// <returns>this</returns>
        public InkscapeOptions SetExportWidth(int? exportWidth) { ExportWidth = exportWidth; return this; }

        /// <summary>
        /// Sets the export height. Sets the <c>-h</c> argument.
        /// </summary>
        /// <param name="exportHeight">The export height.</param>
        /// <returns>this</returns>
        public InkscapeOptions SetExportHeight(int? exportHeight) { ExportHeight = exportHeight; return this; }

        /// <summary>
        /// Sets the export size. Sets the <c>-w</c> and <c>-h</c> arguments.
        /// </summary>
        /// <param name="exportWidth">The export width.</param>
        /// <param name="exportHeight">The export width.</param>
        /// <returns>this</returns>
        public InkscapeOptions SetExportSize(int? exportWidth, int? exportHeight)
            => SetExportWidth(exportWidth).SetExportHeight(exportHeight);

        /// <summary>
        /// Sets the import file.
        /// </summary>
        /// <param name="importFile">The import file.</param>
        /// <returns>this</returns>
        public InkscapeOptions SetImportFile(FileInfo? importFile) { ImportFile = importFile; return this; }
    }
}
