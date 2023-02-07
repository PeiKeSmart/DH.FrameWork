using DH.PaySharp.Request;
using DH.PaySharp.Response;

namespace DH.PaySharp
{
    public interface IGateway
    {
        TResponse Execute<TModel, TResponse>(Request<TModel, TResponse> request) where TResponse : IResponse;
    }
}
