using System;
using System.Drawing;
using System.Windows.Forms;

namespace Silky_Shark
{
    public partial class Overlay : Form
    {
        public Point cursorPos = new Point(0, 0);
        public Color cursorColor = Color.FromArgb(128,128,128);
        public Color cursorFillColor = Color.FromArgb(255, 255, 254);
        public CursorType cursorType = CursorType.Bullseye;

        public Overlay()
        {
            InitializeComponent();
            TopMost = true;
            Paint += new PaintEventHandler(OverlayPaint);
        }

        public enum CursorType
        {
           Bullseye,
           Circle,
           Crosshair,
           Cursor,
           TinyCursor,
           Pen
        }

        public void OverlayPaint(object sender, PaintEventArgs e)
        {
            Point windowOffset = Bounds.Location;
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(cursorColor);
            SolidBrush penBrush = new SolidBrush(cursorColor);
            SolidBrush fillBrush = new SolidBrush(cursorFillColor);
            int x = -1 * windowOffset.X + cursorPos.X;
            int y = -1 * windowOffset.Y + cursorPos.Y;
            Point offset;

            switch (cursorType)
            {
                case CursorType.Bullseye:
                    // Bullseye
                    offset = new Point(x - 5, y - 5);
                    graphics.DrawEllipse(pen, offset.X, offset.Y, 10, 10);
                    graphics.FillRectangle(penBrush, offset.X + 5, offset.Y + 5, 1, 1);
                    break;
                case CursorType.Circle:
                    // Circle
                    offset = new Point(x - 5, y - 5);
                    graphics.DrawEllipse(pen, offset.X, offset.Y, 10, 10);
                    break;
                case CursorType.Crosshair:
                    // Crosshair
                    offset = new Point(x - 5, y - 5);
                    graphics.DrawLine(pen, offset.X + 5, offset.Y + 0, offset.X + 5, offset.Y + 10);
                    graphics.DrawLine(pen, offset.X + 0, offset.Y + 5, offset.X + 10, offset.Y + 5);
                    break;
                case CursorType.Cursor:
                    // Cursor
                    offset = new Point(x, y);
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
                    break;
                case CursorType.TinyCursor:
                    // Tiny Cursor
                    offset = new Point(x, y);
                    Point[] tcPoint =
                    {
                        new Point(offset.X + 0, offset.Y + 0),
                        new Point(offset.X + 0, offset.Y + 9),
                        new Point(offset.X + 3, offset.Y + 6),
                        new Point(offset.X + 6, offset.Y + 6)
                    };
                    graphics.FillPolygon(fillBrush, tcPoint);
                    graphics.DrawPolygon(pen, tcPoint);
                    break;
                case CursorType.Pen:
                    // Pen
                    offset = new Point(x, y - 12);
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
                    break;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.ExStyle |= 0x80000 | 0x20 | 0x80 | 0x00000008;
                return p;
            }
        }
    }
}
