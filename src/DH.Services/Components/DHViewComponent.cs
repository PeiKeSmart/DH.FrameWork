using DH.Core.Events;
using DH.Core.Infrastructure;
using DH.Models;
using DH.Services.Events;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

using Pek;

using XCode.Membership;

namespace DH.Services.Components;

/// <summary>
/// ViewComponent的基类
/// </summary>
public abstract partial class DHViewComponent : ViewComponent
{
    private void PublishModelPrepared<TModel>(TModel model)
    {
        // 组件不是控制器生命周期的一部分
        // 因此，我们不能再使用Action Filters来拦截返回的模型
        // 正如我们在/DH.Web.Framework/Mvc/Filters/PublishModelEventsAttribute.cs中所做的那样表示控制器

        // 模型准备事件
        if (model is BaseDHModel)
        {
            var eventPublisher = EngineContext.Current.Resolve<IEventPublisher>();

            // 我们将所有模型的ModelPrepared事件发布为BaseDHModel
            // 因此需要实现IConsumer<ModelPrepared<BaseDHModel>>接口来处理此事件
            eventPublisher.ModelPreparedAsync(model as BaseDHModel).Wait();
        }

        if (model is IEnumerable<BaseDHModel> modelCollection)
        {
            var eventPublisher = EngineContext.Current.Resolve<IEventPublisher>();

            // 我们将用于收集的ModelPrepared事件发布为IEnumerable<BaseDHModel>
            // 因此，您需要实现IConsumer<ModelPrepared<IEnumerable＜BaseDHModel>>接口来处理此事件
            eventPublisher.ModelPreparedAsync(modelCollection).Wait();
        }
    }

    /// <summary>
    /// 返回一个结果，该结果将呈现名称为<paramref name="viewName"/>的部分视图.
    /// </summary>
    /// <param name="viewName">要渲染的部分视图的名称.</param>
    /// <param name="model">视图的模型对象.</param>
    /// <returns>A <see cref="ViewViewComponentResult"/>.</returns>
    public new ViewViewComponentResult View<TModel>(string viewName, TModel model)
    {
        PublishModelPrepared(model);

        // 调用基方法
        return base.View<TModel>(viewName, model);
    }

    /// <summary>
    /// 返回将呈现局部视图的结果
    /// </summary>
    /// <param name="model">视图的模型对象.</param>
    /// <returns>A <see cref="ViewViewComponentResult"/>.</returns>
    public new ViewViewComponentResult View<TModel>(TModel model)
    {
        PublishModelPrepared(model);

        // 调用基方法
        return base.View<TModel>(model);
    }

    /// <summary>
    ///  返回将呈现名称为viewName的部分视图的结果
    /// </summary>
    /// <param name="viewName">要渲染的局部视图的名称.</param>
    /// <returns>A <see cref="ViewViewComponentResult"/>.</returns>
    public new ViewViewComponentResult View(string viewName)
    {
        // 调用基方法
        return base.View(viewName);
    }

    /// <summary>
    /// 获取菜单树
    /// </summary>
    /// <returns></returns>
    public IList<MenuTree> GetMenu()
    {
        var _provider = EngineContext.Current.Resolve<IManageProvider>();

        var user = _provider.Current as IUser;

        var fact = ManageProvider.Menu;
        var menus = fact.Root.Childs;
        if (user?.Role != null)
        {
            menus = fact.GetMySubMenus(fact.Root.ID, user, true);
        }

        // 如果顶级只有一层，并且至少有三级目录，则提升一级
        if (menus.Count == 1 && menus[0].Childs.All(m => m.Childs.Count > 0)) { menus = menus[0].Childs; }

        menus = menus.Where(m => m.Visible).ToList();

        var menuTree = MenuTree.GetMenuTree(pMenuTree =>
        {
            return fact.GetMySubMenus(pMenuTree.ID, user, true).Where(m => m.Visible).ToList();
        }, list =>
        {

            var menuList = (from menu in list
                                // where m.Visible
                            select new MenuTree
                            {
                                ID = menu.ID,
                                Name = menu.Name,
                                DisplayName = menu.DisplayName ?? menu.Name,
                                FullName = menu.FullName,
                                Url = Url.Content(menu.Url),
                                Icon = menu.Icon,
                                Class = "",
                                Visible = menu.Visible,
                                NewWindow = menu.NewWindow,
                                ParentID = menu.ParentID,
                                Permissions = menu.Permissions
                            }).ToList();
            return menuList.Count > 0 ? menuList : null;
        }, menus.ToList());

        //var childs = menuTree[0].Children;
        return menuTree;
    }
}