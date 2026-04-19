// -----------------------------------------------------------------------
// RightClickPNG — Open-source WebP/HEIC → PNG context menu converter
// LocalizationManager.cs — Culture-aware localization provider
// -----------------------------------------------------------------------

using System.Globalization;
using Microsoft.Win32;

namespace RightClickPNG;

/// <summary>
/// Provides culture-aware localized strings for the application.
/// Automatically detects the system UI culture and falls back to English.
/// Supported languages: English (default), Turkish, French, Russian, Spanish.
/// </summary>
public static class LocalizationManager
{
    /// <summary>
    /// Supported locale keys used throughout the application.
    /// </summary>
    public static class Keys
    {
        public const string AppTitle              = "AppTitle";
        public const string MenuConvertToPng      = "MenuConvertToPng";
        public const string OptionInstall         = "OptionInstall";
        public const string OptionUninstall       = "OptionUninstall";
        public const string OptionExit            = "OptionExit";
        public const string PromptChoice          = "PromptChoice";
        public const string InstallSuccess        = "InstallSuccess";
        public const string UninstallSuccess      = "UninstallSuccess";
        public const string InstallFailed         = "InstallFailed";
        public const string UninstallFailed       = "UninstallFailed";
        public const string AdminRequired         = "AdminRequired";
        public const string ConvertingFile        = "ConvertingFile";
        public const string ConversionSuccess     = "ConversionSuccess";
        public const string ConversionFailed      = "ConversionFailed";
        public const string FileNotFound          = "FileNotFound";
        public const string UnsupportedFormat     = "UnsupportedFormat";
        public const string InvalidChoice         = "InvalidChoice";
        public const string PressAnyKey           = "PressAnyKey";
        public const string AlreadyInstalled      = "AlreadyInstalled";
        public const string NotInstalled          = "NotInstalled";
    }

    private static readonly string _currentLanguage;

