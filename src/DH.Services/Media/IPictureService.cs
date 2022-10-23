using Microsoft.AspNetCore.Http;

namespace DH.Services.Media
{
    /// <summary>
    /// 图片服务接口
    /// </summary>
    public partial interface IPictureService
    {
        /// <summary>
        /// 获取或设置一个值，该值指示图像是否应存储在数据库中。
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        bool IsStoreInDb();
    }
}
