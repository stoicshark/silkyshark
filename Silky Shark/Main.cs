using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Timers;
using System.Configuration;

namespace Silky_Shark
{
    public partial class Main : Form
    {
        Settings settings;
        Overlay overlay = new Overlay();
        List<Point> linePoints = new List<Point>();
        List<Point> smoothPoints = new List<Point>();
        System.Timers.Timer lineSmoothingTimer = new System.Timers.Timer();
        System.Timers.Timer lineProcessingTimer = new System.Timers.Timer();
        int virtualWidth = GetSystemMetrics(78);
        int virtualHeight = GetSystemMetrics(79);
        int virtualLeft = GetSystemMetrics(76);
        int virtualTop = GetSystemMetrics(77);
        public bool smoothingOn = false;
        public bool isDrawing = false;
        public bool mouseMoving = false;
        public bool tabletMode = false;
        public Hotkey[] hotKeyHandling = new Hotkey[4];
        Point position = new Point(0, 0);
        Point lastPosition = new Point(0, 0);

        // (Most) Settings
        public Point tabletOffset = new Point(0,0);
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

        public Main()
        {
            InitializeComponent();

            // Overlay setup
            overlay.Show();
            overlay.TopMost = true;
            overlay.Bounds = Screen.AllScreens[overlayScreen].Bounds;
            button_colorDialog.BackColor = overlay.cursorColor;

            // Attempt to load the config file, if not load/keep default settings
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", "Silky Shark.config");
            LoadConfig();

            // Low level mouse hook (MouseHook.cs)
            MouseHook.Start();
            MouseHook.MouseDownHooked += new EventHandler(MouseDownHandler);
            MouseHook.MouseUpHooked += new EventHandler(MouseUpHandler);
            MouseHook.MouseMoveHooked += new EventHandler(MouseMoveHandler);

            // Mouse smoothing updater
            lineSmoothingTimer.Elapsed += new ElapsedEventHandler(LineSmoothingUpdate);
            lineSmoothingTimer.Interval = 5;

            // Line processing updater
            lineProcessingTimer.Elapsed += new ElapsedEventHandler(LineProcessingUpdate);
            lineProcessingTimer.Interval = smoothingStrength;

            // Register a raw input listener
            int size = Marshal.SizeOf(typeof(RawInputDevice));
            RawInputDevice[] devices = new RawInputDevice[1];
            devices[0].UsagePage = 1;
            devices[0].Usage = 2;
            devices[0].Flags = 0x00000100;
            devices[0].Target = Handle;
            RegisterRawInputDevices(devices, 1, size);
        }

        // (Very ugly) Configuration file handling
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
                checkBox_smoothOnDraw.Checked = false;
                checkBox_stayOnTop.Checked = false;
                checkBox_tabletMode.Checked = false;
                checkBox_tabletMode.Enabled = false;
                checkBox_manualInterpolation.Checked = false;
                trackBar_smoothingInterpolation.Enabled = false;
                textBox_smoothingInterpolation.Enabled = false;
                textBox_smoothingInterpolation.Text = smoothingInterpolation.ToString();
                textBox_smoothingStrength.Text = smoothingStrength.ToString();
                checkBox_smoothOnDraw.Checked = false;
                TopMost = false;

                // Cursor and overlay resetting
                overlay.cursorColor = Color.FromArgb(128, 128, 128);
                overlay.cursorFillColor = Color.FromArgb(255, 255, 254);
                overlay.cursorType = Overlay.CursorType.Bullseye;
                overlay.Show();
                overlay.Bounds = Screen.PrimaryScreen.Bounds;
                button_colorDialog.BackColor = overlay.cursorColor;

