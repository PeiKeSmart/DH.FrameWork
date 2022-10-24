namespace DH.Web.Framework.Mvc.ModelBinding
{
    /// <summary>
    /// 表示自定义模型属性
    /// </summary>
    public partial interface IModelAttribute
    {
        /// <summary>
        /// 获取属性的名称
        /// </summary>
        string Name { get; }
    }
}
