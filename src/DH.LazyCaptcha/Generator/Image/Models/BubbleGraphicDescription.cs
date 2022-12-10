using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;

namespace DH.LazyCaptcha.Generator.Image.Models
{
    public class BubbleGraphicDescription
    {
        public IPath Path { get; set; }
        public Color Color { get; set; }
        public float Thickness { get; set; }
        public float BlendPercentage { get; set; } = 1;
    }
}
