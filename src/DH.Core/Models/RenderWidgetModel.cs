﻿namespace DH.Models;

public partial class RenderWidgetModel : BaseDHModel {
    public string WidgetViewComponentName { get; set; }

    public object WidgetViewComponentArguments { get; set; }
}