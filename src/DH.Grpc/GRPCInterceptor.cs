using Grpc.Core;
using Grpc.Core.Interceptors;

using NewLife.Log;

namespace DH.Grpc;

public class GRPCInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        XTrace.Log.Debug($"starting call");

        var response = await base.UnaryServerHandler(request, context, continuation);

        XTrace.Log.Debug($"finished call");

        return response;
    }
}