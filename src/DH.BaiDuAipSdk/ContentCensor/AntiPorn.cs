using Newtonsoft.Json.Linq;

namespace DG.BaiDuAipSdk.ContentCensor
{
    /// <summary>
    ///     黄反相关
    /// </summary>
    public class AntiPorn : Base
    {
        public const string ANTI_PORN_URL = "https://aip.baidubce.com/rest/2.0/antiporn/v1/detect";
        public const string ANTI_PORN_GIF_URL = "https://aip.baidubce.com/rest/2.0/antiporn/v1/detect_gif";

        public AntiPorn(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        /// <summary>
        ///     黄反识别
        /// </summary>
        /// <param name="image">图像字节数组</param>
        /// <returns>识别结果</returns>
        public JObject Detect(byte[] image)
        {
            CheckNotNull(image, "image");
            PreAction();
            var aipReq = DefaultRequest(ANTI_PORN_URL);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return PostAction(aipReq);
        }

        /// <summary>
        ///     GIF色情图像识别
        /// </summary>
        /// <param name="image">图像字节数组</param>
        /// <returns>识别结果</returns>
        public JObject DetectGif(byte[] image)
        {
            CheckNotNull(image, "image");
            PreAction();
            var aipReq = DefaultRequest(ANTI_PORN_GIF_URL);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return PostAction(aipReq);
        }
    }
}