                // Hotkey resetting
                try
                {
                    hotKeyHandling[0].Dispose();
                }
                catch
                {
                    // Nothing to dispose!
                }
                try
                {
                    hotKeyHandling[1].Dispose();
                }
                catch
                {
                    // Nothing to dispose!
                }
                try
                {
                    hotKeyHandling[2].Dispose();
                }
                catch
                {
                    // Nothing to dispose!
                }
                try
                {
                    hotKeyHandling[3].Dispose();
                }
                catch
                {
                    // Nothing to dispose!
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
                    tabletMode = bool.Parse(config.AppSettings.Settings["Tablet Mode"].Value);
                    checkBox_tabletMode.Enabled = disableAutoDetection;
                    checkBox_tabletMode.Checked = tabletMode;
                    checkBox_smoothOnDraw.Checked = smoothOnDraw;
                    if (manualInterpolation)
                    {
                        checkBox_manualInterpolation.Checked = true;
                        trackBar_smoothingInterpolation.Enabled = true;
                        textBox_smoothingInterpolation.Enabled = true;
                    }
                    textBox_smoothingInterpolation.Text = smoothingInterpolation.ToString();
                    textBox_smoothingStrength.Text = smoothingStrength.ToString();
                    if (stayOnTop)
                    {
                        checkBox_stayOnTop.Checked = true;
                        TopMost = true;
                        overlay.TopMost = true;
                    }

                    // Cursor and overlay loading
                    overlay.cursorType = (Overlay.CursorType)Enum.Parse(typeof(Overlay.CursorType), config.AppSettings.Settings["Cursor Graphic"].Value);
                    overlay.cursorColor = ColorTranslator.FromHtml(config.AppSettings.Settings["Main Color"].Value);
                    overlay.cursorFillColor = ColorTranslator.FromHtml(config.AppSettings.Settings["Fill Color"].Value);
                    overlayScreen = int.Parse(config.AppSettings.Settings["Overlay Screen"].Value);
                    disableOverlay = bool.Parse(config.AppSettings.Settings["Disable Overlay"].Value);
                    allScreens = bool.Parse(config.AppSettings.Settings["All Screens"].Value);
                    manualOverlayOverride = bool.Parse(config.AppSettings.Settings["Manual Overlay Override"].Value);
                    RectangleConverter r = new RectangleConverter();
                    overrideBounds = (Rectangle)r.ConvertFromString(config.AppSettings.Settings["Override Bounds"].Value);
                    if (disableOverlay) overlay.Hide();
                    overlay.Bounds = Screen.AllScreens[overlayScreen].Bounds;
                    if (allScreens) overlay.Bounds = new Rectangle(0, 0, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
                    if (manualOverlayOverride) overlay.Bounds = overrideBounds;
                    button_colorDialog.BackColor = overlay.cursorColor;

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
                            RegisterHotkey(Handle, 100, m, k);
                        }
                    }
                    hotkeys[1] = config.AppSettings.Settings["Hotkey 2"].Value;
                    if (hotkeys[1] != "None")
                    {
                        k = (Keys)c.ConvertFromString(config.AppSettings.Settings["Hotkey 2"].Value);
                        m = Hotkey.GetModifiers(k, out k);
                        if (k != Keys.None)
                        {
                            RegisterHotkey(Handle, 101, m, k);
                        }
                    }
                    hotkeys[2] = config.AppSettings.Settings["Hotkey 3"].Value;
                    if (hotkeys[2] != "None")
                    {
                        k = (Keys)c.ConvertFromString(config.AppSettings.Settings["Hotkey 3"].Value);
                        m = Hotkey.GetModifiers(k, out k);
                        if (k != Keys.None)
                        {
                            RegisterHotkey(Handle, 102, m, k);
                        }
                    }
                    hotkeys[3] = config.AppSettings.Settings["Hotkey 4"].Value;
                    if (hotkeys[3] != "None")
                    {
                        k = (Keys)c.ConvertFromString(config.AppSettings.Settings["Hotkey 4"].Value);
                        m = Hotkey.GetModifiers(k, out k);
                        if (k != Keys.None)
                        {
                            RegisterHotkey(Handle, 103, m, k);
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
            config.AppSettings.Settings.Add("Tablet Mode", tabletMode.ToString());
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
            config.AppSettings.Settings.Add("Cursor Graphic", overlay.cursorType.ToString());
            config.AppSettings.Settings.Remove("Main Color");
            config.AppSettings.Settings.Add("Main Color", ColorTranslator.ToHtml(overlay.cursorColor));
            config.AppSettings.Settings.Remove("Fill Color");
            config.AppSettings.Settings.Add("Fill Color", ColorTranslator.ToHtml(overlay.cursorFillColor));
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

        // Hotkey handling
        public void RegisterHotkey(IntPtr handle, int id, Hotkey.KeyModifiers modifiers, Keys key)
        {
            int i = id - 100;
            try
            {
                hotKeyHandling[i].Dispose();
            }
            catch
            {
                // No hotkey to dispose?
            }

            try
            { 
                hotKeyHandling[i] = new Hotkey(Handle, id, modifiers, key);
                switch (i)
                {
                    case 0:
                        hotKeyHandling[0].HotKeyPressed += new EventHandler(Hotkey_SmoothOnOff);
                        break;
                    case 1:
                        hotKeyHandling[1].HotKeyPressed += new EventHandler(Hotkey_OverlayOnOff);
                        break;
                    case 2:
                        hotKeyHandling[2].HotKeyPressed += new EventHandler(Hotkey_ToggleDisplay);
                        break;
                    case 3:
                        hotKeyHandling[3].HotKeyPressed += new EventHandler(Hotkey_TabletMode);
                        break;
                }
            }
            catch
            {
                // Hotkey registration failed
            }
        }

        private void Hotkey_TabletMode(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Settings>().Count() != 1)
            {
                if (!smoothingOn && disableAutoDetection) checkBox_tabletMode.Checked = !checkBox_tabletMode.Checked;
            }
        }

        private void Hotkey_ToggleDisplay(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Settings>().Count() != 1)
            {
                button_toggleDisplay.PerformClick();
            }
        }

