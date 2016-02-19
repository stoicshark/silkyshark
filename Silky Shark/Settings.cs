using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Silky_Shark
{
    public partial class Settings : Form
    {
        Main mainForm;
        Config config;
        Overlay overlayForm;

        public Settings(Main mF, Config cG, Overlay oF)
        {
            InitializeComponent();
            panel_cursorPanel.Paint += new PaintEventHandler(CursorPreviewPaint);
            mainForm = mF;
            config = cG;
            overlayForm = oF;

            // Overlay tab setup
            if (config.disableOverlay) checkBox_disableOverlay.Checked = true;
            if (config.allScreens) checkBox_allScreens.Checked = true;
            if (config.manualOverlayOverride)
            {
                checkBox_manualOverlayOverride.Checked = true;
            }
            else
            {
                textBox_overlayPositionX.Enabled = false;
                textBox_overlayPositionY.Enabled = false;
                textBox_overlaySizeX.Enabled = false;
                textBox_overlaySizeY.Enabled = false;
            }
            Rectangle ob = config.overrideBounds;
            textBox_overlayPositionX.Text = ob.Location.X.ToString();
            textBox_overlayPositionY.Text = ob.Location.Y.ToString();
            textBox_overlaySizeX.Text = ob.Width.ToString();
            textBox_overlaySizeY.Text = ob.Height.ToString();

            // Cursor tab
            if (config.disableCatchUp) checkBox_disableCatchUp.Checked = true;
            if (config.snapToCursor) checkBox_snapToCursor.Checked = true;
            switch (overlayForm.cursorType)
            {
                case Overlay.CursorType.Bullseye:
                    radioButton_bullseye.Checked = true;
                    break;
                case Overlay.CursorType.Circle:
                    radioButton_circle.Checked = true;
                    break;
                case Overlay.CursorType.Crosshair:
                    radioButton_crosshair.Checked = true;
                    break;
                case Overlay.CursorType.Cursor:
                    radioButton_cursor.Checked = true;
                    break;
                case Overlay.CursorType.TinyCursor:
                    radioButton_tinyCursor.Checked = true;
                    break;
                case Overlay.CursorType.Pen:
                    radioButton_pen.Checked = true;
                    break;
            }
            button_mainColor.BackColor = overlayForm.cursorColor;
            button_fillColor.BackColor = overlayForm.cursorFillColor;

            // Tablet tab
            if (config.disableAutoDetection) checkBox_disableAutoDetection.Checked = true;
            if (mainForm.smoothingOn)
            {
                checkBox_disableAutoDetection.Enabled = false;
            }
            int t = config.tolerance;
            textBox_tolerance.Text = t.ToString();
            if (config.tabletOffsetOverride)
            {
                checkBox_tabletOffsetOverride.Checked = true;
            }
            else
            {
                textBox_tabletOffsetX.Enabled = false;
                textBox_tabletOffsetY.Enabled = false;
            }
            Point co = config.tabletOffset;
            textBox_tabletOffsetX.Text = co.X.ToString();
            textBox_tabletOffsetY.Text = co.Y.ToString();

            // Hotkey tab
            KeysConverter c = new KeysConverter();
            string mod;
            string[] ms;
            Keys k;
            Hotkey.KeyModifiers m;
            k = (Keys)c.ConvertFromString(config.hotkeys[0]);
            if (k.ToString() != "None")
            {
                m = Hotkey.GetModifiers(k, out k);
                mod = "";
                ms = m.ToString().Split(new[] { ", " }, StringSplitOptions.None);
                foreach (string mm in ms)
                {
                    mod += mm + "+";
                }
                textBox_hotkeySmoothOnOff.Text = mod + k;
            }
            k = (Keys)c.ConvertFromString(config.hotkeys[1]);
            if (k.ToString() != "None")
            {
                m = Hotkey.GetModifiers(k, out k);
                mod = "";
                ms = m.ToString().Split(new[] { ", " }, StringSplitOptions.None);
                foreach (string mm in ms)
                {
                    mod += mm + "+";
                }
                textBox_hotkeyOverlayOnOff.Text = mod + k;
            }
            k = (Keys)c.ConvertFromString(config.hotkeys[2]);
            if (k.ToString() != "None")
            {
                m = Hotkey.GetModifiers(k, out k);
                mod = "";
                ms = m.ToString().Split(new[] { ", " }, StringSplitOptions.None);
                foreach (string mm in ms)
                {
                    mod += mm + "+";
                }
                textBox_hotkeyToggleDisplay.Text = mod + k;
            }
            k = (Keys)c.ConvertFromString(config.hotkeys[3]);
            if (k.ToString() != "None")
            {
                m = Hotkey.GetModifiers(k, out k);
                mod = "";
                ms = m.ToString().Split(new[] { ", " }, StringSplitOptions.None);
                foreach (string mm in ms)
                {
                    mod += mm + "+";
                }
                textBox_hotkeyTabletMode.Text = mod + k;
            }
        }

        // Draw some cursor examples
        private void CursorPreviewPaint(object sender, PaintEventArgs e)
        {
            Color cursorColor = overlayForm.cursorColor;
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(cursorColor);
            SolidBrush brush = new SolidBrush(cursorColor);
            SolidBrush fillBrush = new SolidBrush(overlayForm.cursorFillColor);
            Point offset;

            // Bullseye
            offset = new Point(30, 12);
            graphics.DrawEllipse(pen, offset.X, offset.Y, 10, 10);
            graphics.FillRectangle(brush, offset.X + 5, offset.Y + 5, 1, 1);

            // Circle
            offset = new Point(100, 12);
            graphics.DrawEllipse(pen, offset.X, offset.Y, 10, 10);

            // Crosshair
            offset = new Point(170, 12);
            graphics.DrawLine(pen, offset.X + 5, offset.Y + 0, offset.X + 5, offset.Y + 10);
            graphics.DrawLine(pen, offset.X + 0, offset.Y + 5, offset.X + 10, offset.Y + 5);

            // Cursor
            offset = new Point(30, 34);
            Point[] cPoint =
            {
                new Point(offset.X + 0, offset.Y + 0),
                new Point(offset.X + 0, offset.Y + 16),
                new Point(offset.X + 1, offset.Y + 16),
                new Point(offset.X + 4, offset.Y + 13),
                new Point(offset.X + 7, offset.Y + 18),
                new Point(offset.X + 8, offset.Y + 18),
                new Point(offset.X + 9, offset.Y + 17),
                new Point(offset.X + 9, offset.Y + 16),
                new Point(offset.X + 7, offset.Y + 13),
                new Point(offset.X + 7, offset.Y + 12),
                new Point(offset.X + 11, offset.Y + 12),
                new Point(offset.X + 11, offset.Y + 11)
            };
            graphics.FillPolygon(fillBrush, cPoint);
            graphics.DrawPolygon(pen, cPoint);

            // Tiny Cursor
            offset = new Point(100, 39);
            Point[] tcPoint =
            {
                new Point(offset.X + 0, offset.Y + 0),
                new Point(offset.X + 0, offset.Y + 9),
                new Point(offset.X + 3, offset.Y + 6),
                new Point(offset.X + 6, offset.Y + 6)
            };
            graphics.FillPolygon(fillBrush, tcPoint);
            graphics.DrawPolygon(pen, tcPoint);

            // Pen
            offset = new Point(170, 38);
            Point[] pPoint =
            {
                new Point(offset.X + 10, offset.Y + 0),
                new Point(offset.X + 2, offset.Y + 8),
                new Point(offset.X + 0, offset.Y + 12),
                new Point(offset.X + 1, offset.Y + 11),
                new Point(offset.X + 4, offset.Y + 10),
                new Point(offset.X + 12, offset.Y + 2)
            };
            graphics.FillPolygon(fillBrush, pPoint);
            graphics.DrawPolygon(pen, pPoint);
        }

        // Overlay tab handling
        private void checkBox_disableOverlay_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_disableOverlay.Checked)
            {
                overlayForm.Hide();
                config.disableOverlay = true;
            }
            else
            {
                overlayForm.Show();
                config.disableOverlay = false;
            }
        }

        private void checkBox_allScreens_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_allScreens.Checked)
            {
                overlayForm.Bounds = new Rectangle(0, 0, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
                config.allScreens = true;
            }
            else
            {
                overlayForm.Bounds = Screen.AllScreens[config.overlayScreen].Bounds;
                config.allScreens = false;
            }
        }

        private void checkBox_manualOverlayOverride_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_manualOverlayOverride.Checked)
            {
                overlayForm.Bounds = config.overrideBounds;
                textBox_overlayPositionX.Enabled = true;
                textBox_overlayPositionY.Enabled = true;
                textBox_overlaySizeX.Enabled = true;
                textBox_overlaySizeY.Enabled = true;
                config.manualOverlayOverride = true;
            }
            else
            {
                if (checkBox_allScreens.Checked)
                {
                    overlayForm.Bounds = new Rectangle(0, 0, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
                    config.allScreens = true;
                }
                else
                {
                    overlayForm.Bounds = Screen.AllScreens[config.overlayScreen].Bounds;
                    config.allScreens = false;
                }
                textBox_overlayPositionX.Enabled = false;
                textBox_overlayPositionY.Enabled = false;
                textBox_overlaySizeX.Enabled = false;
                textBox_overlaySizeY.Enabled = false;
                config.manualOverlayOverride = false;
            }
        }

        private void textBox_overlaySizeX_TextChanged(object sender, EventArgs e)
        {
            if (config.manualOverlayOverride)
            {
                Rectangle b = config.overrideBounds;
                int i = int.TryParse(textBox_overlaySizeX.Text, out i) ? i : 0;
                b.Width = i;
                config.overrideBounds = b;
                overlayForm.Bounds = b;
                textBox_overlaySizeX.Text = b.Width.ToString();
            }
        }

        private void textBox_overlaySizeY_TextChanged(object sender, EventArgs e)
        {
            if (config.manualOverlayOverride)
            {
                Rectangle b = config.overrideBounds;
                int i = int.TryParse(textBox_overlaySizeY.Text, out i) ? i : 0;
                b.Height = i;
                config.overrideBounds = b;
                overlayForm.Bounds = b;
                textBox_overlaySizeY.Text = b.Height.ToString();
            }
        }

        private void textBox_overlayPositionX_TextChanged(object sender, EventArgs e)
        {
            if (config.manualOverlayOverride)
            {
                Rectangle b = config.overrideBounds;
                int i = int.TryParse(textBox_overlayPositionX.Text, out i) ? i : 0;
                b.X = i;
                config.overrideBounds = b;
                overlayForm.Bounds = b;
                textBox_overlayPositionX.Text = b.X.ToString();
            }
        }

        private void textBox_overlayPositionY_TextChanged(object sender, EventArgs e)
        {
            if (config.manualOverlayOverride)
            {
                Rectangle b = config.overrideBounds;
                int i = int.TryParse(textBox_overlayPositionY.Text, out i) ? i : 0;
                b.Y = i;
                config.overrideBounds = b;
                overlayForm.Bounds = b;
                textBox_overlayPositionY.Text = b.Y.ToString();
            }
        }
        

        // Cursor tab handling
        private void checkBox_disableCatchUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_disableCatchUp.Checked)
            {
                config.disableCatchUp = true;
            }
            else
            {
                config.disableCatchUp = false;
            }
        }

        private void checkBox_snapToCursor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_snapToCursor.Checked)
            {
                config.snapToCursor = true;
            }
            else
            {
                config.snapToCursor = false;
            }
        }

        private void radioButton_bullseye_CheckedChanged(object sender, EventArgs e)
        {
            overlayForm.cursorType = Overlay.CursorType.Bullseye;
            overlayForm.Invalidate();
        }

        private void radioButton_circle_CheckedChanged(object sender, EventArgs e)
        {
            overlayForm.cursorType = Overlay.CursorType.Circle;
            overlayForm.Invalidate();
        }

        private void radioButton_crosshair_CheckedChanged(object sender, EventArgs e)
        {
            overlayForm.cursorType = Overlay.CursorType.Crosshair;
            overlayForm.Invalidate();
        }

        private void radioButton_cursor_CheckedChanged(object sender, EventArgs e)
        {
            overlayForm.cursorType = Overlay.CursorType.Cursor;
            overlayForm.Invalidate();
        }

        private void radioButton_tinyCursor_CheckedChanged(object sender, EventArgs e)
        {
            overlayForm.cursorType = Overlay.CursorType.TinyCursor;
            overlayForm.Invalidate();
        }

        private void radioButton_pen_CheckedChanged(object sender, EventArgs e)
        {
            overlayForm.cursorType = Overlay.CursorType.Pen;
            overlayForm.Invalidate();
        }

        private void button_mainColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (colorDialog.Color == Color.White) colorDialog.Color = Color.FromArgb(255, 255, 254);
                button_mainColor.BackColor = colorDialog.Color;
                mainForm.button_colorDialog.BackColor = colorDialog.Color;
                overlayForm.cursorColor = colorDialog.Color;
                overlayForm.Invalidate();
                panel_cursorPanel.Invalidate();
            }
        }

        private void button_fillColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (colorDialog.Color == Color.White) colorDialog.Color = Color.FromArgb(255, 255, 254);
                button_fillColor.BackColor = colorDialog.Color;
                overlayForm.cursorFillColor = colorDialog.Color;
                overlayForm.Invalidate();
                panel_cursorPanel.Invalidate();
            }
        }

        // Tablet tab handling
        private void checkBox_disableAutoDetection_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_disableAutoDetection.Checked)
            {
                mainForm.checkBox_tabletMode.Enabled = true;
                config.disableAutoDetection = true;
            }
            else
            {
                mainForm.checkBox_tabletMode.Enabled = false;
                config.disableAutoDetection = false;
            }
        }

        private void textBox_tolerance_TextChanged(object sender, EventArgs e)
        {
            int i = int.TryParse(textBox_tolerance.Text, out i) ? i : 0;
            if (i > 0)
            {
                config.tolerance = i;
                textBox_tolerance.Text = i.ToString();
            }
            else
            {
                config.tolerance = 0;
                textBox_tolerance.Text = "0";
            }
        }

        private void checkBox_tabletOffsetOverride_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_tabletOffsetOverride.Checked)
            {
                textBox_tabletOffsetX.Enabled = true;
                textBox_tabletOffsetY.Enabled = true;
                config.tabletOffsetOverride = true;
            }
            else
            {
                textBox_tabletOffsetX.Enabled = false;
                textBox_tabletOffsetY.Enabled = false;
                config.tabletOffsetOverride = false;
            }
        }

        private void textBox_tabletOffsetX_TextChanged(object sender, EventArgs e)
        {
            Point p = config.tabletOffset;
            int i = int.TryParse(textBox_tabletOffsetX.Text, out i) ? i : 0;
            config.tabletOffset = new Point(i, p.Y);
            textBox_tabletOffsetX.Text = i.ToString();
        }

        private void textBox_tabletOffsetY_TextChanged(object sender, EventArgs e)
        {
            Point p = config.tabletOffset;
            int i = int.TryParse(textBox_tabletOffsetY.Text, out i) ? i : 0;
            config.tabletOffset = new Point(p.X, i);
            textBox_tabletOffsetY.Text = i.ToString();
        }

        // Hotkey tab handling
        private void textBox_hotkeySmoothOnOff_KeyDown(object sender, KeyEventArgs e)
        {
            SettingsRegisterHotkey(e, 0);
        }

        private void button_hotkeyOnOff_Click(object sender, EventArgs e)
        {
            SettingsUnregisterHotkey(0);
        }

        private void textBox_hotkeyOverlayOnOff_KeyDown(object sender, KeyEventArgs e)
        {
            SettingsRegisterHotkey(e, 1);
        }

        private void button_hotkeyOverlayOnOff_Click(object sender, EventArgs e)
        {
            SettingsUnregisterHotkey(1);
        }

        private void textBox_hotkeyToggleDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            SettingsRegisterHotkey(e, 2);
        }

        private void button_hotkeyToggleDisplay_Click(object sender, EventArgs e)
        {
            SettingsUnregisterHotkey(2);
        }

        private void textBox_hotkeyTabletMode_KeyDown(object sender, KeyEventArgs e)
        {
            SettingsRegisterHotkey(e, 3);
        }
        private void button_hotkeyTabletMode_Click(object sender, EventArgs e)
        {
            SettingsUnregisterHotkey(3);
        }

        private void SettingsRegisterHotkey(KeyEventArgs e, int id)
        {
            e.SuppressKeyPress = true;
            if (e.Modifiers != Keys.None)
            {
                Keys key = Keys.None;
                Hotkey.KeyModifiers modifiers = Hotkey.GetModifiers(e.KeyData, out key);
                if (key != Keys.None)
                {
                    string mod = "";
                    string[] mSplit = modifiers.ToString().Split(new[] { ", " }, StringSplitOptions.None);
                    foreach (string m in mSplit)
                    {
                        mod += m + "+";
                    }
                    string hotkey = e.KeyData.ToString();
                    if (!config.hotkeys.Any(hotkey.Contains))
                    {
                        switch (id)
                        {
                            case 0:
                                textBox_hotkeySmoothOnOff.Text = mod + key;
                                break;
                            case 1:
                                textBox_hotkeyOverlayOnOff.Text = mod + key;
                                break;
                            case 2:
                                textBox_hotkeyToggleDisplay.Text = mod + key;
                                break;
                            case 3:
                                textBox_hotkeyTabletMode.Text = mod + key;
                                break;
                        }
                        config.hotkeys[id] = hotkey;
                        mainForm.RegisterHotkey(mainForm.Handle, id, modifiers, key);
                    }
                }
            }
        }

        private  void SettingsUnregisterHotkey(int id)
        {
            try
            {
                mainForm.hotKeyHandling[id].Dispose();
                config.hotkeys[id] = "None";
                switch (id)
                {
                    case 0:
                        textBox_hotkeySmoothOnOff.Text = "";
                        break;
                    case 1:
                        textBox_hotkeyOverlayOnOff.Text = "";
                        break;
                    case 2:
                        textBox_hotkeyToggleDisplay.Text = "";
                        break;
                    case 3:
                        textBox_hotkeyTabletMode.Text = "";
                        break;
                }
            }
            catch
            {
                // No hotkey to dispose?
            }
        }

        // Re-enable config menu items
        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.ToolStripMenuItem_saveConfig.Enabled = true;
            mainForm.ToolStripMenuItem_restoreDefaults.Enabled = true;
        }
    }
}
