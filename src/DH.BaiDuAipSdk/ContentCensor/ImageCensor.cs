using Newtonsoft.Json.Linq;

namespace DG.BaiDuAipSdk.ContentCensor
{
    /// <summary>
    /// 图像审核
    /// </summary>
    public class ImageCensor : Base
    {
        public const string USER_DEFINED = "https://aip.baidubce.com/rest/2.0/solution/v1/img_censor/v2/user_defined";

        public ImageCensor(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        /// <summary>
        /// 图像审核接口
        /// 为用户提供色情识别、暴恐识别、政治敏感人物识别、广告识别、图像垃圾文本识别（反作弊）、恶心图像识别等一系列图像识别接口的一站式服务调用，
        /// 并且支持用户在控制台中自定义配置所有接口的报警阈值和疑似区间，上传自定义文本黑库和敏感人物名单等。
        /// 相比于组合服务接口，本接口除了支持自定义配置外，还对返回结果进行了总体的包装，按照用户在控制台中配置的规则直接返回是否合规，如果不合规则指出具体不合规的内容。
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject UserDefined(byte[] image, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_DEFINED);

            CheckNotNull(image, "image");
            aipReq.Bodys["image"] = System.Convert.ToBase64String(image);
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }

        /// <summary>
        /// 图像审核接口
        /// 为用户提供色情识别、暴恐识别、政治敏感人物识别、广告识别、图像垃圾文本识别（反作弊）、恶心图像识别等一系列图像识别接口的一站式服务调用，
        /// 并且支持用户在控制台中自定义配置所有接口的报警阈值和疑似区间，上传自定义文本黑库和敏感人物名单等。
        /// 相比于组合服务接口，本接口除了支持自定义配置外，还对返回结果进行了总体的包装，按照用户在控制台中配置的规则直接返回是否合规，如果不合规则指出具体不合规的内容。
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public JObject UserDefinedUrl(string imageUrl, Dictionary<string, object> options = null)
        {
            var aipReq = DefaultRequest(USER_DEFINED);

            CheckNotNull(imageUrl, "imageUrl");
            aipReq.Bodys["imgUrl"] = imageUrl;
            PreAction();

            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            return PostAction(aipReq);
        }


    }
}
