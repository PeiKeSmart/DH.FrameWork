namespace DH.Core.Http
{
    /// <summary>
    /// 表示与HTTP功能相关的默认值
    /// </summary>
    public static partial class DHHttpDefaults
    {
        /// <summary>
        /// 获取默认HTTP客户端的名称
        /// </summary>
        public static string DefaultHttpClient => "default";

        /// <summary>
        /// 获取存储值的请求项的名称，该值指示客户端是否正在使用POST重定向到新位置
        /// </summary>
        public static string IsPostBeingDoneRequestItem => "dh.IsPOSTBeingDone";

        /// <summary>
        /// 获取请求项的名称，该请求项存储指示请求是否由通用路由转换器重定向的值
        /// </summary>
        public static string GenericRouteInternalRedirect => "dh.RedirectFromGenericPathRoute";

        /// <summary>
        /// 获取HTTP_CLUSTER_HTTPS标头的名称
        /// </summary>
        public static string HttpClusterHttpsHeader => "HTTP_CLUSTER_HTTPS";

        /// <summary>
        /// 获取HTTP_X_FORWARDED_PROTO标头的名称
        /// </summary>
        public static string HttpXForwardedProtoHeader => "X-Forwarded-Proto";

        /// <summary>
        /// 获取X-FORWARDED-FOR标头的名称
        /// </summary>
        public static string XForwardedForHeader => "X-FORWARDED-FOR";
    }
}
