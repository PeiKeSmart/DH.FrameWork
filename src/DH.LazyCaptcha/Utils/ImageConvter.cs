using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DH.LazyCaptcha.Utils
{
    /// <summary>
    /// 图片处理，支持图像二值化、图像灰度化及图像灰度反转等
    /// 参考<![CDATA[https://blog.csdn.net/sqqyq/article/details/103789579]]>
    /// </summary>
    public class ImageConvter
    {

        /// <summary>
        /// 黑白二值化
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Image<Rgba32> Binaryzation(Image<Rgba32> image)
        {
            image = ToGray(image);//先灰度处理
            int threshold = 180;//定义阈值
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    //获取该像素点的RGB的颜色
                    var color = image[i, j];
                    //计算颜色,大于平均值为黑,小于平均值为白
                    System.Drawing.Color newColor = color.B < threshold ? System.Drawing.Color.FromArgb(0, 0, 0) : System.Drawing.Color.FromArgb(255, 255, 255);
                    //修改该像素点的RGB的颜色
                    image[i, j] = new Rgba32(newColor.R, newColor.G, newColor.B, newColor.A);
                }
            }
            return image;
        }

        /// <summary>
        /// 图像灰度处理
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Image<Rgba32> ToGray(Image<Rgba32> img)
        {
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    var color = img[i, j];
                    //计算灰度值
                    int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                    System.Drawing.Color newColor = System.Drawing.Color.FromArgb(gray, gray, gray);
                    //修改该像素点的RGB的颜色
                    img[i, j] = new Rgba32(newColor.R, newColor.G, newColor.B, newColor.A);
                }
            }
            return img;
        }

        /// <summary>
        /// 图像红色处理
        /// </summary>
        /// <param name="image"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Image<Rgba32> BinaryzationRed(Image<Rgba32> image, int r, int g, int b)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    //获取该像素点的RGB的颜色
                    var color = image[i, j];
                    //计算颜色,大于平均值为黑,小于平均值为白
                    if (color.R >= r && color.G <= g && color.B <= b)
                    { //修改该像素点的RGB的颜色
                        image[i, j] = Color.Red;
                    }
                    else
                    {
                        image[i, j] = Color.Transparent;
                    }
                }
            }
            return image;
        }

    }
}