    private static readonly Dictionary<string, Dictionary<string, string>> _translations = new()
    {
        // ── English (Default) ──────────────────────────────────────────
        ["en"] = new Dictionary<string, string>
        {
            [Keys.AppTitle]           = "RightClickPNG — WebP/HEIC to PNG Converter",
            [Keys.MenuConvertToPng]   = "Convert to PNG",
            [Keys.OptionInstall]      = "Add to Right-Click Menu",
            [Keys.OptionUninstall]    = "Remove from Right-Click Menu",
            [Keys.OptionExit]         = "Exit",
            [Keys.PromptChoice]      = "Select an option (1-3):",
            [Keys.InstallSuccess]    = "✓ Context menu entry added successfully.",
            [Keys.UninstallSuccess]  = "✓ Context menu entry removed successfully.",
            [Keys.InstallFailed]     = "✗ Failed to add context menu entry.",
            [Keys.UninstallFailed]   = "✗ Failed to remove context menu entry.",
            [Keys.AdminRequired]     = "⚠ Administrator privileges are required. Please run as Administrator.",
            [Keys.ConvertingFile]    = "Converting: {0}",
            [Keys.ConversionSuccess] = "✓ Saved: {0}",
            [Keys.ConversionFailed]  = "✗ Conversion failed for: {0}",
            [Keys.FileNotFound]      = "✗ File not found: {0}",
            [Keys.UnsupportedFormat] = "✗ Unsupported format: {0}",
            [Keys.InvalidChoice]     = "Invalid selection. Please try again.",
            [Keys.PressAnyKey]       = "Press any key to exit...",
            [Keys.AlreadyInstalled]  = "Context menu entry is already installed.",
            [Keys.NotInstalled]      = "Context menu entry is not currently installed.",
        },

        // ── Türkçe ────────────────────────────────────────────────────
        ["tr"] = new Dictionary<string, string>
        {
            [Keys.AppTitle]           = "RightClickPNG — WebP/HEIC → PNG Dönüştürücü",
            [Keys.MenuConvertToPng]   = "PNG'ye Dönüştür",
            [Keys.OptionInstall]      = "Sağ Tık Menüsüne Ekle",
            [Keys.OptionUninstall]    = "Sağ Tık Menüsünden Kaldır",
            [Keys.OptionExit]         = "Çıkış",
            [Keys.PromptChoice]      = "Bir seçenek belirleyin (1-3):",
            [Keys.InstallSuccess]    = "✓ Sağ tık menüsüne başarıyla eklendi.",
            [Keys.UninstallSuccess]  = "✓ Sağ tık menüsünden başarıyla kaldırıldı.",
            [Keys.InstallFailed]     = "✗ Sağ tık menüsüne eklenemedi.",
            [Keys.UninstallFailed]   = "✗ Sağ tık menüsünden kaldırılamadı.",
            [Keys.AdminRequired]     = "⚠ Yönetici izni gereklidir. Lütfen Yönetici olarak çalıştırın.",
            [Keys.ConvertingFile]    = "Dönüştürülüyor: {0}",
            [Keys.ConversionSuccess] = "✓ Kaydedildi: {0}",
            [Keys.ConversionFailed]  = "✗ Dönüştürme başarısız: {0}",
            [Keys.FileNotFound]      = "✗ Dosya bulunamadı: {0}",
            [Keys.UnsupportedFormat] = "✗ Desteklenmeyen format: {0}",
            [Keys.InvalidChoice]     = "Geçersiz seçim. Lütfen tekrar deneyin.",
            [Keys.PressAnyKey]       = "Çıkmak için bir tuşa basın...",
            [Keys.AlreadyInstalled]  = "Sağ tık menüsü zaten kurulu.",
            [Keys.NotInstalled]      = "Sağ tık menüsü şu anda kurulu değil.",
        },

        // ── Français ──────────────────────────────────────────────────
        ["fr"] = new Dictionary<string, string>
        {
            [Keys.AppTitle]           = "RightClickPNG — Convertisseur WebP/HEIC vers PNG",
            [Keys.MenuConvertToPng]   = "Convertir en PNG",
            [Keys.OptionInstall]      = "Ajouter au Menu Contextuel",
            [Keys.OptionUninstall]    = "Supprimer du Menu Contextuel",
            [Keys.OptionExit]         = "Quitter",
            [Keys.PromptChoice]      = "Sélectionnez une option (1-3) :",
            [Keys.InstallSuccess]    = "✓ Entrée du menu contextuel ajoutée avec succès.",
            [Keys.UninstallSuccess]  = "✓ Entrée du menu contextuel supprimée avec succès.",
            [Keys.InstallFailed]     = "✗ Échec de l'ajout de l'entrée du menu contextuel.",
            [Keys.UninstallFailed]   = "✗ Échec de la suppression de l'entrée du menu contextuel.",
            [Keys.AdminRequired]     = "⚠ Des privilèges administrateur sont requis. Veuillez exécuter en tant qu'administrateur.",
            [Keys.ConvertingFile]    = "Conversion : {0}",
            [Keys.ConversionSuccess] = "✓ Enregistré : {0}",
            [Keys.ConversionFailed]  = "✗ Échec de la conversion : {0}",
            [Keys.FileNotFound]      = "✗ Fichier introuvable : {0}",
            [Keys.UnsupportedFormat] = "✗ Format non pris en charge : {0}",
            [Keys.InvalidChoice]     = "Sélection invalide. Veuillez réessayer.",
            [Keys.PressAnyKey]       = "Appuyez sur une touche pour quitter...",
            [Keys.AlreadyInstalled]  = "L'entrée du menu contextuel est déjà installée.",
            [Keys.NotInstalled]      = "L'entrée du menu contextuel n'est pas installée.",
        },

        // ── Русский ───────────────────────────────────────────────────
        ["ru"] = new Dictionary<string, string>
        {
            [Keys.AppTitle]           = "RightClickPNG — Конвертер WebP/HEIC в PNG",
            [Keys.MenuConvertToPng]   = "Конвертировать в PNG",
            [Keys.OptionInstall]      = "Добавить в контекстное меню",
            [Keys.OptionUninstall]    = "Удалить из контекстного меню",
            [Keys.OptionExit]         = "Выход",
            [Keys.PromptChoice]      = "Выберите опцию (1-3):",
            [Keys.InstallSuccess]    = "✓ Пункт контекстного меню успешно добавлен.",
            [Keys.UninstallSuccess]  = "✓ Пункт контекстного меню успешно удалён.",
            [Keys.InstallFailed]     = "✗ Не удалось добавить пункт контекстного меню.",
            [Keys.UninstallFailed]   = "✗ Не удалось удалить пункт контекстного меню.",
            [Keys.AdminRequired]     = "⚠ Требуются права администратора. Пожалуйста, запустите от имени администратора.",
            [Keys.ConvertingFile]    = "Конвертация: {0}",
            [Keys.ConversionSuccess] = "✓ Сохранено: {0}",
            [Keys.ConversionFailed]  = "✗ Ошибка конвертации: {0}",
            [Keys.FileNotFound]      = "✗ Файл не найден: {0}",
            [Keys.UnsupportedFormat] = "✗ Неподдерживаемый формат: {0}",
            [Keys.InvalidChoice]     = "Неверный выбор. Попробуйте снова.",
            [Keys.PressAnyKey]       = "Нажмите любую клавишу для выхода...",
            [Keys.AlreadyInstalled]  = "Пункт контекстного меню уже установлен.",
            [Keys.NotInstalled]      = "Пункт контекстного меню не установлен.",
        },

        // ── Español ───────────────────────────────────────────────────
        ["es"] = new Dictionary<string, string>
        {
            [Keys.AppTitle]           = "RightClickPNG — Convertidor WebP/HEIC a PNG",
            [Keys.MenuConvertToPng]   = "Convertir a PNG",
            [Keys.OptionInstall]      = "Agregar al Menú Contextual",
            [Keys.OptionUninstall]    = "Eliminar del Menú Contextual",
            [Keys.OptionExit]         = "Salir",
            [Keys.PromptChoice]      = "Seleccione una opción (1-3):",
            [Keys.InstallSuccess]    = "✓ Entrada del menú contextual agregada exitosamente.",
            [Keys.UninstallSuccess]  = "✓ Entrada del menú contextual eliminada exitosamente.",
            [Keys.InstallFailed]     = "✗ Error al agregar la entrada del menú contextual.",
            [Keys.UninstallFailed]   = "✗ Error al eliminar la entrada del menú contextual.",
            [Keys.AdminRequired]     = "⚠ Se requieren privilegios de administrador. Ejecute como Administrador.",
            [Keys.ConvertingFile]    = "Convirtiendo: {0}",
            [Keys.ConversionSuccess] = "✓ Guardado: {0}",
            [Keys.ConversionFailed]  = "✗ Error en la conversión: {0}",
            [Keys.FileNotFound]      = "✗ Archivo no encontrado: {0}",
            [Keys.UnsupportedFormat] = "✗ Formato no compatible: {0}",
            [Keys.InvalidChoice]     = "Selección inválida. Intente de nuevo.",
            [Keys.PressAnyKey]       = "Presione cualquier tecla para salir...",
            [Keys.AlreadyInstalled]  = "La entrada del menú contextual ya está instalada.",
            [Keys.NotInstalled]      = "La entrada del menú contextual no está instalada.",
        },
    };

