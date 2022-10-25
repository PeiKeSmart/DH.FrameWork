namespace DH.Web.Framework.Mvc.Routing
{
    /// <summary>
    /// 表示与路由相关的默认值
    /// </summary>
    public static partial class DHRoutingDefaults
    {
        #region Route names

        public static partial class RouteName
        {
            public static partial class Generic
            {
                /// <summary>
                /// 获取通用路由名称
                /// </summary>
                public static string GenericUrl => "GenericUrl";

                /// <summary>
                /// 获取通用路由（语言代码为en/）名称
                /// </summary>
                public static string GenericUrlWithLanguageCode => "GenericUrlWithLanguageCode";

                /// <summary>
                /// 获取通用目录路由（语言代码为en/）名称
                /// </summary>
                public static string GenericCatalogUrl => "GenericCatalogUrl";

                /// <summary>
                /// 获取通用目录路由名称
                /// </summary>
                public static string GenericCatalogUrlWithLanguageCode => "GenericCatalogUrlWithLanguageCode";

                /// <summary>
                /// 获取通用产品目录路由名称
                /// </summary>
                public static string ProductCatalog => "ProductCatalog";

                /// <summary>
                /// 获取通用产品路线名称
                /// </summary>
                public static string Product => "ProductDetails";

                /// <summary>
                /// 获取通用类别路由名称
                /// </summary>
                public static string Category => "Category";

                /// <summary>
                /// 获取通用制造商路由名称
                /// </summary>
                public static string Manufacturer => "Manufacturer";

                /// <summary>
                /// 获取通用供应商路由名称
                /// </summary>
                public static string Vendor => "Vendor";

                /// <summary>
                /// 获取通用新闻项路由名称
                /// </summary>
                public static string NewsItem => "NewsItem";

                /// <summary>
                /// 获取通用日志发布路由名称
                /// </summary>
                public static string BlogPost => "BlogPost";

                /// <summary>
                /// 获取通用主题路由名称
                /// </summary>
                public static string Topic => "TopicDetails";

                /// <summary>
                /// 获取通用产品标记路由名称
                /// </summary>
                public static string ProductTag => "ProductsByTag";
            }
        }

        #endregion

        #region Route values keys

        public static partial class RouteValue
        {
            /// <summary>
            /// 获取操作路由值的默认键
            /// </summary>
            public static string Action => "action";

            /// <summary>
            /// 获取控制器路由值的默认键
            /// </summary>
            public static string Controller => "controller";

            /// <summary>
            /// 获取永久重定向路由值的默认键
            /// </summary>
            public static string PermanentRedirect => "permanentRedirect";

            /// <summary>
            /// 获取url路由值的默认键
            /// </summary>
            public static string Url => "url";

            /// <summary>
            /// 获取博客帖子id路由值的默认键
            /// </summary>
            public static string BlogPostId => "blogpostId";

            /// <summary>
            /// 获取类别id路由值的默认键
            /// </summary>
            public static string CategoryId => "categoryid";

            /// <summary>
            /// 获取制造商id路由值的默认键
            /// </summary>
            public static string ManufacturerId => "manufacturerid";

            /// <summary>
            /// 获取新闻项id路由值的默认键
            /// </summary>
            public static string NewsItemId => "newsitemId";

            /// <summary>
            /// 获取产品id路由值的默认键
            /// </summary>
            public static string ProductId => "productid";

            /// <summary>
            /// 获取产品标记id路由值的默认键
            /// </summary>
            public static string ProductTagId => "productTagId";

            /// <summary>
            /// 获取主题id路由值的默认键
            /// </summary>
            public static string TopicId => "topicid";

            /// <summary>
            /// 获取供应商id路由值的默认键
            /// </summary>
            public static string VendorId => "vendorid";

            /// <summary>
            /// 获取语言路由值
            /// </summary>
            public static string Language => "language";

            /// <summary>
            /// 获取用户名路由值的默认键
            /// </summary>
            public static string SeName => "SeName";

            /// <summary>
            /// 获取目录路由值的默认键
            /// </summary>
            public static string CatalogSeName => "CatalogSeName";
        }

        #endregion

        /// <summary>
        /// 获取语言参数转换器
        /// </summary>
        public static string LanguageParameterTransformer => "lang";
    }
}
