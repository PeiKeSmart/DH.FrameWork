using SkiaSharp;

namespace DH.SLazyCaptcha.Generator.Image.Models;

public class InterferenceLineGraphicDescription {
    public SKColor Color { get; set; }
    public SKPoint Start { get; set; }
    public SKPoint Ctrl1 { get; set; }
    public SKPoint Ctrl2 { get; set; }
    public SKPoint End { get; set; }
    public float BlendPercentage { get; set; } = 1;
}