// -----------------------------------------------------------------------
// RightClickPNG — Open-source WebP/HEIC to PNG context menu converter
// SetupForm.cs — Windows 11 Fluent Design style setup panel
// -----------------------------------------------------------------------

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace RightClickPNG;

/// <summary>
/// A Windows 11 Fluent Design inspired setup panel for installing
/// or uninstalling the context menu entry.
/// </summary>
[SupportedOSPlatform("windows")]
public sealed class SetupForm : Form
{
    // ── Windows 11 Color Palette ───────────────────────────────────
    private static readonly Color BgWindow      = Color.FromArgb(243, 243, 243);
    private static readonly Color BgCard        = Color.FromArgb(255, 255, 255);
    private static readonly Color AccentBlue    = Color.FromArgb(0, 103, 192);
    private static readonly Color AccentBlueLt  = Color.FromArgb(0, 120, 212);
    private static readonly Color BtnDefault    = Color.FromArgb(251, 251, 251);
    private static readonly Color BtnDefaultBrd = Color.FromArgb(204, 204, 204);
    private static readonly Color BtnHover      = Color.FromArgb(238, 238, 238);
    private static readonly Color BtnPress      = Color.FromArgb(224, 224, 224);
    private static readonly Color TextDark      = Color.FromArgb(26, 26, 26);
    private static readonly Color TextMuted     = Color.FromArgb(96, 96, 96);
    private static readonly Color DividerColor  = Color.FromArgb(225, 225, 225);
    private static readonly Color SuccessGreen  = Color.FromArgb(15, 123, 15);
    private static readonly Color ErrorRed      = Color.FromArgb(196, 43, 28);

    // ── DWM Rounded Corners (Windows 11) ───────────────────────────
    private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
    private const int DWMWCP_ROUND = 2;

    [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
    private static extern void DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int pvAttribute, int cbAttribute);

    // ── Borderless Dragging ────────────────────────────────────────
    private const int WM_NCLBUTTONDOWN = 0xA1;
    private const int HT_CAPTION = 0x2;

    [DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    // ── UI Elements ────────────────────────────────────────────────
    private readonly Label _lblStatus;

    public SetupForm()
    {
        SuspendLayout();

        // ── Form ───────────────────────────────────────────────────
        Text = "RightClickPNG";
        ClientSize = new Size(400, 280);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = BgWindow;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowIcon = false;
        Font = new Font("Segoe UI", 9f);
        DoubleBuffered = true;

        // ── Header area ────────────────────────────────────────────
        var pnlHeader = new Panel
        {
            Location = new Point(0, 0),
            Size = new Size(400, 100),
            BackColor = BgWindow,
        };
        pnlHeader.Paint += (_, e) =>
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // App icon circle
            using var circleBrush = new SolidBrush(AccentBlue);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(circleBrush, 28, 20, 48, 48);

            // Icon text inside circle
            using var iconFont = new Font("Segoe UI", 18f, FontStyle.Bold);
            using var whiteBrush = new SolidBrush(Color.White);
            var iconSize = e.Graphics.MeasureString("P", iconFont);
            float iconX = 28 + (48 - iconSize.Width) / 2 + 1;
            float iconY = 20 + (48 - iconSize.Height) / 2;
            e.Graphics.DrawString("P", iconFont, whiteBrush, iconX, iconY);

            // Title
            using var titleFont = new Font("Segoe UI Semibold", 15f);
            using var darkBrush = new SolidBrush(TextDark);
            e.Graphics.DrawString("RightClickPNG", titleFont, darkBrush, 88, 20);

            // Subtitle
            using var subFont = new Font("Segoe UI", 9.5f);
            using var mutedBrush = new SolidBrush(TextMuted);
            e.Graphics.DrawString("WebP / HEIC  →  PNG  •  v1.0", subFont, mutedBrush, 90, 50);
        };

        // ── Divider ────────────────────────────────────────────────
        var divider = new Panel
        {
            Location = new Point(28, 100),
            Size = new Size(344, 1),
            BackColor = DividerColor,
        };

        // ── Install Button ─────────────────────────────────────────
        var btnInstall = CreateButton(
            LocalizationManager.Get(LocalizationManager.Keys.OptionInstall),
            isPrimary: true
        );
        btnInstall.Location = new Point(28, 120);
        btnInstall.Size = new Size(344, 40);
        btnInstall.Click += OnInstallClick;

        // ── Uninstall Button ───────────────────────────────────────
        var btnUninstall = CreateButton(
            LocalizationManager.Get(LocalizationManager.Keys.OptionUninstall),
            isPrimary: false
        );
        btnUninstall.Location = new Point(28, 172);
        btnUninstall.Size = new Size(344, 40);
        btnUninstall.Click += OnUninstallClick;

        // ── Status Label ───────────────────────────────────────────
        _lblStatus = new Label
        {
            Text = "",
            Font = new Font("Segoe UI", 9f),
            ForeColor = TextMuted,
            Location = new Point(28, 228),
            Size = new Size(344, 36),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = BgWindow,
        };

        // ── Assemble ───────────────────────────────────────────────
        Controls.AddRange(new Control[] { pnlHeader, divider, btnInstall, btnUninstall, _lblStatus });

        ResumeLayout(false);
    }

