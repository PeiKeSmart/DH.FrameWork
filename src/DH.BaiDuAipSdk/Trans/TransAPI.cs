using Flurl.Http;

using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DG.BaiDuAipSdk.Trans
{
    /// <summary>
    /// 百度翻译工具类
    /// </summary>
    public static class TransAPI
    {
        /// <summary>
        /// 翻译。参考来源：https://fanyi-api.baidu.com/doc/21
        /// </summary>
        /// <param name="AppId">百度翻译AppId</param>
        /// <param name="SecretKey">百度翻译密钥</param>
        /// <param name="Content">原文</param>
        /// <param name="To">翻译目标语言</param>
        /// <param name="From">源语言。默认为auto</param>
        /// <returns></returns>
        public static async Task<String> Translate(String AppId, String SecretKey, String Content, String To, String From = "auto")
        {
            Random rd = new Random();
            string salt = rd.Next(100000).ToString();

            var sign = EncryptString(AppId + Content + salt + SecretKey);

            var result = await "http://api.fanyi.baidu.com/api/trans/vip/translate"
                .PostUrlEncodedAsync(new
                {
                    q = Content,
                    from = From,
                    to = To,
                    appid = AppId,
                    salt = salt,
                    sign = sign
                })
                .ReceiveString();

            return result;
        }

        // 计算MD5值
        public static string EncryptString(string str)
        {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }

    }
}
