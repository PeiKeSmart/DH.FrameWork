using System.Xml;

using DH.Core;
using DH.Core.Infrastructure;
using DH.Services.Common;

using NewLife.Log;

using Pek.Webs;

using XCode.Membership;

namespace DH.Services.Plugins.Marketplace;

/// <summary>
/// 表示官方提要管理器（DH.Web.FrameWrok市场的插件）
/// </summary>
public partial class OfficialFeedManager
{
    #region Fields

    private readonly DHHttpClient _dhHttpClient;

    #endregion

    #region Ctor

    public OfficialFeedManager(DHHttpClient nopHttpClient)
    {
        _dhHttpClient = nopHttpClient;
    }

    #endregion

    #region Utilities

    /// <summary>
    /// 获取元素值
    /// </summary>
    /// <param name="node">XML节点</param>
    /// <param name="elementName">元素名称</param>
    /// <returns>值（文本）</returns>
    protected virtual string GetElementValue(XmlNode node, string elementName)
    {
        return node?.SelectSingleNode(elementName)?.InnerText;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 获取可用的市场扩展类别
    /// </summary>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含结果
    /// </returns>
    public virtual async Task<IList<OfficialFeedCategory>> GetCategoriesAsync()
    {
        // 加载XML
        var xml = new XmlDocument();
        try
        {
            xml.LoadXml(await _dhHttpClient.GetExtensionsCategoriesAsync());
        }
        catch (Exception ex)
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();

            // 获取当前客户
            var currentCustomer = workContext.CurrentCustomer;

            var message = "No access to the list of plugins. Website www.yuanrenyi.com is not available.";
            XTrace.Log.Error(message);
            XTrace.WriteException(ex);
            LogProvider.Provider?.WriteLog("插件", "错误", false, message, currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
        }

        // 从XML中获取类别列表
        return xml.SelectNodes(@"//categories/category").Cast<XmlNode>().Select(node => new OfficialFeedCategory
        {
            Id = int.Parse(GetElementValue(node, @"id")),
            ParentCategoryId = int.Parse(GetElementValue(node, @"parentCategoryId")),
            Name = GetElementValue(node, @"name")
        }).ToList();
    }

    /// <summary>
    /// 获取可用版本的市场扩展
    /// </summary>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含结果
    /// </returns>
    public virtual async Task<IList<OfficialFeedVersion>> GetVersionsAsync()
    {
        //load XML
        var xml = new XmlDocument();
        try
        {
            xml.LoadXml(await _dhHttpClient.GetExtensionsVersionsAsync());
        }
        catch (Exception ex)
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();

            // 获取当前客户
            var currentCustomer = workContext.CurrentCustomer;

            var message = "No access to the list of plugins. Website www.nopcommerce.com is not available.";
            XTrace.Log.Error(message);
            XTrace.WriteException(ex);
            LogProvider.Provider?.WriteLog("插件", "错误", false, message, currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
        }

        // 从XML获取版本列表
        return xml.SelectNodes(@"//versions/version").Cast<XmlNode>().Select(node => new OfficialFeedVersion
        {
            Id = int.Parse(GetElementValue(node, @"id")),
            Name = GetElementValue(node, @"name")
        }).ToList();
    }

    #endregion
}
