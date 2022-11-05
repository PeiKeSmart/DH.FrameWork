﻿using System.IO;

namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Utilities
{
    internal static class FileNameToContentTypeMapper
    {
        public static string? GetContentTypeForMaterial(string fileName)
        {
            string extension = Path.GetExtension(fileName ?? "/")?.ToLower() ?? string.Empty;
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".pdf":
                    return "application/pdf";
            }

            return null;
        }
    }
}
