namespace DH.Core.Domain.Customers;

/// <summary>
/// 表示与客户数据相关的默认值
/// </summary>
public static partial class DHCustomerDefaults
{
    #region System customer roles

    /// <summary>
    /// 获取"管理员"客户角色的系统名称
    /// </summary>
    public static string AdministratorsRoleName => "Administrators";

    /// <summary>
    /// 获取'论坛主持人'客户角色的系统名称
    /// </summary>
    public static string ForumModeratorsRoleName => "ForumModerators";

    /// <summary>
    /// 获取'registered'客户角色的系统名称
    /// </summary>
    public static string RegisteredRoleName => "Registered";

    /// <summary>
    /// 获取'来宾'客户角色的系统名称
    /// </summary>
    public static string GuestsRoleName => "Guests";

    /// <summary>
    /// 获取'供应商'客户角色的系统名称
    /// </summary>
    public static string VendorsRoleName => "Vendors";

    #endregion

    #region System customers

    /// <summary>
    /// 获取'搜索引擎'客户对象的系统名称
    /// </summary>
    public static string SearchEngineCustomerName => "SearchEngine";

    /// <summary>
    /// 获取'后台任务'客户对象的系统名称
    /// </summary>
    public static string BackgroundTaskCustomerName => "BackgroundTask";

    #endregion

    #region Customer attributes

    /// <summary>
    /// 获取用于存储'DiscountCouponCode'值的泛型属性的名称
    /// </summary>
    public static string DiscountCouponCodeAttribute => "DiscountCouponCode";

    /// <summary>
    /// 获取用于存储'GiftCardCouponCodes'值的泛型属性的名称
    /// </summary>
    public static string GiftCardCouponCodesAttribute => "GiftCardCouponCodes";

    /// <summary>
    /// 获取用于存储'AvatarPictureId'值的泛型属性的名称
    /// </summary>
    public static string AvatarPictureIdAttribute => "AvatarPictureId";

    /// <summary>
    /// 获取用于存储'ForumPostCount'值的泛型属性的名称
    /// </summary>
    public static string ForumPostCountAttribute => "ForumPostCount";

    /// <summary>
    /// 获取用于存储'Signature'值的泛型属性的名称
    /// </summary>
    public static string SignatureAttribute => "Signature";

    /// <summary>
    /// 获取用于存储'PasswordRecoveryToken'值的泛型属性的名称
    /// </summary>
    public static string PasswordRecoveryTokenAttribute => "PasswordRecoveryToken";

    /// <summary>
    /// 获取用于存储'PasswordRecoveryTokenDateGenerated'值的泛型属性的名称
    /// </summary>
    public static string PasswordRecoveryTokenDateGeneratedAttribute => "PasswordRecoveryTokenDateGenerated";

    /// <summary>
    /// 获取用于存储'AccountActivationToken'值的泛型属性的名称
    /// </summary>
    public static string AccountActivationTokenAttribute => "AccountActivationToken";

    /// <summary>
    /// 获取用于存储'EmailRevalidationToken'值的泛型属性的名称
    /// </summary>
    public static string EmailRevalidationTokenAttribute => "EmailRevalidationToken";

    /// <summary>
    /// 获取用于存储'LastVisitedPage'值的泛型属性的名称
    /// </summary>
    public static string LastVisitedPageAttribute => "LastVisitedPage";

    /// <summary>
    /// 获取用于存储'ImpersonatedCustomerId'值的泛型属性的名称
    /// </summary>
    public static string ImpersonatedCustomerIdAttribute => "ImpersonatedCustomerId";

    /// <summary>
    /// 获取用于存储'AdminAreaStoreScopeConfiguration'值的泛型属性的名称
    /// </summary>
    public static string AdminAreaStoreScopeConfigurationAttribute => "AdminAreaStoreScopeConfiguration";

    /// <summary>
    /// 获取用于存储'SelectedPaymentMethod'值的泛型属性的名称
    /// </summary>
    public static string SelectedPaymentMethodAttribute => "SelectedPaymentMethod";

    /// <summary>
    /// 获取用于存储'SelectedShippingOption'值的泛型属性的名称
    /// </summary>
    public static string SelectedShippingOptionAttribute => "SelectedShippingOption";

    /// <summary>
    /// 获取用于存储'SelectedPickupPoint'值的泛型属性的名称
    /// </summary>
    public static string SelectedPickupPointAttribute => "SelectedPickupPoint";

    /// <summary>
    /// 获取用于存储'CheckoutAttributes'值的泛型属性的名称
    /// </summary>
    public static string CheckoutAttributes => "CheckoutAttributes";

    /// <summary>
    /// 获取用于存储'OfferedShippingOptions'值的泛型属性的名称
    /// </summary>
    public static string OfferedShippingOptionsAttribute => "OfferedShippingOptions";

    /// <summary>
    /// 获取用于存储'LastContinueShoppingPage'值的泛型属性的名称
    /// </summary>
    public static string LastContinueShoppingPageAttribute => "LastContinueShoppingPage";

    /// <summary>
    /// 获取用于存储'NotifiedAboutNewPrivateMessages'值的泛型属性的名称
    /// </summary>
    public static string NotifiedAboutNewPrivateMessagesAttribute => "NotifiedAboutNewPrivateMessages";

    /// <summary>
    /// 获取用于存储'WorkingThemeName'值的泛型属性的名称
    /// </summary>
    public static string WorkingThemeNameAttribute => "WorkingThemeName";

    /// <summary>
    /// 获取用于存储'UseRewardPointsDuringCheckout'值的泛型属性的名称
    /// </summary>
    public static string UseRewardPointsDuringCheckoutAttribute => "UseRewardPointsDuringCheckout";

    /// <summary>
    /// 获取用于存储'EuCookieLawAccepted'值的泛型属性的名称
    /// </summary>
    public static string EuCookieLawAcceptedAttribute => "EuCookieLaw.Accepted";

    /// <summary>
    /// 获取用于存储'SelectedMultiFactorAuthProvider'值的泛型属性的名称
    /// </summary>
    public static string SelectedMultiFactorAuthenticationProviderAttribute => "SelectedMultiFactorAuthProvider";

    /// <summary>
    /// 获取会话密钥的名称
    /// </summary>
    public static string CustomerMultiFactorAuthenticationInfo => "CustomerMultiFactorAuthenticationInfo";

    /// <summary>
    /// 获取用于存储'HideConfigurationSteps'值的泛型属性的名称
    /// </summary>
    public static string HideConfigurationStepsAttribute => "HideConfigurationSteps";

    /// <summary>
    /// 获取用于存储'CloseConfigurationSteps'值的泛型属性的名称
    /// </summary>
    public static string CloseConfigurationStepsAttribute => "CloseConfigurationSteps";

    #endregion
}
