using Pek;

namespace DH.Models;

public partial class RenderWidgetModel : BasePekModel {
    public string WidgetViewComponentName { get; set; }

    public object WidgetViewComponentArguments { get; set; }
}
