using DH.Entity;

namespace DH.Services.Seo
{
    /// <summary>
    /// 提供有关URL记录的信息
    /// </summary>
    public partial interface IUrlRecordService
    {
        /// <summary>
        /// Find URL record
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>
        /// The task result contains the found URL record
        /// </returns>
        UrlRecord GetBySlug(string slug);

        /// <summary>
        /// Find slug
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="entityName">Entity name</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>
        /// The task result contains the found slug
        /// </returns>
        string GetActiveSlug(int entityId, string entityName, int languageId);
    }
}
