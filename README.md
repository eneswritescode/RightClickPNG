# RightClickPNG

<p align="center">
  <strong>⚡ One-click Universal Image to PNG conversion directly from your Windows context menu</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet" alt=".NET 8" />
  <img src="https://img.shields.io/badge/License-MIT-green?style=flat-square" alt="MIT License" />
  <img src="https://img.shields.io/badge/Platform-Windows%2010%20%7C%2011-0078D6?style=flat-square&logo=windows" alt="Windows" />
  <img src="https://img.shields.io/badge/Formats-12%20Supported-ff69b4?style=flat-square" alt="12 Formats" />
  <img src="https://img.shields.io/badge/Languages-12%2B-orange?style=flat-square" alt="Many Languages" />
</p>

---

## ✨ Features

- **Blazing Fast Conversion**: Right-click almost any image file and convert it to a high-quality PNG in a single click.
- **Vast Format Support**: Converts WebP, HEIC, AVIF, JXL, JPG, GIF, and many more.
- **Windows 11 Native**: Fully supports the modern Windows 11 context menu, complete with a custom icon.
- **Alpha Channel Preservation**: Full transparency (RGBA) is accurately retained during conversion.
- **Seamless UI**: Conversion runs completely silently safely in the background. No annoying popups.
- **Smart Language Sync**: The language you select during installation is permanently saved and applied to your right-click menu instantly!
- **Desktop & Start Menu Shortcuts**: Easily accessible setup console through standard Windows shortcuts.
- **Safe & Non-Destructive**: Existing PNG files are never overwritten; numeric suffixes (e.g., \_1.png\) are added automatically.

---

## 📷 Supported Image Formats

RightClickPNG seamlessly integrates into the Windows Shell for the following file extensions:

- \.webp\ (Web Picture)
- \.heic\ / \.heif\ (High-Efficiency Image Container)
- \.jpg\ / \.jpeg\ (JPEG Image)
- \.avif\ (AV1 Image File Format)
- \.jxl\ (JPEG XL)
- \.bmp\ (Bitmap)
- \.gif\ (Graphics Interchange Format)
- \.tif\ / \.tiff\ (Tagged Image File Format)
- \.ico\ (Windows Icon)

---

## 🌍 Supported Languages

The installer and the Windows Context Menu are fully localized. When you pick your language in the installer, RightClickPNG remembers it! 

| Language   | Context Menu Label    | 
|------------|-----------------------|
| 🇬🇧 English  | Convert to PNG        | 
| 🇹🇷 Türkçe   | PNG'ye Dönüştür       | 
| 🇫🇷 Français | Convertir en PNG      | 
| 🇩🇪 Deutsch  | In PNG konvertieren   |
| 🇪🇸 Español  | Convertir a PNG       | 
| 🇮🇹 Italiano | In PNG converti       | 
| 🇷🇺 Русский  | Конвертировать в PNG  | 
| ...and more!|                       |

---

## 📦 Installation

### Prerequisites
- **Windows 10 or Windows 11** (x64)
- [.NET 8.0 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0) 

### Quick Install (Recommended)
1. Download the latest \RightClickPNG_Setup_v1.x.x.exe\ from the [Releases](../../releases) page.
2. Run the installer. You can choose to create Desktop or Start Menu shortcuts.
3. Choose your preferred language.
4. You're done! Right-click any supported image to see the new context menu option.

---

## 🚀 Usage

### 1. Context Menu (The Main Feature)
Simply right-click on any supported photo (e.g., \photo.heic\ or \image.webp\) and click **"Convert to PNG"** (or your localized equivalent). A new \.png\ file will instantly appear in the same folder.

### 2. Manual Setup Console
You can also launch \RightClickPNG\ directly from your Desktop, Start Menu, or installation folder. This will open a console-based Setup Manager that lets you:
- Manually Add/Repair the Right-Click menu entries.
- Manually Remove the Right-Click menu entries.

> ⚠️ *Note: Running the setup console requires Administrator privileges to modify the Windows Registry.*

---

## 🛠️ Build from Source

We've made building the application and the installer completely automated! 

### Requirements
- **.NET 8.0 SDK**
- **Inno Setup 6** (Installed at default \AppData\Local\Programs\Inno Setup 6\ or \Program Files (x86)\)

### One-Click Build
Simply run the included batch script:

\\\at
.\build_installer.bat
\\\

**What the script does:**
1. Cleans old builds.
2. Publishes a highly-optimized, framework-dependent \.NET 8\ Release build.
3. Invokes the Inno Setup Compiler (\iscc.exe\).
4. Injects the standalone \icon.ico\ directly into the build so Windows 11 can render it properly.
5. Outputs the final ready-to-distribute \.exe\ into the \installer_output/\ directory.

---

## 🏗️ Project Structure

\\\	ext
RightClickPNG/
├── Program.cs              # Entry point — CLI routing & manual setup UI
├── Converter.cs            # Magick.NET image conversion logic (Supports 12+ formats)
├── LocalizationManager.cs  # Multi-language dictionary fetching translations from Registry
├── RegistryManager.cs      # CLI-based Registry writer & repair tool
├── RightClickPNG.csproj    # .NET build configuration
├── setup.iss               # Inno Setup installer script (Handles native Shell integration)
├── build_installer.bat     # One-click build and package automation script
├── icon.ico                # Standalone icon for the Windows 11 Context Menu
└── README.md               # You are here
\\\

### 🔧 Architecture Details

- **ImageMagick Integration:** The project utilizes \Magick.NET\ for enterprise-grade image decoding (essential for formats like AVIF, HEIC, and JXL) with deep alpha-channel accuracy.
- **Persistent Localization:** The Inno Setup script writes your language choice to \HKCU\Software\RightClickPNG\. \LocalizationManager.cs\ reads this key at runtime to instantly display the correct context menu and console text.
- **Windows 11 Context Menus:** The installer maps keys to \HKCR\SystemFileAssociations\<extension>\shell\RightClickPNG\. To bypass Windows 11 shell extraction bugs, the icon is distributed physically as \icon.ico\ rather than embedded inside the compiled \.exe\.

---

## 🗑️ Uninstallation

RightClickPNG cleans up after itself perfectly.
1. Open **Windows Settings** -> **Apps** -> **Installed Apps**.
2. Find **RightClickPNG** and click **Uninstall**.
3. All context menu entries, registry keys, shortcuts, and application files are immediately removed!

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! 
Want to add a new language? 
1. Open \LocalizationManager.cs\ and \setup.iss\
2. Add your language dictionary.
3. Submit a Pull Request!

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 🙏 Acknowledgments

- [Magick.NET](https://github.com/dlemstra/Magick.NET) — The powerful .NET wrapper for ImageMagick.
- [Inno Setup](https://jrsoftware.org/isinfo.php) — For robust, scriptable Windows packaging.
