using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Core.Domain.Media;

/// <summary>媒体设置</summary>
[DisplayName("媒体设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("MediaSettings")]
public class MediaSettings : Config<MediaSettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static MediaSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "Media" };
    #endregion

    /// <summary>
    /// 客户头像的图片大小（如果启用）
    /// </summary>
    public int AvatarPictureSize { get; set; }

    /// <summary>
    /// 目录页面（例如类别详细信息页面）上显示的产品图片拇指的图片大小
    /// </summary>
    public int ProductThumbPictureSize { get; set; }

    /// <summary>
    /// 产品详细信息页面上显示的主要产品图片的图片大小
    /// </summary>
    public int ProductDetailsPictureSize { get; set; }

    /// <summary>
    /// 产品详细信息页面上显示的产品图片拇指的图片大小
    /// </summary>
    public int ProductThumbPictureSizeOnProductDetailsPage { get; set; }

    /// <summary>
    /// 关联产品图片的图片大小
    /// </summary>
    public int AssociatedProductPictureSize { get; set; }

    /// <summary>
    /// 类别图片的图片大小
    /// </summary>
    public int CategoryThumbPictureSize { get; set; }

    /// <summary>
    /// 制造商图片的图片大小
    /// </summary>
    public int ManufacturerThumbPictureSize { get; set; }

    /// <summary>
    /// 供应商图片的图片大小
    /// </summary>
    public int VendorThumbPictureSize { get; set; }

    /// <summary>
    /// 购物车页面上产品图片的图片大小
    /// </summary>
    public int CartThumbPictureSize { get; set; }

    /// <summary>
    /// 订单详细信息页面上产品图片的图片大小
    /// </summary>
    public int OrderThumbPictureSize { get; set; }

    /// <summary>
    /// 迷你运输车箱产品图片的图片大小
    /// </summary>
    public int MiniCartThumbPictureSize { get; set; }

    /// <summary>
    /// 用于自动完成搜索框的产品图片的图片大小
    /// </summary>
    public int AutoCompleteSearchThumbPictureSize { get; set; }

    /// <summary>
    /// 产品详细信息页面上图像方块的图片大小（与“图像方块”属性类型一起使用
    /// </summary>
    public int ImageSquarePictureSize { get; set; }

    /// <summary>
    /// 指示是否启用图片缩放的值
    /// </summary>
    public bool DefaultPictureZoomEnabled { get; set; }

    /// <summary>
    /// A value indicating whether to allow uploading of SVG files in admin area
    /// </summary>
    public bool AllowSVGUploads { get; set; }

    /// <summary>
    /// Maximum allowed picture size. If a larger picture is uploaded, then it'll be resized
    /// </summary>
    public int MaximumImageSize { get; set; }

    /// <summary>
    /// Gets or sets a default quality used for image generation
    /// </summary>
    public int DefaultImageQuality { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether single (/content/images/thumbs/) or multiple (/content/images/thumbs/001/ and /content/images/thumbs/002/) directories will used for picture thumbs
    /// </summary>
    public bool MultipleThumbDirectories { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether we should use fast HASHBYTES (hash sum) database function to compare pictures when importing products
    /// </summary>
    public bool ImportProductImagesUsingHash { get; set; }

    /// <summary>
    /// Gets or sets Azure CacheControl header (e.g. "max-age=3600, public")
    /// </summary>
    /// <remarks>
    /// max-age=[seconds]     — specifies the maximum amount of time that a representation will be considered fresh. Similar to Expires, this directive is relative to the time of the request, rather than absolute. [seconds] is the number of seconds from the time of the request you wish the representation to be fresh for.
    /// s-maxage=[seconds]    — similar to max-age, except that it only applies to shared (e.g., proxy) caches.
    /// public                — marks authenticated responses as cacheable; normally, if HTTP authentication is required, responses are automatically private.
    /// private               — allows caches that are specific to one user (e.g., in a browser) to store the response; shared caches (e.g., in a proxy) may not.
    /// no-cache              — forces caches to submit the request to the origin server for validation before releasing a cached copy, every time. This is useful to assure that authentication is respected (in combination with public), or to maintain rigid freshness, without sacrificing all of the benefits of caching.
    /// no-store              — instructs caches not to keep a copy of the representation under any conditions.
    /// must-revalidate       — tells caches that they must obey any freshness information you give them about a representation. HTTP allows caches to serve stale representations under special conditions; by specifying this header, you’re telling the cache that you want it to strictly follow your rules.
    /// proxy-revalidate      — similar to must-revalidate, except that it only applies to proxy caches.
    /// </remarks>
    public string AzureCacheControlHeader { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether need to use absolute pictures path
    /// </summary>
    public bool UseAbsoluteImagePath { get; set; }

    /// <summary>
    /// Gets or sets the value to specify a policy list for embedded content
    /// </summary>
    public string VideoIframeAllow { get; set; }

    /// <summary>
    /// Gets or sets the width of the frame in CSS pixels
    /// </summary>
    public int VideoIframeWidth { get; set; }

    /// <summary>
    /// Gets or sets the height of the frame in CSS pixels
    /// </summary>
    public int VideoIframeHeight { get; set; }

    /// <summary>
    /// Gets or sets the product default image id. If 0, then wwwroot/images/default-image.png will be used
    /// </summary>
    public int ProductDefaultImageId { get; set; }
}
