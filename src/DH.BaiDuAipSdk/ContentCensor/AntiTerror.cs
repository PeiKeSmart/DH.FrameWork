using Newtonsoft.Json.Linq;

namespace DG.BaiDuAipSdk.ContentCensor
{
    /// <summary>
    ///     暴恐
    /// </summary>
    public class AntiTerror : Base
    {
        public const string ANTI_TERROR = "https://aip.baidubce.com/rest/2.0/antiterror/v1/detect";

        public AntiTerror(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        /// <summary>
        ///     暴恐识别
        /// </summary>
        /// <param name="image">图像字节数组</param>
        /// <returns></returns>
        public JObject Detect(byte[] image)
        {
            CheckNotNull(image, "image");
            PreAction();
            var aipReq = DefaultRequest(ANTI_TERROR);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return PostAction(aipReq);
        }
    }
}
