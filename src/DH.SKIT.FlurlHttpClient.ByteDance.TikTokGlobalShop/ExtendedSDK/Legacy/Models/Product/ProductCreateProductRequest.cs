using System.Collections.Generic;

namespace SKIT.FlurlHttpClient.ByteDance.TikTokGlobalShop.ExtendedSDK.Legacy.Models
{
    /// <summary>
    /// <para>表示 [POST] /products 接口的请求。</para>
    /// </summary>
    public class ProductCreateProductRequest : TikTokShopLegacyRequest
    {
        public static class Types
        {
            public class Image
            {
                /// <summary>
                /// 获取或设置图片 ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("id")]
                [System.Text.Json.Serialization.JsonPropertyName("id")]
                public string ImageId { get; set; } = string.Empty;
            }

            public class Video
            {
                /// <summary>
                /// 获取或设置文件 ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("video_id")]
                [System.Text.Json.Serialization.JsonPropertyName("video_id")]
                public string FileId { get; set; } = string.Empty;
            }

            public class SizeChart
            {
                /// <summary>
                /// 获取或设置图片 ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("img_id")]
                [System.Text.Json.Serialization.JsonPropertyName("img_id")]
                public string ImageId { get; set; } = string.Empty;
            }

            public class SKU
            {
                public static class Types
                {
                    public class SalesAttribute
                    {
                        public static class Types
                        {
                            public class Image : ProductCreateProductRequest.Types.Image
                            {
                            }
                        }

                        /// <summary>
                        /// 获取或设置属性 ID。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("attribute_id")]
                        [System.Text.Json.Serialization.JsonPropertyName("attribute_id")]
                        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Common.NumericalStringConverter))]
                        public string AttributeId { get; set; } = string.Empty;

                        /// <summary>
                        /// 获取或设置属性名称。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("attribute_name")]
                        [System.Text.Json.Serialization.JsonPropertyName("attribute_name")]
                        public string? AttributeName { get; set; }

