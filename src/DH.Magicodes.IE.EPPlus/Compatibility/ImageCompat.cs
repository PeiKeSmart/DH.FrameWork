﻿using OfficeOpenXml.Utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace OfficeOpenXml.Compatibility
{
    internal class ImageCompat
    {
        internal static byte[] GetImageAsByteArray(Image image, IImageFormat format)
        {
            using (var ms = RecyclableMemoryStream.GetStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }
    }
}
