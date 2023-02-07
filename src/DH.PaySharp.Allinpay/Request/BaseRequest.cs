using DH.PaySharp.Request;
using DH.PaySharp.Response;
using DH.PaySharp.Utils;

namespace DH.PaySharp.Allinpay.Request
{
    public class BaseRequest<TModel, TResponse> : Request<TModel, TResponse> where TResponse : IResponse
    {
        public override void AddGatewayData(TModel model)
        {
            base.AddGatewayData(model);

            GatewayData.Add(model, StringCase.Lower);
        }

        internal virtual void Execute(Merchant merchant)
        {
            if (!string.IsNullOrEmpty(NotifyUrl))
            {
                GatewayData.Add("notify_url", NotifyUrl);
            }
        }
    }
}
