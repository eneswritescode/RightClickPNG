// -----------------------------------------------------------------------
// RightClickPNG — Open-source WebP/HEIC to PNG context menu converter
// Program.cs — Entry point, mode selection, and argument routing
// -----------------------------------------------------------------------

using System.Runtime.Versioning;

namespace RightClickPNG;

/// <summary>
/// Application entry point. Operates in two modes:
///   1. Conversion Mode — when file paths are passed as arguments (silent, no window).
///   2. Setup Mode      — when launched without arguments (modern UI panel).
/// </summary>
[SupportedOSPlatform("windows")]
public static class Program
{
    private static readonly string LogFilePath =
        Path.Combine(AppContext.BaseDirectory, "error_log.txt");

    [STAThread]
    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            // ── Conversion Mode ────────────────────────────────────
            // Silent — no window, no UI, just convert and exit.
            RunConversionMode(args);
        }
        else
        {
            // ── Setup Mode ─────────────────────────────────────────
            // Show the modern notification panel for install/uninstall.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SetupForm());
        }
    }

    /// <summary>
    /// Silently converts all provided file paths to PNG.
    /// Errors are logged to error_log.txt without any UI.
    /// </summary>
    private static void RunConversionMode(string[] filePaths)
    {
        var results = Converter.ConvertAll(filePaths);

        foreach (var result in results)
        {
            if (!result.Success)
            {
                LogError(result.SourcePath, result.ErrorMessage);
            }
        }
    }

    /// <summary>
    /// Appends an error entry to the log file silently.
    /// </summary>
    private static void LogError(string filePath, string error)
    {
        try
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string entry = $"[{timestamp}] {filePath} — {error}{Environment.NewLine}";
            File.AppendAllText(LogFilePath, entry);
        }
        catch
        {
            // If we can't even write a log, fail silently — no user disruption.
        }
    }
}
