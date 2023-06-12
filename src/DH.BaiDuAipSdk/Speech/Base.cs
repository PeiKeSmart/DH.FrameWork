using System.Net;

namespace DG.BaiDuAipSdk.Speech
{
    public class Base : AipServiceBase
    {
        public Base(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
            IsDev = true;
        }

        public Base(string appId, string apiKey, string secretKey) : base(appId, apiKey, secretKey)
        {
            IsDev = true;
        }

        protected string Cuid
        {
            get { return Utils.Md5(Token); }
        }


        protected override void DoAuthorization()
        {
            lock (AuthLock)
            {
                if (!NeetAuth())
                    return;

                var resp = Auth.OpenApiFetchToken(ApiKey, SecretKey, true);

                ExpireAt = DateTime.Now.AddSeconds((int)resp["expires_in"] - 1);
                IsDev = true;
                Token = (string)resp["access_token"];
                HasDoneAuthoried = true;
            }
        }


        protected override HttpWebRequest GenerateWebRequest(AipHttpRequest aipRequest)
        {
            return aipRequest.GenerateSpeechRequest(this.Timeout);
        }
    }
}
