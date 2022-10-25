using DH.Core.Caching;
using DH.Core.Domain.Localization;
using DH.Entity;

namespace DH.Services.Seo
{
    /// <summary>
    /// 提供有关URL记录的信息
    /// </summary>
    public partial class UrlRecordService : IUrlRecordService
    {
        private static readonly object _lock = new();

        private readonly LocalizationSettings _localizationSettings;

        public UrlRecordService(LocalizationSettings localizationSettings)
        {
            _localizationSettings = localizationSettings;
        }

        /// <summary>
        /// 查找slug
        /// </summary>
        /// <param name="entityId">实体标识符</param>
        /// <param name="entityName">实体名称</param>
        /// <param name="languageId">语言标识符</param>
        /// <returns>
        /// 任务结果包含找到的slug
        /// </returns>
        public virtual string GetActiveSlug(int entityId, string entityName, int languageId)
        {
            return UrlRecord.GetAllUrlRecords(entityId, entityName, languageId).FirstOrDefault().Slug; 
        }

        /// <summary>
        /// Find URL record
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>
        /// The task result contains the found URL record
        /// </returns>
        public virtual UrlRecord GetBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return null;

            return UrlRecord.FindAllBySlug(slug).FirstOrDefault();
        }

    }
}