        private void Hotkey_OverlayOnOff(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Settings>().Count() != 1)
            {
                if (!disableOverlay)
                {
                    overlay.Hide();
                    disableOverlay = true;
                }
                else
                {
                    overlay.Show();
                    disableOverlay = false;
                }
            }
        }

        private void Hotkey_SmoothOnOff(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Settings>().Count() != 1)
            {
                button_smoothOnOff.PerformClick();
            }
        }
        
        // Reading global raw input
        private void VirtualCursorUpdate(ref Message m)
        {
            int RidInput = 0x10000003;
            int headerSize = Marshal.SizeOf(typeof(RawInputHeader));
            int size = Marshal.SizeOf(typeof(RawInput));
            RawInput input;
            GetRawInputData(m.LParam, RidInput, out input, ref size, headerSize);
            RawMouse mouse = input.Mouse;

            if (!disableAutoDetection)
            {
                if (mouse.LastX > tolerance || mouse.LastY > tolerance)
                {
                    checkBox_tabletMode.Checked = true;
                    tabletMode = true;
                }
                else
                {
                    checkBox_tabletMode.Checked = false;
                    tabletMode = false;
                }
            }

            if (isDrawing)
            {
                if (tabletMode)
                {
                    Point offset = new Point(0, 0);
                    if (tabletOffsetOverride) offset = tabletOffset;
                    int tabletX = mouse.LastX * virtualWidth / 65536;
                    int tabletY = mouse.LastY * virtualHeight / 65536;
                    Point p = new Point(tabletX + offset.X + virtualLeft, tabletY + offset.Y + virtualTop);
                    position = p;
                    overlay.cursorPos = p;
                    overlay.Invalidate();
                }
                else
                {
                    Point p = new Point(position.X + mouse.LastX, position.Y + mouse.LastY);
                    //if (p.X < virtualLeft) p.X = virtualLeft;
                    //if (p.X > virtualWidth) p.X = virtualWidth;
                    //if (p.Y < virtualTop) p.Y = virtualTop;
                    //if (p.Y > virtualHeight) p.Y = virtualHeight;
                    position = p;
                    overlay.cursorPos = p;
                    overlay.Invalidate();
                }
            }
        }

        // Line processing (interlopation)
        private void LineProcessingUpdate(object sender, ElapsedEventArgs e)
        {
            try
            {
                // B-Spline smoothing
                if (linePoints.Count > 3)
                {
                    int i;
                    int splineX;
                    int splineY;
                    double[] a = new double[5];
                    double[] b = new double[5];
                    Point p1 = linePoints[0];
                    Point p2 = linePoints[1];
                    Point p3 = linePoints[2];
                    Point p4 = linePoints[3];

                    a[0] = (-p1.X + 3 * p2.X - 3 * p3.X + p4.X) / 6.0;
                    a[1] = (3 * p1.X - 6 * p2.X + 3 * p3.X) / 6.0;
                    a[2] = (-3 * p1.X + 3 * p3.X) / 6.0;
                    a[3] = (p1.X + 4 * p2.X + p3.X) / 6.0;
                    b[0] = (-p1.Y + 3 * p2.Y - 3 * p3.Y + p4.Y) / 6.0;
                    b[1] = (3 * p1.Y - 6 * p2.Y + 3 * p3.Y) / 6.0;
                    b[2] = (-3 * p1.Y + 3 * p3.Y) / 6.0;
                    b[3] = (p1.Y + 4 * p2.Y + p3.Y) / 6.0;

                    smoothPoints.Add(new Point((int)a[3], (int)b[3]));

                    for (i = 1; i <= smoothingInterpolation - 1; i++)
                    {
                        float t = Convert.ToSingle(i) / Convert.ToSingle(smoothingInterpolation);
                        splineX = (int)((a[2] + t * (a[1] + t * a[0])) * t + a[3]);
                        splineY = (int)((b[2] + t * (b[1] + t * b[0])) * t + b[3]);
                        if (smoothPoints.Last<Point>() != new Point(splineX, splineY))
                        {
                            smoothPoints.Add(new Point(splineX, splineY));
                        }
                    }
                    linePoints.RemoveAt(0);
                }
                else if (MouseHook.GetCursorPosition() != position && isDrawing)
                {
                    if (disableCatchUp)
                    {
                        if (mouseMoving)
                        {
                            linePoints.Add(position);
                        }
                    }
                    else
                    {
                        linePoints.Add(position);
                    }
                }
            }
            catch
            {
                // Fail processing gracefully
            }
        }

        // Line smoothing
        private void LineSmoothingUpdate(object sender, ElapsedEventArgs e)
        {
            Point guidePos = overlay.cursorPos;

            if (!isDrawing)
            {
                lineProcessingTimer.Stop();
                lineSmoothingTimer.Stop();
                if (!snapToCursor) MouseHook.SetCursorPos(guidePos.X, guidePos.Y);
                MouseHook.moveEnabled = true;
            }

            if (lastPosition == guidePos)
            {
                mouseMoving = false;
            }
            else
            {
                mouseMoving = true;
            }
            lastPosition = guidePos;

            try
            {
                // Begin smoothing only if we have points to work with and if drawing
                if (smoothPoints.Count > 0 && isDrawing)
                {
                    if (disableCatchUp)
                    {
                        if (mouseMoving)
                        {
                            MouseHook.SetCursorPos(smoothPoints[0].X, smoothPoints[0].Y);
                            smoothPoints.RemoveAt(0);
                        }
                        lastPosition = guidePos;
                    }
                    else
                    {
                        MouseHook.SetCursorPos(smoothPoints[0].X, smoothPoints[0].Y);
                        smoothPoints.RemoveAt(0);
                    }
                }
            }
            catch
            {
                // Fail smoothing gracefully
            }
        }

        // Mouse event handling
        private void MouseDownHandler(object sender, EventArgs e)
        {
            if (smoothingOn)
            {
                if (smoothOnDraw && !isDrawing)
                {
                    MouseHook.moveEnabled = false;
                    linePoints.Clear();
                    smoothPoints.Clear();
                    Point p = MouseHook.GetCursorPosition();
                    smoothPoints.Add(p);
                    linePoints.Add(p);
                    linePoints.Add(p);
                    linePoints.Add(p);
                    position = p;
                    isDrawing = true;
                    lineProcessingTimer.Start();
                    lineSmoothingTimer.Start();
                }
            }
        }

        private void MouseUpHandler(object sender, EventArgs e)
        {
            if (smoothingOn)
            {
                if (smoothOnDraw && isDrawing)
                {
                    isDrawing = false;
                    linePoints.Clear();
                    smoothPoints.Clear();
                    if (!snapToCursor)
                    {
                        Point guidePos = overlay.cursorPos;
                        MouseHook.SetCursorPos(guidePos.X, guidePos.Y);
                    } else
                    {
                        overlay.cursorPos = MouseHook.GetCursorPosition();
                    }
                }
            }
        }

        private void MouseMoveHandler(object sender, EventArgs e)
        {
            if (!smoothingOn)
            {
                overlay.cursorPos = MouseHook.GetCursorPosition();
                overlay.Invalidate();
            }

            if (smoothOnDraw && !isDrawing && MouseHook.moveEnabled)
            {
                overlay.cursorPos = MouseHook.GetCursorPosition();
                overlay.Invalidate();
            }
        }
        
        protected override void WndProc(ref Message m)
        {
            const int WM_INPUT = 0xFF;
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        m.Result = (IntPtr)0x2;
                    }
                    break;
            }
            if (m.Msg == WM_INPUT && smoothingOn)
            {
                this.VirtualCursorUpdate(ref m);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        // Interface handling
        private void button_SmoothOnOff_Click(object sender, EventArgs e)
        {
            if (smoothingOn)
            {
                // Off
                button_smoothOnOff.BackColor = Color.Gainsboro;
                MouseHook.moveEnabled = true;
                smoothingOn = false;
                isDrawing = false;
                lineSmoothingTimer.Stop();
                lineProcessingTimer.Stop();
                try
                {
                    settings.checkBox_disableAutoDetection.Enabled = true;
                }
                catch
                {
                    // Fail gracefully
                }
                if (disableAutoDetection)
                {
                    checkBox_tabletMode.Enabled = true;
                }
            }
            else
            {
                // On
                button_smoothOnOff.BackColor = Color.Azure;
                linePoints.Clear();
                smoothPoints.Clear();
                position = MouseHook.GetCursorPosition();
                smoothPoints.Add(position);
                if (smoothOnDraw)
                {
                    MouseHook.moveEnabled = true;
                    isDrawing = false;
                }
                else
                {
                    MouseHook.moveEnabled = false;
                    isDrawing = true;
                    lineProcessingTimer.Start();
                    lineSmoothingTimer.Start();
                }
                smoothingOn = true;
                try
                {
                    settings.checkBox_disableAutoDetection.Enabled = false;
                }
                catch
                {
                    // Fail gracefully
                }
                if (disableAutoDetection)
                {
                    checkBox_tabletMode.Enabled = false;
                }
            }
        }

        private void trackBar_smoothStrength_Scroll(object sender, EventArgs e)
        {
            smoothingStrength = trackBar_smoothingStrength.Value;
            lineProcessingTimer.Interval = smoothingStrength;
            textBox_smoothingStrength.Text = smoothingStrength.ToString();
            if (!manualInterpolation)
            {
                smoothingInterpolation = (int)Math.Round(smoothingStrength * 0.15);
                trackBar_smoothingInterpolation.Value = smoothingInterpolation;
                textBox_smoothingInterpolation.Text = smoothingInterpolation.ToString();
            }
        }

        private void textBox_smoothingStrength_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(textBox_smoothingStrength.Text) < 1)
                {
                    smoothingStrength = 1;
                }
                else if (int.Parse(textBox_smoothingStrength.Text) > 100)
                {
                    smoothingStrength = 100;
                }
                else
                {
                    smoothingStrength = int.Parse(textBox_smoothingStrength.Text);
                }
            }
            catch
            {
                smoothingStrength = 1;
            }
            lineProcessingTimer.Interval = smoothingStrength;
            trackBar_smoothingStrength.Value = smoothingStrength;
            textBox_smoothingStrength.Text = smoothingStrength.ToString();
            if (!manualInterpolation)
            {
                smoothingInterpolation = (int)Math.Round(smoothingStrength * 0.15);
                trackBar_smoothingInterpolation.Value = smoothingInterpolation;
                textBox_smoothingInterpolation.Text = smoothingInterpolation.ToString();
            }
        }

        private void trackBar_smoothingInterpolation_Scroll(object sender, EventArgs e)
        {
            smoothingInterpolation = trackBar_smoothingInterpolation.Value;
            trackBar_smoothingInterpolation.Value = smoothingInterpolation;
            textBox_smoothingInterpolation.Text = smoothingInterpolation.ToString();
        }

        private void textBox_smoothingInterpolation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(textBox_smoothingInterpolation.Text) < 0)
                {
                    smoothingInterpolation = 0;
                }
                else if (int.Parse(textBox_smoothingInterpolation.Text) > 20)
                {
                    smoothingInterpolation = 20;
                }
                else
                {
                    smoothingInterpolation = int.Parse(textBox_smoothingInterpolation.Text);
                }
            }
            catch
            {
                smoothingInterpolation = 0;
            }
            trackBar_smoothingInterpolation.Value = smoothingInterpolation;
            textBox_smoothingInterpolation.Text = smoothingInterpolation.ToString();
        }

        private void checkBox_manualInterpolation_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_manualInterpolation.Checked)
            {
                manualInterpolation = true;
                trackBar_smoothingInterpolation.Enabled = true;
                textBox_smoothingInterpolation.Enabled = true;
            }
            else
            {
                manualInterpolation = false;
                trackBar_smoothingInterpolation.Enabled = false;
                textBox_smoothingInterpolation.Enabled = false;
                smoothingInterpolation = (int)Math.Round(smoothingStrength * 0.15);
                trackBar_smoothingInterpolation.Value = smoothingInterpolation;
                textBox_smoothingInterpolation.Text = smoothingInterpolation.ToString();
            }
        }

        private void checkBox_stayOnTop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_stayOnTop.Checked)
            {
                TopMost = true;
                overlay.TopMost = true;
                stayOnTop = true;
            }
            else
            {
                TopMost = false;
                stayOnTop = false;
            }
        }

        private void checkBox_smoothOnDraw_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_smoothOnDraw.Checked)
            {
                if (smoothingOn)
                {
                    isDrawing = false;
                    MouseHook.moveEnabled = true;
                    linePoints.Clear();
                    smoothPoints.Clear();
                    lineProcessingTimer.Stop();
                    lineSmoothingTimer.Stop();
                }
                smoothOnDraw = true;
            }
            else
            {
                if (smoothingOn)
                {
                    isDrawing = true;
                    MouseHook.moveEnabled = false;
                    lineProcessingTimer.Start();
                    lineSmoothingTimer.Start();
                }
                smoothOnDraw = false;
            }
        }

        private void checkBox_tabletMode_CheckedChanged(object sender, EventArgs e)
        {
            tabletMode = checkBox_tabletMode.Checked;
        }
        
        private void button_toggleScreen_Click(object sender, EventArgs e)
        {
            overlayScreen++;
            if (overlayScreen > (Screen.AllScreens.Count() - 1))
            {
                overlayScreen = 0;
            }
            overlay.Bounds = Screen.AllScreens[overlayScreen].Bounds;
        }

        private void button_colorDialog_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (colorDialog.Color == Color.White) colorDialog.Color = Color.FromArgb(255,255,254);
                button_colorDialog.BackColor = colorDialog.Color;
                overlay.cursorColor = colorDialog.Color;
                overlay.Invalidate();
                try
                {
                    settings.panel_cursorPanel.Invalidate();
                    settings.button_mainColor.BackColor = colorDialog.Color;
                }
                catch
                {
                    // Fail setting color in settings gracefully
                }
            }
        }

        // Menu handling
        private void toolStrip_Settings_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Settings>().Count() != 1)
            {
                settings = new Settings(this, overlay);
                settings.Owner = this;
                settings.MinimizeBox = false;
                settings.MaximizeBox = false;
                ToolStripMenuItem_restoreDefaults.Enabled = false;
                ToolStripMenuItem_saveConfig.Enabled = false;
                settings.Show();
            }
        }

        private void ToolStripMenuItem_saveConfig_Click(object sender, EventArgs e)
        {
            SaveConfig();
            MessageBox.Show("Configuration settings saved to: Silky Shark.config", "Silky Shark", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToolStripMenuItem_restoreDefaults_Click(object sender, EventArgs e)
        {
            LoadConfig(true);
            MessageBox.Show("Default settings restored.", "Silky Shark", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToolStripMenuItem_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolStripMenuItem_help_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Help>().Count() != 1)
            {
                Help help = new Help();
                help.Owner = this;
                help.MinimizeBox = false;
                help.MaximizeBox = false;
                help.Show();
            }
        }

        private void ToolStripMenuItem_about_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<About>().Count() != 1)
            {
                About about = new About();
                about.Owner = this;
                about.MinimizeBox = false;
                about.MaximizeBox = false;
                about.Show();
            }
        }

        // Raw input hook
        private struct RawInputDevice
        {
            public short UsagePage;
            public short Usage;
            public int Flags;
            public IntPtr Target;
        }

        private struct RawInputHeader
        {
            public int Type;
            public int Size;
            public IntPtr Device;
            public IntPtr WParam;
        }

        private struct RawInput
        {
            public RawInputHeader Header;
            public RawMouse Mouse;
        }

        private struct RawMouse
        {
            public short Flags;
            public short ButtonFlags;
            public short ButtonData;
            public int RawButtons;
            public int LastX;
            public int LastY;
            public int Extra;
        }  

        //Dll importing
        [DllImport("user32.dll")]
        private static extern int RegisterRawInputDevices(RawInputDevice[] devices, int number, int size);

        [DllImport("user32.dll")]
        private static extern int GetRawInputData(IntPtr rawInput, int command, out RawInput data, ref int size, int headerSize);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);
    } 
}
