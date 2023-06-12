namespace DG.BaiDuAipSdk.ContentCensor
{
    public class Base : AipServiceBase
    {
        public Base(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        protected AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Formed
            };
        }
    }
}
