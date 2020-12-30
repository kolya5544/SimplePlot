using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SimplePlot
{
    class Program
    {
        static void Main(string[] args)
        {
            int W = 1000; int H = 1000;
            Bitmap bmp = new Bitmap(W, H);

            for (int x = 0; x < W; x++)
            {
                bmp.SetPixel(x, H / 2, Color.Black);
            }

            for (int y = 0; y < H; y++)
            {
                bmp.SetPixel(W / 2 - 1, y, Color.Black);
            }

            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            Point lastDot = Point.Empty;
            for (int x = -(W / 2); x < (W / 2); x++)
            {
                int y = PlotValue(x);
                int where = -y + (H / 2);

                if (lastDot == Point.Empty)
                {
                    lastDot = new Point(x + (W / 2), where); continue;
                }
                var newDot = new Point(x + (W / 2), where);
                if (newDot.Y <= 1000000 && newDot.Y >= -1000000)
                {
                    g.DrawLine(Pens.Red, lastDot, newDot);
                    lastDot = newDot;
                }
                else
                {
                    lastDot = new Point(newDot.X, newDot.Y > 0 ? 100000 : -100000);
                }

            }

            bmp.Save("output.png");
        }

        public static int ZOOM = 100;

        private static int PlotValue(int x)
        {
            double val = (double)x / ZOOM;

            return (int)Math.Round(val * val * ZOOM);
        }
    }
}
