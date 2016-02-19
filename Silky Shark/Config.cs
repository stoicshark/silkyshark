using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Silky_Shark
{
    public class Config
    {
        private Main mainForm;
        private Overlay overlayForm;
        public Point tabletOffset = new Point(0, 0);
        public Rectangle overrideBounds = new Rectangle(0, 0, 0, 0);
        public int smoothingStrength = 30;
        public int smoothingInterpolation = 4;
        public int overlayScreen = 0;
        public int tolerance = 300;
        public bool manualInterpolation = false;
        public bool stayOnTop = false;
        public bool disableOverlay = false;
        public bool allScreens = false;
        public bool manualOverlayOverride = false;
        public bool disableCatchUp = false;
        public bool snapToCursor = false;
        public bool smoothOnDraw = false;
        public bool tabletOffsetOverride = false;
        public bool disableAutoDetection = false;
        public string[] hotkeys = { "None", "None", "None", "None" };

        public Config(Main m, Overlay o)
        {
            mainForm = m;
            overlayForm = o;
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", "Silky Shark.config");
        }

        public void LoadConfig(bool def = false)
        {
            if (def)
            {
                tabletOffset = new Point(0, 0);
                overrideBounds = new Rectangle(0, 0, 0, 0);
                smoothingStrength = 30;
                smoothingInterpolation = 4;
                overlayScreen = 0;
                tolerance = 300;
                manualInterpolation = false;
                stayOnTop = false;
                disableOverlay = false;
                allScreens = false;
                manualOverlayOverride = false;
                disableCatchUp = false;
                snapToCursor = false;
                smoothOnDraw = false;
                tabletOffsetOverride = false;
                disableAutoDetection = false;
                hotkeys[0] = "None";
                hotkeys[1] = "None";
                hotkeys[2] = "None";
                hotkeys[3] = "None";

                // Main window resetting
                mainForm.checkBox_smoothOnDraw.Checked = false;
                mainForm.checkBox_stayOnTop.Checked = false;
                mainForm.checkBox_tabletMode.Checked = false;
                mainForm.checkBox_tabletMode.Enabled = false;
                mainForm.checkBox_manualInterpolation.Checked = false;
                mainForm.trackBar_smoothingInterpolation.Enabled = false;
                mainForm.textBox_smoothingInterpolation.Enabled = false;
                mainForm.textBox_smoothingInterpolation.Text = smoothingInterpolation.ToString();
                mainForm.textBox_smoothingStrength.Text = smoothingStrength.ToString();
                mainForm.checkBox_smoothOnDraw.Checked = false;
                mainForm.TopMost = false;

                // Cursor and overlay resetting
                overlayForm.cursorColor = Color.FromArgb(128, 128, 128);
                overlayForm.cursorFillColor = Color.FromArgb(255, 255, 254);
                overlayForm.cursorType = Overlay.CursorType.Bullseye;
                overlayForm.Show();
                overlayForm.Bounds = Screen.PrimaryScreen.Bounds;
                mainForm.button_colorDialog.BackColor = overlayForm.cursorColor;

                // Hotkey resetting
                for (int i = 0; i < mainForm.hotKeyHandling.Count(); i++)
                {
                    try
                    {
                        mainForm.hotKeyHandling[i].Dispose();
                    }
                    catch
                    {
                        // Nothing to dispose!
                    }
                }
            }
            else
            {
                try
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    // Main window loading
                    smoothingStrength = int.Parse(config.AppSettings.Settings["Strength"].Value);
                    smoothingInterpolation = int.Parse(config.AppSettings.Settings["Interpolation"].Value);
                    manualInterpolation = bool.Parse(config.AppSettings.Settings["Manual Interpolation"].Value);
                    smoothOnDraw = bool.Parse(config.AppSettings.Settings["Smooth On Draw"].Value);
                    stayOnTop = bool.Parse(config.AppSettings.Settings["Stay On Top"].Value);
                    disableAutoDetection = bool.Parse(config.AppSettings.Settings["Disable Auto Detection"].Value);
                    mainForm.tabletMode = bool.Parse(config.AppSettings.Settings["Tablet Mode"].Value);
                    mainForm.checkBox_tabletMode.Enabled = disableAutoDetection;
                    mainForm.checkBox_tabletMode.Checked = mainForm.tabletMode;
                    mainForm.checkBox_smoothOnDraw.Checked = smoothOnDraw;
                    if (manualInterpolation)
                    {
                        mainForm.checkBox_manualInterpolation.Checked = true;
                        mainForm.trackBar_smoothingInterpolation.Enabled = true;
                        mainForm.textBox_smoothingInterpolation.Enabled = true;
                    }
                    mainForm.textBox_smoothingInterpolation.Text = smoothingInterpolation.ToString();
                    mainForm.textBox_smoothingStrength.Text = smoothingStrength.ToString();
                    if (stayOnTop)
                    {
                        mainForm.checkBox_stayOnTop.Checked = true;
                        mainForm.TopMost = true;
                        overlayForm.TopMost = true;
                    }

                    // Cursor and overlay loading
                    overlayForm.cursorType = (Overlay.CursorType)Enum.Parse(typeof(Overlay.CursorType), config.AppSettings.Settings["Cursor Graphic"].Value);
                    overlayForm.cursorColor = ColorTranslator.FromHtml(config.AppSettings.Settings["Main Color"].Value);
                    overlayForm.cursorFillColor = ColorTranslator.FromHtml(config.AppSettings.Settings["Fill Color"].Value);
                    overlayScreen = int.Parse(config.AppSettings.Settings["Overlay Screen"].Value);
                    disableOverlay = bool.Parse(config.AppSettings.Settings["Disable Overlay"].Value);
                    allScreens = bool.Parse(config.AppSettings.Settings["All Screens"].Value);
                    manualOverlayOverride = bool.Parse(config.AppSettings.Settings["Manual Overlay Override"].Value);
                    RectangleConverter r = new RectangleConverter();
                    overrideBounds = (Rectangle)r.ConvertFromString(config.AppSettings.Settings["Override Bounds"].Value);
                    if (disableOverlay) overlayForm.Hide();
                    overlayForm.Bounds = Screen.AllScreens[overlayScreen].Bounds;
                    if (allScreens) overlayForm.Bounds = new Rectangle(0, 0, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
                    if (manualOverlayOverride) overlayForm.Bounds = overrideBounds;
                    mainForm.button_colorDialog.BackColor = overlayForm.cursorColor;

                    // ...and everything else
                    disableCatchUp = bool.Parse(config.AppSettings.Settings["Disable Catch Up"].Value);
                    snapToCursor = bool.Parse(config.AppSettings.Settings["Snap To Cursor"].Value);
                    tolerance = int.Parse(config.AppSettings.Settings["Tolerance"].Value);
                    tabletOffsetOverride = bool.Parse(config.AppSettings.Settings["Tablet Offset Override"].Value);
                    PointConverter p = new PointConverter();
                    tabletOffset = (Point)p.ConvertFromString(config.AppSettings.Settings["Tablet Offset"].Value);
                    KeysConverter c = new KeysConverter();
                    Keys k;
                    Hotkey.KeyModifiers m;
                    hotkeys[0] = config.AppSettings.Settings["Hotkey 1"].Value;
                    if (hotkeys[0] != "None")
                    {
                        k = (Keys)c.ConvertFromString(config.AppSettings.Settings["Hotkey 1"].Value);
                        m = Hotkey.GetModifiers(k, out k);
                        if (k != Keys.None)
                        {
                            mainForm.RegisterHotkey(mainForm.Handle, 0, m, k);
                        }
                    }
                    hotkeys[1] = config.AppSettings.Settings["Hotkey 2"].Value;
                    if (hotkeys[1] != "None")
                    {
                        k = (Keys)c.ConvertFromString(config.AppSettings.Settings["Hotkey 2"].Value);
                        m = Hotkey.GetModifiers(k, out k);
                        if (k != Keys.None)
                        {
                            mainForm.RegisterHotkey(mainForm.Handle, 1, m, k);
                        }
                    }
                    hotkeys[2] = config.AppSettings.Settings["Hotkey 3"].Value;
                    if (hotkeys[2] != "None")
                    {
                        k = (Keys)c.ConvertFromString(config.AppSettings.Settings["Hotkey 3"].Value);
                        m = Hotkey.GetModifiers(k, out k);
                        if (k != Keys.None)
                        {
                            mainForm.RegisterHotkey(mainForm.Handle, 2, m, k);
                        }
                    }
                    hotkeys[3] = config.AppSettings.Settings["Hotkey 4"].Value;
                    if (hotkeys[3] != "None")
                    {
                        k = (Keys)c.ConvertFromString(config.AppSettings.Settings["Hotkey 4"].Value);
                        m = Hotkey.GetModifiers(k, out k);
                        if (k != Keys.None)
                        {
                            mainForm.RegisterHotkey(mainForm.Handle, 3, m, k);
                        }
                    }
                }
                catch
                {
                    // Quietly fail loading bad configs or no configs
                }
            }
        }

        public void SaveConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("Strength");
            config.AppSettings.Settings.Add("Strength", smoothingStrength.ToString());
            config.AppSettings.Settings.Remove("Interpolation");
            config.AppSettings.Settings.Add("Interpolation", smoothingInterpolation.ToString());
            config.AppSettings.Settings.Remove("Manual Interpolation");
            config.AppSettings.Settings.Add("Manual Interpolation", manualInterpolation.ToString());
            config.AppSettings.Settings.Remove("Smooth On Draw");
            config.AppSettings.Settings.Add("Smooth On Draw", smoothOnDraw.ToString());
            config.AppSettings.Settings.Remove("Stay On Top");
            config.AppSettings.Settings.Add("Stay On Top", stayOnTop.ToString());
            config.AppSettings.Settings.Remove("Tablet Mode");
            config.AppSettings.Settings.Add("Tablet Mode", mainForm.tabletMode.ToString());
            config.AppSettings.Settings.Remove("Overlay Screen");
            config.AppSettings.Settings.Add("Overlay Screen", overlayScreen.ToString());
            config.AppSettings.Settings.Remove("Disable Overlay");
            config.AppSettings.Settings.Add("Disable Overlay", disableOverlay.ToString());
            config.AppSettings.Settings.Remove("All Screens");
            config.AppSettings.Settings.Add("All Screens", allScreens.ToString());
            config.AppSettings.Settings.Remove("Manual Overlay Override");
            config.AppSettings.Settings.Add("Manual Overlay Override", manualOverlayOverride.ToString());
            config.AppSettings.Settings.Remove("Override Bounds");
            RectangleConverter r = new RectangleConverter();
            config.AppSettings.Settings.Add("Override Bounds", r.ConvertToString(overrideBounds));
            config.AppSettings.Settings.Remove("Disable Catch Up");
            config.AppSettings.Settings.Add("Disable Catch Up", disableCatchUp.ToString());
            config.AppSettings.Settings.Remove("Snap to Cursor");
            config.AppSettings.Settings.Add("Snap to Cursor", snapToCursor.ToString());
            config.AppSettings.Settings.Remove("Cursor Graphic");
            config.AppSettings.Settings.Add("Cursor Graphic", overlayForm.cursorType.ToString());
            config.AppSettings.Settings.Remove("Main Color");
            config.AppSettings.Settings.Add("Main Color", ColorTranslator.ToHtml(overlayForm.cursorColor));
            config.AppSettings.Settings.Remove("Fill Color");
            config.AppSettings.Settings.Add("Fill Color", ColorTranslator.ToHtml(overlayForm.cursorFillColor));
            config.AppSettings.Settings.Remove("Disable Auto Detection");
            config.AppSettings.Settings.Add("Disable Auto Detection", disableAutoDetection.ToString());
            config.AppSettings.Settings.Remove("Tolerance");
            config.AppSettings.Settings.Add("Tolerance", tolerance.ToString());
            config.AppSettings.Settings.Remove("Tablet Offset Override");
            config.AppSettings.Settings.Add("Tablet Offset Override", tabletOffsetOverride.ToString());
            config.AppSettings.Settings.Remove("Tablet Offset");
            PointConverter p = new PointConverter();
            config.AppSettings.Settings.Add("Tablet Offset", p.ConvertToString(tabletOffset));
            config.AppSettings.Settings.Remove("Hotkey 1");
            config.AppSettings.Settings.Add("Hotkey 1", hotkeys[0].ToString());
            config.AppSettings.Settings.Remove("Hotkey 2");
            config.AppSettings.Settings.Add("Hotkey 2", hotkeys[1].ToString());
            config.AppSettings.Settings.Remove("Hotkey 3");
            config.AppSettings.Settings.Add("Hotkey 3", hotkeys[2].ToString());
            config.AppSettings.Settings.Remove("Hotkey 4");
            config.AppSettings.Settings.Add("Hotkey 4", hotkeys[3].ToString());
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
