using SkiaSharp;

namespace DH.SLazyCaptcha;

public class DefaultColors {
    public static DefaultColors Instance = new DefaultColors();

    public List<SKColor> Colors = new List<SKColor>
        {
            SKColor.Parse("#0087ff"),
            SKColor.Parse("#339933"),
            SKColor.Parse("#ff6666"),
            SKColor.Parse("#ff9900"),
            SKColor.Parse("#996600"),
            SKColor.Parse("#996699"),
            SKColor.Parse("#339999"),
            SKColor.Parse("#6666ff"),
            SKColor.Parse("#0066cc"),
            SKColor.Parse("#cc3333"),
            SKColor.Parse("#0099cc"),
            SKColor.Parse("#003366"),
        };
}