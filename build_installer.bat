@echo off
echo ===========================================
echo RightClickPNG - Build and Package Script
echo ===========================================

echo.
echo [1] Publishing .NET Project (win-x64 Release)...
dotnet publish -c Release -r win-x64
if %errorlevel% neq 0 (
    echo [ERROR] .NET Publish failed.
    pause
    exit /b %errorlevel%
)

echo.
echo [2] Compiling Inno Setup Installer...
set "INNO_COMPILER=%LOCALAPPDATA%\Programs\Inno Setup 6\ISCC.exe"

if not exist "%INNO_COMPILER%" (
    echo [ERROR] Inno Setup Compiler not found at: %INNO_COMPILER%
    echo Please install Inno Setup 6 or update the path in this script.
    pause
    exit /b 1
)

"%INNO_COMPILER%" setup.iss
if %errorlevel% neq 0 (
    echo [ERROR] Inno Setup compilation failed.
    pause
    exit /b %errorlevel%
)

echo.
echo ===========================================
echo [SUCCESS] Installer successfully created!
echo Location: installer_output\
echo ===========================================
pause