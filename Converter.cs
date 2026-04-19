// -----------------------------------------------------------------------
// RightClickPNG — Open-source WebP/HEIC to PNG context menu converter
// Converter.cs — Image conversion engine using Magick.NET
// -----------------------------------------------------------------------

using ImageMagick;

namespace RightClickPNG;

/// <summary>
/// Handles conversion of WebP and HEIC image files to PNG format
/// while preserving the alpha channel (transparency).
/// </summary>
public static class Converter
{
    private static readonly HashSet<string> SupportedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".webp", ".heic", ".heif", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff", ".gif", ".avif", ".jxl", ".ico"
    };

    public static bool IsSupportedFormat(string filePath)
    {
        var ext = Path.GetExtension(filePath);
        return !string.IsNullOrEmpty(ext) && SupportedExtensions.Contains(ext);
    }

    public static ConversionResult ConvertToPng(string sourceFilePath)
    {
        try
        {
            if (!File.Exists(sourceFilePath))
                return ConversionResult.Fail(sourceFilePath,
                    LocalizationManager.GetFormatted(LocalizationManager.Keys.FileNotFound, sourceFilePath));

            if (!IsSupportedFormat(sourceFilePath))
                return ConversionResult.Fail(sourceFilePath,
                    LocalizationManager.GetFormatted(LocalizationManager.Keys.UnsupportedFormat,
                        Path.GetExtension(sourceFilePath)));

            string outputPath = GetUniqueOutputPath(sourceFilePath);

            using var image = new MagickImage(sourceFilePath);
            image.Alpha(AlphaOption.Set);
            image.Format = MagickFormat.Png32;
            image.Settings.SetDefine(MagickFormat.Png, "color-type", "6");
            image.Write(outputPath);

            return ConversionResult.Ok(sourceFilePath, outputPath);
        }
        catch (Exception ex)
        {
            return ConversionResult.Fail(sourceFilePath, ex.Message);
        }
    }

    public static List<ConversionResult> ConvertAll(IEnumerable<string> filePaths)
    {
        var results = new List<ConversionResult>();
        foreach (var path in filePaths)
            results.Add(ConvertToPng(path));
        return results;
    }

    private static string GetUniqueOutputPath(string sourceFilePath)
    {
        string dir = Path.GetDirectoryName(sourceFilePath) ?? ".";
        string baseName = Path.GetFileNameWithoutExtension(sourceFilePath);
        string output = Path.Combine(dir, $"{baseName}.png");

        if (!File.Exists(output)) return output;

        int counter = 2;
        while (File.Exists(output))
        {
            output = Path.Combine(dir, $"{baseName} ({counter}).png");
            counter++;
        }
        return output;
    }
}

public sealed class ConversionResult
{
    public bool Success { get; private init; }
    public string SourcePath { get; private init; } = string.Empty;
    public string OutputPath { get; private init; } = string.Empty;
    public string ErrorMessage { get; private init; } = string.Empty;

    public static ConversionResult Ok(string source, string output) => new()
    { Success = true, SourcePath = source, OutputPath = output };

    public static ConversionResult Fail(string source, string error) => new()
    { Success = false, SourcePath = source, ErrorMessage = error };
}
