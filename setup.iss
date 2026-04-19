; -----------------------------------------------------------------------
; RightClickPNG — Inno Setup Installer Script
; Automatically registers context menu entries during installation
; -----------------------------------------------------------------------

#define MyAppName "RightClickPNG"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "RightClickPNG"
#define MyAppURL "https://github.com/eneswritescode/RightClickPNG"
#define MyAppExeName "RightClickPNG.exe"
#define PublishDir "bin\Release\net8.0-windows\win-x64\publish"

[Setup]
AppId={{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}/releases
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=no
LicenseFile=LICENSE
OutputDir=installer_output
OutputBaseFilename=RightClickPNG_Setup_v{#MyAppVersion}
Compression=lzma2/ultra64
SolidCompression=yes
PrivilegesRequired=admin
WizardStyle=modern
SetupIconFile=icon.ico
UninstallDisplayIcon={app}\{#MyAppExeName}
ArchitecturesInstallIn64BitMode=x64compatible
MinVersion=10.0

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "turkish"; MessagesFile: "compiler:Languages\Turkish.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Files]
Source: "{#PublishDir}\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "icon.ico"; DestDir: "{app}"; Flags: ignoreversion

[Registry]
Root: HKCR; Subkey: "SystemFileAssociations\.webp\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.webp\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.webp\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.heic\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.heic\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.heic\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.heif\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.heif\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.heif\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.jpg\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.jpg\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.jpg\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.jpeg\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.jpeg\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.jpeg\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.bmp\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.bmp\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.bmp\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.tif\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.tif\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.tif\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.tiff\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.tiff\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.tiff\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.gif\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.gif\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.gif\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.avif\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.avif\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.avif\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.jxl\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.jxl\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.jxl\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

Root: HKCR; Subkey: "SystemFileAssociations\.ico\shell\RightClickPNG"; ValueType: string; ValueName: ""; ValueData: "Convert to PNG"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SystemFileAssociations\.ico\shell\RightClickPNG"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\icon.ico"
Root: HKCR; Subkey: "SystemFileAssociations\.ico\shell\RightClickPNG\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

[Code]
function InitializeSetup(): Boolean;
var
  InstallPath: string;
begin
  Result := True;
  
  // Uygulamanın önceden yüklenip yüklenmediği kontrolü (Kayıt defterindeki Uninstall string değerini check ediyoruz)
  if RegQueryStringValue(HKLM, 'Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}_is1', 'UninstallString', InstallPath) or
     RegQueryStringValue(HKLM, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}_is1', 'UninstallString', InstallPath) or
     RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}_is1', 'UninstallString', InstallPath) then
  begin
    if MsgBox('RightClickPNG bilgisayarınızda zaten yüklü görünüyor. Mevcut sürümün üzerine kurmak/güncellemek istediğinize emin misiniz?', mbConfirmation, MB_YESNO) = IDNO then
    begin
      Result := False;
    end;
  end;
end;

// ── Localized context menu labels ─────────────────────────────────
// Inno Setup [Registry] section uses static values, so we override
// them at runtime based on the selected installer language.

function GetLocalizedMenuLabel(): String;
begin
  case ActiveLanguage of
    'turkish': Result := 'PNG''ye Dönüştür';
    'french':  Result := 'Convertir en PNG';
    'russian': Result := #1050#1086#1085#1074#1077#1088#1090#1080#1088#1086#1074#1072#1090#1100' '#1074' PNG';
    'spanish': Result := 'Convertir a PNG';
  else
    Result := 'Convert to PNG';
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  MenuLabel: String;
begin
  if CurStep = ssPostInstall then
  begin
    MenuLabel := GetLocalizedMenuLabel();

    // Update registry values with the localized label
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.webp\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.heic\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.heif\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.jpg\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.jpeg\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.bmp\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.tif\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.tiff\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.gif\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.avif\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.jxl\shell\RightClickPNG', '', MenuLabel);
    RegWriteStringValue(HKEY_CLASSES_ROOT, 'SystemFileAssociations\.ico\shell\RightClickPNG', '', MenuLabel);

    // Save selected language code to the registry for the application to read
    case ActiveLanguage of
      'turkish': RegWriteStringValue(HKEY_CURRENT_USER, 'Software\RightClickPNG', 'Language', 'tr');
      'french':  RegWriteStringValue(HKEY_CURRENT_USER, 'Software\RightClickPNG', 'Language', 'fr');
      'russian': RegWriteStringValue(HKEY_CURRENT_USER, 'Software\RightClickPNG', 'Language', 'ru');
      'spanish': RegWriteStringValue(HKEY_CURRENT_USER, 'Software\RightClickPNG', 'Language', 'es');
    else
      RegWriteStringValue(HKEY_CURRENT_USER, 'Software\RightClickPNG', 'Language', 'en');
    end;
  end;
end;
