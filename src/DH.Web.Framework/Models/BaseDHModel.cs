using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.Xml.Serialization;

namespace DH.Web.Framework.Models
{
    /// <summary>
    /// 表示DH框架基本模型
    /// </summary>
    public partial record BaseDHModel
    {
        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        [Obsolete]
        public BaseDHModel()
        {
            CustomProperties = new Dictionary<string, object>();
            PostInitialize();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 执行绑定模型的其他操作
        /// </summary>
        /// <param name="bindingContext">模型绑定上下文</param>
        /// <remarks>开发人员可以在自定义分部类中重写此方法，以便添加一些自定义模型绑定</remarks>
        public virtual void BindModel(ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// 执行模型初始化的其他操作
        /// </summary>
        /// <remarks>开发人员可以在自定义分部类中重写此方法，以便向构造函数添加一些自定义初始化代码</remarks>
        protected virtual void PostInitialize()
        {
        }

        #endregion

        #region Properties

        //// 如果IFormCollection被传递给控制器方法，MVC将抑制进一步的验证。这就是为什么我们将其添加到模型中
        //[XmlIgnore]
        //public IFormCollection Form { get; set; }

        /// <summary>
        /// 获取或设置属性以存储模型的任何自定义值
        /// </summary>
        [XmlIgnore]
        [Obsolete]
        public Dictionary<string, object> CustomProperties { get; set; }

        #endregion

    }
}
