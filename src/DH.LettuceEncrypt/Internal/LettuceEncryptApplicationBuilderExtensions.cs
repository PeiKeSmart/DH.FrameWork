using LettuceEncrypt.Internal;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// ��������
/// </summary>
internal static class LettuceEncryptApplicationBuilderExtensions
{
    /// <summary>
    /// �����������֤������Ȩ���м��
    /// </summary>
    /// <param name="app">Ӧ�ó���������</param>
    /// <returns>Ӧ�ó���������</returns>
    public static IApplicationBuilder UseHttpChallengeResponseMiddleware(this IApplicationBuilder app)
    {
        app.Map("/.well-known/acme-challenge", mapped =>
        {
            mapped.UseMiddleware<HttpChallengeResponseMiddleware>();
        });
        return app;
    }
}
