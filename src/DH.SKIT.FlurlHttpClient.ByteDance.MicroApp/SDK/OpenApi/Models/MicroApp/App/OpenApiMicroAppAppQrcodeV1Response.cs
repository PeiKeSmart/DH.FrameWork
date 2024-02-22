﻿namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.SDK.OpenApi.Models
{
    /// <summary>
    /// <para>表示 [POST] /v1/microapp/app/qrcode 接口的响应。</para>
    /// </summary>
    public class OpenApiMicroAppAppQrcodeV1Response : ByteDanceMicroAppOpenApiResponse
    {
        public override bool IsSuccessful()
        {
            return base.IsSuccessful() && base.GetRawBytes()?.Length > 0;
        }
    }
}