    /// <summary>
    /// Static constructor — detects the system UI culture on first access.
    /// </summary>
    static LocalizationManager()
    {
        string? culture = null;
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\RightClickPNG");
            culture = key?.GetValue("Language") as string;
        }
        catch
        {
            // Ignore registry errors
        }

        if (string.IsNullOrEmpty(culture))
        {
            culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLowerInvariant();
        }

        _currentLanguage = _translations.ContainsKey(culture) ? culture : "en";
    }

    /// <summary>
    /// Returns the current resolved language code (e.g. "en", "tr", "fr").
    /// </summary>
    public static string CurrentLanguage => _currentLanguage;

    /// <summary>
    /// Retrieves a localized string for the given key.
    /// Falls back to English if the key is missing in the current language.
    /// </summary>
    /// <param name="key">A key from <see cref="Keys"/>.</param>
    /// <returns>The localized string, or the key itself if not found.</returns>
    public static string Get(string key)
    {
        if (_translations.TryGetValue(_currentLanguage, out var locale) &&
            locale.TryGetValue(key, out var value))
        {
            return value;
        }

        // Fallback to English
        if (_translations["en"].TryGetValue(key, out var fallback))
        {
            return fallback;
        }

        return key;
    }

    /// <summary>
    /// Retrieves a localized string and applies <see cref="string.Format(string, object[])"/>.
    /// </summary>
    /// <param name="key">A key from <see cref="Keys"/>.</param>
    /// <param name="args">Format arguments.</param>
    /// <returns>The formatted localized string.</returns>
    public static string GetFormatted(string key, params object[] args)
    {
        return string.Format(Get(key), args);
    }
}
