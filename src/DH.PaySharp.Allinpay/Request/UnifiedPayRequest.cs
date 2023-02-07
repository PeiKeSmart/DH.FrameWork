using DH.PaySharp.Allinpay.Domain;
using DH.PaySharp.Allinpay.Response;

namespace DH.PaySharp.Allinpay.Request
{
    public class UnifiedPayRequest : BaseRequest<UnifiedPayModel, UnifiedPayResponse>
    {
        public UnifiedPayRequest()
        {
            RequestUrl = "/apiweb/unitorder/pay";
        }
    }
}
