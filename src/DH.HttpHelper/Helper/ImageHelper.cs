using System.Drawing;
using System.IO;

namespace DG.HttpHelper.Helper
{
    internal class ImageHelper
    {
        /// <summary>
        /// 将字节数组转为图片
        /// </summary>
        /// <param name=" b">字节数组</param>
        /// <returns>返回图片</returns>
        internal static System.Drawing.Image ByteToImage(byte[] b)
        {
            try
            {
                MemoryStream ms = new MemoryStream(b);
                return Bitmap.FromStream(ms, true);
            }
            catch { return null; }
        }
    }
}
