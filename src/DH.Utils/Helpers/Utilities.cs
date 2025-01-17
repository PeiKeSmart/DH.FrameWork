using System.Drawing;

using NewLife;

namespace DH.Helpers;

public static class BitmapUtilities {
    public static Bitmap Concat(this Bitmap bitmap, Bitmap[] bitmaps, Size resultSize)
    {
        if (bitmap == null)
            throw new ArgumentNullException(nameof(bitmap));
        if (bitmaps == null)
            throw new ArgumentNullException(nameof(bitmaps));

        var totalWidth = bitmap.Width;
        var totalHeight = bitmap.Height;

        var lastWidth = 0;
        var lastHeight = 0;

        foreach (var b in bitmaps)
        {
            totalHeight += b.Height;
            totalWidth += b.Width;
        }

        using Graphics g = Graphics.FromImage(bitmap);
        foreach (var b in bitmaps)
        {
            g.DrawImage(b, lastWidth, lastHeight, 1920, 1080);

            lastWidth += b.Width;
            lastHeight += b.Height;
        }
        return bitmap.Resize(resultSize);
    }

    public static Bitmap Resize(this Bitmap bitmap, int width, int height)
    {
        return new Bitmap(bitmap, width, height);
    }
    public static Bitmap Resize(this Bitmap bitmap, double width, double height)
    {
        return new Bitmap(bitmap, width.ToInt(), height.ToInt());
    }
    public static Bitmap Resize(this Bitmap bitmap, Size newSize)
    {
        return new Bitmap(bitmap, newSize);
    }
    public static string GetImagePath(string name)
    {
        if (File.Exists(name + ".jpg"))
            return name + ".jpg";
        else if (File.Exists(name + ".jpeg"))
            return name + ".jpeg";
        else if (File.Exists(name + ".png"))
            return name + ".png";
        return string.Empty;
    }
}
