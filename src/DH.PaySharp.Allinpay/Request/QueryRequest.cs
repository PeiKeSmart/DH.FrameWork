using DH.PaySharp.Allinpay.Domain;
using DH.PaySharp.Allinpay.Response;

namespace DH.PaySharp.Allinpay.Request
{
    public class QueryRequest : BaseRequest<QueryModel, QueryResponse>
    {
        public QueryRequest()
        {
            RequestUrl = "/apiweb/unitorder/query";
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