                        /// <summary>
                        /// 获取或设置属性值 ID。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("value_id")]
                        [System.Text.Json.Serialization.JsonPropertyName("value_id")]
                        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Common.NumericalStringConverter))]
                        public string? ValueId { get; set; }

                        /// <summary>
                        /// 获取或设置自定义属性值。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("custom_value")]
                        [System.Text.Json.Serialization.JsonPropertyName("custom_value")]
                        public string? CustomValue { get; set; }

                        /// <summary>
                        /// 获取或设置图片信息。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("sku_img")]
                        [System.Text.Json.Serialization.JsonPropertyName("sku_img")]
                        public Types.Image? Image { get; set; }
                    }

                    public class StockInfo
                    {
                        /// <summary>
                        /// 获取或设置仓库 ID。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("warehouse_id")]
                        [System.Text.Json.Serialization.JsonPropertyName("warehouse_id")]
                        public string WarehouseId { get; set; } = string.Empty;

                        /// <summary>
                        /// 获取或设置可用库存。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("available_stock")]
                        [System.Text.Json.Serialization.JsonPropertyName("available_stock")]
                        public int AvailableStock { get; set; }
                    }

                    public class IdentifierCodeInfo
                    {
                        /// <summary>
                        /// 获取或设置标识码类型。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("identifier_code_type")]
                        [System.Text.Json.Serialization.JsonPropertyName("identifier_code_type")]
                        public int IdentifierCodeType { get; set; }

                        /// <summary>
                        /// 获取或设置标识码。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("identifier_code")]
                        [System.Text.Json.Serialization.JsonPropertyName("identifier_code")]
                        public string IdentifierCode { get; set; } = string.Empty;
                    }
                }

                /// <summary>
                /// 获取或设置外部 SKU ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("outer_sku_id")]
                [System.Text.Json.Serialization.JsonPropertyName("outer_sku_id")]
                public string? OuterSKUId { get; set; }

                /// <summary>
                /// 获取或设置销售属性列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("sales_attributes")]
                [System.Text.Json.Serialization.JsonPropertyName("sales_attributes")]
                public IList<Types.SalesAttribute>? SalesAttributeList { get; set; }

                /// <summary>
                /// 获取或设置库存信息列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("stock_infos")]
                [System.Text.Json.Serialization.JsonPropertyName("stock_infos")]
                public IList<Types.StockInfo>? StockInfoList { get; set; }

                /// <summary>
                /// 获取或设置卖家 SKU 信息。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("seller_sku")]
                [System.Text.Json.Serialization.JsonPropertyName("seller_sku")]
                public string? SellerSKU { get; set; }

                /// <summary>
                /// 获取或设置原价。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("original_price")]
                [System.Text.Json.Serialization.JsonPropertyName("original_price")]
                public decimal OriginalPrice { get; set; }

                /// <summary>
                /// 获取或设置产品标识码信息。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("product_identifier_code")]
                [System.Text.Json.Serialization.JsonPropertyName("product_identifier_code")]
                public Types.IdentifierCodeInfo? IdentifierCodeInfo { get; set; }
            }

            public class ProductCertification
            {
                public static class Types
                {
                    public class Image : ProductCreateProductRequest.Types.Image
                    {
                    }

                    public class File
                    {
                        /// <summary>
                        /// 获取或设置文件 ID。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("id")]
                        [System.Text.Json.Serialization.JsonPropertyName("id")]
                        public string FileId { get; set; } = string.Empty;

                        /// <summary>
                        /// 获取或设置文件名称。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("name")]
                        [System.Text.Json.Serialization.JsonPropertyName("name")]
                        public string? FileName { get; set; }

                        /// <summary>
                        /// 获取或设置文件类型。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("type")]
                        [System.Text.Json.Serialization.JsonPropertyName("type")]
                        public string? FileType { get; set; }
                    }
                }

                /// <summary>
                /// 获取或设置商品认证 ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("id")]
                [System.Text.Json.Serialization.JsonPropertyName("id")]
                [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Common.NumericalStringConverter))]
                public string CertificationId { get; set; } = string.Empty;

                /// <summary>
                /// 获取或设置图片列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("images")]
                [System.Text.Json.Serialization.JsonPropertyName("images")]
                public IList<Types.Image>? ImageList { get; set; }

                /// <summary>
                /// 获取或设置文件列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("files")]
                [System.Text.Json.Serialization.JsonPropertyName("files")]
                public IList<Types.File>? FileList { get; set; }
            }

            public class ProductAttribute
            {
                public static class Types
                {
                    public class ProductAttributeValue
                    {
                        /// <summary>
                        /// 获取或设置属性值 ID。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("value_id")]
                        [System.Text.Json.Serialization.JsonPropertyName("value_id")]
                        public string ValueId { get; set; } = string.Empty;

                        /// <summary>
                        /// 获取或设置属性值名称。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("value_name")]
                        [System.Text.Json.Serialization.JsonPropertyName("value_name")]
                        public string ValueName { get; set; } = string.Empty;
                    }
                }

                /// <summary>
                /// 获取或设置属性 ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("attribute_id")]
                [System.Text.Json.Serialization.JsonPropertyName("attribute_id")]
                public string AttributeId { get; set; } = string.Empty;

                /// <summary>
                /// 获取或设置属性值列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("attribute_values")]
                [System.Text.Json.Serialization.JsonPropertyName("attribute_values")]
                public IList<Types.ProductAttributeValue> ValueList { get; set; } = new List<Types.ProductAttributeValue>();
            }

            public class ExemptionOfIdentifierCode
            {
                /// <summary>
                /// 获取或设置 GTIN 豁免原因类型。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("exemption_reason")]
                [System.Text.Json.Serialization.JsonPropertyName("exemption_reason")]
                public int? ExemptionReasonType { get; set; }
            }
        }

        /// <summary>
        /// 获取或设置外部商品 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("outer_product_id")]
        [System.Text.Json.Serialization.JsonPropertyName("outer_product_id")]
        public string? OuterProductId { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置商品名称。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("product_name")]
        [System.Text.Json.Serialization.JsonPropertyName("product_name")]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置商品描述。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("description")]
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// 获取或设置分类 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("category_id")]
        [System.Text.Json.Serialization.JsonPropertyName("category_id")]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Common.NumericalStringConverter))]
        public string CategoryId { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置品牌 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("brand_id")]
        [System.Text.Json.Serialization.JsonPropertyName("brand_id")]
        public string? BrandId { get; set; }

        /// <summary>
        /// 获取或设置图片列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("images")]
        [System.Text.Json.Serialization.JsonPropertyName("images")]
        public IList<Types.Image>? ImageList { get; set; }

        /// <summary>
        /// 获取或设置视频信息。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("product_video")]
        [System.Text.Json.Serialization.JsonPropertyName("product_video")]
        public Types.Video? Video { get; set; }

        /// <summary>
        /// 获取或设置质保期 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("warranty_period")]
        [System.Text.Json.Serialization.JsonPropertyName("warranty_period")]
        public int? WarrantyPeriodId { get; set; }

        /// <summary>
        /// 获取或设置质保政策。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("warranty_policy")]
        [System.Text.Json.Serialization.JsonPropertyName("warranty_policy")]
        public string? WarrantyPolicy { get; set; }

        /// <summary>
        /// 获取或设置包装长度（单位：厘米）。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("package_length")]
        [System.Text.Json.Serialization.JsonPropertyName("package_length")]
        public int? PackageLength { get; set; }

        /// <summary>
        /// 获取或设置包装宽度（单位：厘米）。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("package_width")]
        [System.Text.Json.Serialization.JsonPropertyName("package_width")]
        public int? PackageWidth { get; set; }

        /// <summary>
        /// 获取或设置包装高度（单位：厘米）。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("package_height")]
        [System.Text.Json.Serialization.JsonPropertyName("package_height")]
        public int? PackageHeight { get; set; }

        /// <summary>
        /// 获取或设置包装重量（单位：千克）。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("package_weight")]
        [System.Text.Json.Serialization.JsonPropertyName("package_weight")]
        public int? PackageWeight { get; set; }

        /// <summary>
        /// 获取或设置包装尺寸单位。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("package_dimension_unit")]
        [System.Text.Json.Serialization.JsonPropertyName("package_dimension_unit")]
        public string? PackageDimensionUnit { get; set; }

        /// <summary>
        /// 获取或设置尺码表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("size_chart")]
        [System.Text.Json.Serialization.JsonPropertyName("size_chart")]
        public Types.SizeChart? SizeChart { get; set; }

        /// <summary>
        /// 获取或设置是否开启货到付款。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("is_cod_open")]
        [System.Text.Json.Serialization.JsonPropertyName("is_cod_open")]
        public bool IsCashOnDeliveryOpen { get; set; }

        /// <summary>
        /// 获取或设置配送服务 ID 列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("delivery_service_ids")]
        [System.Text.Json.Serialization.JsonPropertyName("delivery_service_ids")]
        public IList<string>? DeliveryServiceIdList { get; set; }

        /// <summary>
        /// 获取或设置 SKU 列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("skus")]
        [System.Text.Json.Serialization.JsonPropertyName("skus")]
        public IList<Types.SKU>? SKUList { get; set; }

        /// <summary>
        /// 获取或设置商品认证列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("product_certifications")]
        [System.Text.Json.Serialization.JsonPropertyName("product_certifications")]
        public IList<Types.ProductCertification>? ProductCertificationList { get; set; }

        /// <summary>
        /// 获取或设置商品属性列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("product_attributes")]
        [System.Text.Json.Serialization.JsonPropertyName("product_attributes")]
        public IList<Types.ProductAttribute>? ProductAttributeList { get; set; }

        /// <summary>
        /// 获取或设置产品标识码豁免信息。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("exemption_of_identifier_code")]
        [System.Text.Json.Serialization.JsonPropertyName("exemption_of_identifier_code")]
        public Types.ExemptionOfIdentifierCode? ExemptionOfIdentifierCode { get; set; }
    }
}
