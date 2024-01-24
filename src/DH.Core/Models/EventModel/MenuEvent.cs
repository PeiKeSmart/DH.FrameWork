using XCode.Membership;

namespace DH.Models.EventModel;

/// <summary>
/// 菜单消费者事件
/// </summary>
public class MenuEvent
{
    public MenuEvent(Menu menu, String expand)
    {
        Menu = menu;
        Expand = expand;
    }

    public Menu Menu { get; }

    public String Expand { get; }
}