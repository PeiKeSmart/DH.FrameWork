namespace DH.Core
{
    /// <summary>
    /// MimeType常量集合使用,以避免输入错误
    /// 如果需要MimeTypes缺失，请随时添加
    /// </summary>
    public static class MimeTypes
    {
        #region application/*

        /// <summary>
        /// 类型
        /// </summary>
        public static string ApplicationForceDownload => "application/force-download";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ApplicationJson => "application/json";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ApplicationManifestJson => "application/manifest+json";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ApplicationOctetStream => "application/octet-stream";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ApplicationPdf => "application/pdf";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ApplicationRssXml => "application/rss+xml";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ApplicationXml => "application/xml";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ApplicationXWwwFormUrlencoded => "application/x-www-form-urlencoded";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ApplicationXZipCo => "application/x-zip-co";

        #endregion

        #region image/*

        /// <summary>
        /// 类型
        /// </summary>
        public static string ImageBmp => "image/bmp";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ImageGif => "image/gif";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ImageJpeg => "image/jpeg";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ImagePJpeg => "image/pjpeg";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ImagePng => "image/png";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ImageTiff => "image/tiff";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ImageWebp => "image/webp";

        /// <summary>
        /// 类型
        /// </summary>
        public static string ImageSvg => "image/svg+xml";

        #endregion

        #region text/*

        /// <summary>
        /// 类型
        /// </summary>
        public static string TextCss => "text/css";

        /// <summary>
        /// 类型
        /// </summary>
        public static string TextCsv => "text/csv";

        /// <summary>
        /// 类型
        /// </summary>
        public static string TextJavascript => "text/javascript";

        /// <summary>
        /// 类型
        /// </summary>
        public static string TextPlain => "text/plain";

        /// <summary>
        /// 类型
        /// </summary>
        public static string TextXlsx => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        #endregion
    }
}
