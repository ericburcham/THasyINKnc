using System;
using System.Drawing;

namespace ParallelTasks
{
    public static class BitmapExtensions
    {
        public static void CopyPixels(this Bitmap source, Bitmap destination)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");
            for (var y = 0; y < source.Height; y++)
            {
                for (var x = 0; x < source.Width; x++)
                {
                    var p = source.GetPixel(x, y);
                    destination.SetPixel(x, y, Color.FromArgb(p.R, p.G, p.B)); // apparently preserves alpha
                }
            }
        }

        public static Bitmap ResizeBitmap(this Bitmap image, Size size)
        {
            return new Bitmap(image, size);
        }

        public static void SetAlpha(this Bitmap bitmap, int alpha)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");
            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Height; y++)
                {
                    var p = bitmap.GetPixel(x, y);
                    p = Color.FromArgb(alpha, p.R, p.G, p.B);
                    bitmap.SetPixel(x, y, p);
                }
            }
        }

        public static void SetGray(this Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    var luma = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    bitmap.SetPixel(x, y, Color.FromArgb(luma, luma, luma));
                }
            }
        }
    }
}
