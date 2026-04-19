// -----------------------------------------------------------------------
// RightClickPNG — Open-source WebP/HEIC to PNG context menu converter
// RegistryManager.cs — Windows Registry context menu integration
// -----------------------------------------------------------------------

using Microsoft.Win32;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace RightClickPNG;

/// <summary>
/// Manages Windows Registry entries for integrating RightClickPNG
/// into the shell context menu for supported image file types.
/// Requires Administrator privileges for HKEY_CLASSES_ROOT access.
/// </summary>
[SupportedOSPlatform("windows")]
public static class RegistryManager
{
    private const string ShellKeyName = "RightClickPNG";
    private const string CommandSubKey = "command";

    /// <summary>
    /// File type registry paths where the context menu entry will be added.
    /// These correspond to the file extensions supported by the converter.
    /// </summary>
    private static readonly string[] FileTypeKeys =
    {
        @"SystemFileAssociations\.webp\shell",
        @"SystemFileAssociations\.heic\shell",
        @"SystemFileAssociations\.heif\shell",
        @"SystemFileAssociations\.jpg\shell",
        @"SystemFileAssociations\.jpeg\shell",
        @"SystemFileAssociations\.bmp\shell",
        @"SystemFileAssociations\.tif\shell",
        @"SystemFileAssociations\.tiff\shell",
        @"SystemFileAssociations\.gif\shell",
        @"SystemFileAssociations\.avif\shell",
        @"SystemFileAssociations\.jxl\shell",
        @"SystemFileAssociations\.ico\shell"
    };

    /// <summary>
    /// Checks whether the current process is running with Administrator privileges.
    /// </summary>
    public static bool IsRunningAsAdmin()
    {
        using var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>
    /// Checks whether the context menu entries are currently installed.
    /// </summary>
    public static bool IsInstalled()
    {
        try
        {
            var testPath = $@"{FileTypeKeys[0]}\{ShellKeyName}";
            using var key = Registry.ClassesRoot.OpenSubKey(testPath, false);
            return key is not null;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Adds the "Convert to PNG" context menu entry for all supported image types.
    /// The menu label is determined by the current system language.
    /// </summary>
    /// <returns><c>true</c> if all entries were created successfully.</returns>
    public static bool Install()
    {
        if (!IsRunningAsAdmin()) return false;

        try
        {
            string exePath = GetExecutablePath();
            string iconPath = Path.Combine(AppContext.BaseDirectory, "icon.ico");
            string menuLabel = LocalizationManager.Get(LocalizationManager.Keys.MenuConvertToPng);

            foreach (var fileTypeKey in FileTypeKeys)
            {
                string fullKeyPath = $@"{fileTypeKey}\{ShellKeyName}";

                using var shellKey = Registry.ClassesRoot.CreateSubKey(fullKeyPath, true);
                if (shellKey is null) continue;

                shellKey.SetValue("", menuLabel);
                shellKey.SetValue("Icon", $"\"{iconPath}\"");

                using var cmdKey = shellKey.CreateSubKey(CommandSubKey, true);
                cmdKey?.SetValue("", $"\"{exePath}\" \"%1\"");
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Removes the "Convert to PNG" context menu entry for all supported image types.
    /// </summary>
    /// <returns><c>true</c> if all entries were removed successfully.</returns>
    public static bool Uninstall()
    {
        if (!IsRunningAsAdmin()) return false;

        try
        {
            foreach (var fileTypeKey in FileTypeKeys)
            {
                string fullKeyPath = $@"{fileTypeKey}\{ShellKeyName}";

                try
                {
                    Registry.ClassesRoot.DeleteSubKeyTree(fullKeyPath, false);
                }
                catch (ArgumentException)
                {
                    // Key does not exist — continue silently
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Returns the full path to the currently running executable.
    /// </summary>
    private static string GetExecutablePath()
    {
        return System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName
            ?? AppContext.BaseDirectory + "RightClickPNG.exe";
    }
}
