using DH.Entity;

namespace DH.Services.Media
{
    /// <summary>
    /// 图片服务
    /// </summary>
    public partial class PictureService : IPictureService
    {
        #region Fields


        #endregion

        #region Ctor


        #endregion

        #region CRUD methods

        /// <summary>
        /// 获取一个值，该值指示图像是否应存储在数据库中。
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        public virtual bool IsStoreInDb()
        {
            return Setting.GetSettingByKey("Media.Images.StoreInDB", true);
        }

        #endregion
    }
}
