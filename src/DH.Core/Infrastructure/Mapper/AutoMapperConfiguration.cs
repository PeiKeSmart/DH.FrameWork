using AutoMapper;

namespace DH.Core.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// 映射器
        /// </summary>
        public static IMapper Mapper { get; private set; }

        /// <summary>
        /// 映射器配置
        /// </summary>
        public static MapperConfiguration MapperConfiguration { get; private set; }

        /// <summary>
        /// 初始化映射器
        /// </summary>
        /// <param name="config">Mapper configuration</param>
        public static void Init(MapperConfiguration config)
        {
            MapperConfiguration = config;
            Mapper = config.CreateMapper();
        }
    }
}
