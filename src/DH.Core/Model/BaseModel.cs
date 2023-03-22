using System.Xml.Serialization;

namespace DH.Model;

/// <summary>
/// 表示基本模型
/// </summary>
public partial record BaseModel {
    #region 实例化

    /// <summary>
    /// 实例化
    /// </summary>
    public BaseModel()
    {
        CustomProperties = new Dictionary<string, string>();
        PostInitialize();
    }

    #endregion

    #region 方法

    /// <summary>
    /// 对模型初始化执行其他操作
    /// </summary>
    /// <remarks>开发人员可以在自定义分部类中重写此方法，以便向构造函数添加一些自定义初始化代码</remarks>
    protected virtual void PostInitialize()
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// 获取或设置属性以存储模型的任何自定义值
    /// </summary>
    [XmlIgnore]
    public Dictionary<string, string> CustomProperties { get; set; }

    #endregion

}