    // ── Apply Windows 11 rounded corners on handle creation ────────
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        try
        {
            int preference = DWMWCP_ROUND;
            DwmSetWindowAttribute(Handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(int));
        }
        catch
        {
            // Not Windows 11 — ignore
        }
    }

    // ── Button Factory ─────────────────────────────────────────────

    private static Button CreateButton(string text, bool isPrimary)
    {
        var btn = new Button
        {
            Text = text,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI Semibold", 10f),
            Cursor = Cursors.Hand,
            TextAlign = ContentAlignment.MiddleCenter,
        };

        if (isPrimary)
        {
            btn.BackColor = AccentBlue;
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = AccentBlueLt;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 84, 153);
        }
        else
        {
            btn.BackColor = BtnDefault;
            btn.ForeColor = TextDark;
            btn.FlatAppearance.BorderColor = BtnDefaultBrd;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseOverBackColor = BtnHover;
            btn.FlatAppearance.MouseDownBackColor = BtnPress;
        }

        return btn;
    }

    // ── Event Handlers ─────────────────────────────────────────────

    private void OnInstallClick(object? sender, EventArgs e)
    {
        if (!RegistryManager.IsRunningAsAdmin())
        {
            ShowStatus(LocalizationManager.Get(LocalizationManager.Keys.AdminRequired), ErrorRed);
            return;
        }

        if (RegistryManager.IsInstalled())
        {
            ShowStatus(LocalizationManager.Get(LocalizationManager.Keys.AlreadyInstalled), AccentBlue);
            return;
        }

        bool ok = RegistryManager.Install();
        ShowStatus(
            LocalizationManager.Get(ok
                ? LocalizationManager.Keys.InstallSuccess
                : LocalizationManager.Keys.InstallFailed),
            ok ? SuccessGreen : ErrorRed);
    }

    private void OnUninstallClick(object? sender, EventArgs e)
    {
        if (!RegistryManager.IsRunningAsAdmin())
        {
            ShowStatus(LocalizationManager.Get(LocalizationManager.Keys.AdminRequired), ErrorRed);
            return;
        }

        if (!RegistryManager.IsInstalled())
        {
            ShowStatus(LocalizationManager.Get(LocalizationManager.Keys.NotInstalled), AccentBlue);
            return;
        }

        bool ok = RegistryManager.Uninstall();
        ShowStatus(
            LocalizationManager.Get(ok
                ? LocalizationManager.Keys.UninstallSuccess
                : LocalizationManager.Keys.UninstallFailed),
            ok ? SuccessGreen : ErrorRed);
    }

    private void ShowStatus(string message, Color color)
    {
        _lblStatus.ForeColor = color;
        _lblStatus.Text = message;
    }
}
