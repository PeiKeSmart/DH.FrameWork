using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace DH.LazyCaptcha.Generator.Image.Models
{
    public class TextGraphicDescription
    {
        public string Text { get; set; }
        public Font Font { get; set; }
        public Color Color { get; set; }
        public PointF Location { get; set; }
        public float BlendPercentage { get; set; } = 1;
    }
}
