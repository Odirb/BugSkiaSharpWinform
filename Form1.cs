using System;
using System.Drawing;
using System.Windows.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace SkiaWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            _skControl.PaintSurface += OnPaintSurface;
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var icon = SystemIcons.Information;
            var skBitmap = icon.ToBitmap().ToSKBitmap();

            var info = e.Info;
            var surface = e.Surface;

            float scale = Math.Min((float)info.Width / skBitmap.Width, (float)info.Height / skBitmap.Height);
            float x = (info.Width - scale * skBitmap.Width) / 2;
            float y = (info.Height - scale * skBitmap.Height) / 2;
            SKRect destRect = new SKRect(x, y, x + scale * skBitmap.Width,
                y + scale * skBitmap.Height);

            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.FilterQuality = SKFilterQuality.None;

                e.Surface.Canvas.Clear();
                e.Surface.Canvas.DrawBitmap(skBitmap, destRect, paint);
            }
        }
    }
}
