using System.Drawing;

namespace DH.QRCode.Codec.Data;

public class QRCodeBitmapImage : QRCodeImage {
    private Bitmap image;

    public virtual int Width => image.Width;

    public virtual int Height => image.Height;

    public QRCodeBitmapImage(Bitmap image)
    {
        this.image = image;
    }

    public virtual int getPixel(int x, int y)
    {
        return image.GetPixel(x, y).ToArgb();
    }
}