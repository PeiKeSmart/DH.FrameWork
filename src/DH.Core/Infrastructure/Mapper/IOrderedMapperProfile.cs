namespace DH.Core.Infrastructure.Mapper
{
    /// <summary>
    /// 映射器配置文件注册器接口
    /// </summary>
    public interface IOrderedMapperProfile
    {
        /// <summary>
        /// 获取此配置实现的顺序
        /// </summary>
        int Order { get; }
    }
}
