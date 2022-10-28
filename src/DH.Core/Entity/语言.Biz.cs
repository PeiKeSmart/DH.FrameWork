using NewLife.Data;
using NewLife.Log;

using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using XCode;

namespace DH.Entity
{
    public partial class Language : Entity<Language>
    {
        #region 对象操作
        static Language()
        {
            // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(nameof(DefaultCurrencyId));

            // 过滤器 UserModule、TimeModule、IPModule
        }

        /// <summary>验证并修补数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 建议先调用基类方法，基类方法会做一些统一处理
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行修正
        }

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            if (Meta.Session.Count > 0) return;

            if (XTrace.Debug) XTrace.WriteLine("开始初始化Language[语言]数据……");

            var list = new List<Language>();

            var entity = new Language
            {
                Name = "中文(简体)",
                DisplayName = "中文(简体)",
                EnglishName = "Chinese (Simplified, PRC)",
                LanguageCulture = "zh-CN",
                UniqueSeoCode = "cn",
                Flag = "~/images/culture-flags/zh-CN.gif",
                Lcid = 2052,
                Status = true,
                DisplayOrder = 0,
                IsDefault = 1,
                LangAbbreviation = "zh"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1028,
                Name = "中文(繁体)",
                LanguageCulture = "zh-TW",
                EnglishName = "Chinese (Traditional, Taiwan)",
                DisplayName = "中文(繁體)",
                UniqueSeoCode = "tw",
                Flag = "~/images/culture-flags/zh-TW.gif",
                Status = false,
                DisplayOrder = 1,
                IsDefault = 0,
                LangAbbreviation = "cht"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1033,
                Name = "英语 (美式)",
                LanguageCulture = "en-US",
                EnglishName = "English (United States)",
                DisplayName = "English (US)",
                UniqueSeoCode = "us",
                Flag = "~/images/culture-flags/en-US.gif",
                Status = false,
                DisplayOrder = 2,
                IsDefault = 0,
                LangAbbreviation = "en"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 2057,
                Name = "英语 (英式)",
                LanguageCulture = "en-GB",
                EnglishName = "English (United Kingdom)",
                DisplayName = "English",
                UniqueSeoCode = "en",
                Flag = "~/images/culture-flags/en-GB.gif",
                Status = true,
                DisplayOrder = 3,
                IsDefault = 0,
                LangAbbreviation = "en"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1046,
                Name = "葡萄牙语 (巴西)",
                LanguageCulture = "pt-BR",
                EnglishName = "Portuguese (Brazil)",
                DisplayName = "Português (Brasil)",
                UniqueSeoCode = "br",
                Flag = "~/images/culture-flags/pt-BR.gif",
                Status = false,
                DisplayOrder = 4,
                IsDefault = 0,
                LangAbbreviation = "pt"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1049,
                Name = "俄语 (俄罗斯)",
                LanguageCulture = "ru-RU",
                EnglishName = "Russian (Russia)",
                DisplayName = "Pycckий",
                UniqueSeoCode = "ru",
                Flag = "~/images/culture-flags/ru-RU.gif",
                Status = false,
                DisplayOrder = 5,
                IsDefault = 0,
                LangAbbreviation = "ru"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1041,
                Name = "日语",
                LanguageCulture = "ja-JP",
                EnglishName = "Japanese (Japan)",
                DisplayName = "日本語",
                UniqueSeoCode = "jp",
                Flag = "~/images/culture-flags/ja-JP.gif",
                Status = false,
                DisplayOrder = 6,
                IsDefault = 0,
                LangAbbreviation = "jp"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1066,
                Name = "越南语 (越南)",
                LanguageCulture = "vi-VN",
                EnglishName = "Vietnamese (Vietnam)",
                DisplayName = "Tiếng Việt",
                UniqueSeoCode = "vn",
                Flag = "~/images/culture-flags/vi-VN.gif",
                Status = false,
                DisplayOrder = 7,
                IsDefault = 0,
                LangAbbreviation = "vie"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1036,
                Name = "法语 (法国)",
                LanguageCulture = "fr-FR",
                EnglishName = "French (France)",
                DisplayName = "Français",
                UniqueSeoCode = "fr",
                Flag = "~/images/culture-flags/fr-FR.gif",
                Status = false,
                DisplayOrder = 8,
                IsDefault = 0,
                LangAbbreviation = "fra"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1045,
                Name = "波兰语 (波兰)",
                LanguageCulture = "pl-PL",
                EnglishName = "Polish (Poland)",
                DisplayName = "Polski",
                UniqueSeoCode = "pl",
                Flag = "~/images/culture-flags/pl-PL.gif",
                Status = false,
                DisplayOrder = 9,
                IsDefault = 0,
                LangAbbreviation = "pl"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1040,
                Name = "意大利语 (意大利)",
                LanguageCulture = "it-IT",
                EnglishName = "Italian (Italy)",
                DisplayName = "Italiano",
                UniqueSeoCode = "it",
                Flag = "~/images/culture-flags/it-IT.gif",
                Status = false,
                DisplayOrder = 10,
                IsDefault = 0,
                LangAbbreviation = "it"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1055,
                Name = "土耳其语 (土耳其)",
                LanguageCulture = "tr-TR",
                EnglishName = "Turkish (Turkey)",
                DisplayName = "Türkçe",
                UniqueSeoCode = "tr",
                Flag = "~/images/culture-flags/tr-TR.gif",
                Status = false,
                DisplayOrder = 11,
                IsDefault = 0,
                LangAbbreviation = "tr"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1042,
                Name = "韩文 (韩国)",
                LanguageCulture = "ko-KR",
                EnglishName = "Korean (Korea)",
                DisplayName = "한국어",
                UniqueSeoCode = "kr",
                Flag = "~/images/culture-flags/ko-KR.gif",
                Status = false,
                DisplayOrder = 12,
                IsDefault = 0,
                LangAbbreviation = "kor"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1054,
                Name = "泰语 (泰国)",
                LanguageCulture = "th-TH",
                EnglishName = "Thai (Thailand)",
                DisplayName = "ไทย",
                UniqueSeoCode = "th",
                Flag = "~/images/culture-flags/th-TH.gif",
                Status = false,
                DisplayOrder = 13,
                IsDefault = 0,
                LangAbbreviation = "th"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1031,
                Name = "德语 (德国)",
                LanguageCulture = "de-DE",
                EnglishName = "German (Germany)",
                DisplayName = "Deutsch",
                UniqueSeoCode = "de",
                Flag = "~/images/culture-flags/de-DE.gif",
                Status = false,
                DisplayOrder = 14,
                IsDefault = 0,
                LangAbbreviation = "de"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 2058,
                Name = "西班牙语 (墨西哥)",
                LanguageCulture = "es-MX",
                EnglishName = "Spanish (Mexico)",
                DisplayName = "Español (Latam)",
                UniqueSeoCode = "latam",
                Flag = "~/images/culture-flags/es-MX.gif",
                Status = false,
                DisplayOrder = 15,
                IsDefault = 0,
                LangAbbreviation = "spa"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 3082,
                Name = "西班牙语 (西班牙，南美洲)",
                LanguageCulture = "es-ES",
                EnglishName = "Spanish (Spain, International Sort)",
                DisplayName = "Español",
                UniqueSeoCode = "es",
                Flag = "~/images/culture-flags/es-ES.gif",
                Status = false,
                DisplayOrder = 16,
                IsDefault = 0,
                LangAbbreviation = "spa"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 3073,
                Name = "阿拉伯语 (阿拉伯语)",
                LanguageCulture = "ar-EG",
                EnglishName = "Arabic (Egypt)",
                DisplayName = "Arabic (Egypt)",
                UniqueSeoCode = "eg",
                Flag = "~/images/culture-flags/ar-EG.gif",
                Status = false,
                DisplayOrder = 17,
                IsDefault = 0,
                LangAbbreviation = "ara"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1029,
                Name = "捷克语 (捷克共和国)",
                LanguageCulture = "cs-CZ",
                EnglishName = "Czech (Czech Republic)",
                DisplayName = "Czech (Czech Republic)",
                UniqueSeoCode = "cz",
                Flag = "~/images/culture-flags/cs-CZ.gif",
                Status = false,
                DisplayOrder = 18,
                IsDefault = 0,
                LangAbbreviation = "cs"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 2055,
                Name = "德语 (瑞士)",
                LanguageCulture = "de-CH",
                EnglishName = "German (Switzerland)",
                DisplayName = "German (Switzerland)",
                UniqueSeoCode = "ch",
                Flag = "~/images/culture-flags/de-CH.gif",
                Status = false,
                DisplayOrder = 19,
                IsDefault = 0,
                LangAbbreviation = "de"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1030,
                Name = "丹麦语 (丹麦)",
                LanguageCulture = "da-DK",
                EnglishName = "Danish (Denmark)",
                DisplayName = "Danish (Denmark)",
                UniqueSeoCode = "dk",
                Flag = "~/images/culture-flags/da-DK.gif",
                Status = false,
                DisplayOrder = 20,
                IsDefault = 0,
                LangAbbreviation = "dan"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1035,
                Name = "芬兰语 (芬兰)",
                LanguageCulture = "fi-FI",
                EnglishName = "Finnish (Finland)",
                DisplayName = "Finnish (Finland)",
                UniqueSeoCode = "fi",
                Flag = "~/images/culture-flags/fi-FI.gif",
                Status = false,
                DisplayOrder = 21,
                IsDefault = 0,
                LangAbbreviation = "fin"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1032,
                Name = "希腊语 (希腊)",
                LanguageCulture = "el-GR",
                EnglishName = "Greek (Greece)",
                DisplayName = "Greek (Greece)",
                UniqueSeoCode = "gr",
                Flag = "~/images/culture-flags/el-GR.gif",
                Status = false,
                DisplayOrder = 22,
                IsDefault = 0,
                LangAbbreviation = "el"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1038,
                Name = "匈牙利语 (匈牙利)",
                LanguageCulture = "hu-HU",
                EnglishName = "Hungarian (Hungary)",
                DisplayName = "Hungarian (Hungary)",
                UniqueSeoCode = "hu",
                Flag = "~/images/culture-flags/hu-HU.gif",
                Status = false,
                DisplayOrder = 23,
                IsDefault = 0,
                LangAbbreviation = "hu"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1043,
                Name = "荷兰语 (荷兰)",
                LanguageCulture = "nl-NL",
                EnglishName = "Dutch (Netherlands)",
                DisplayName = "Dutch (Netherlands)",
                UniqueSeoCode = "nl",
                Flag = "~/images/culture-flags/nl-NL.gif",
                Status = false,
                DisplayOrder = 24,
                IsDefault = 0,
                LangAbbreviation = "nl"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 2068,
                Name = "挪威语 (挪威语)",
                LanguageCulture = "nn-NO",
                EnglishName = "Norwegian, Nynorsk (Norway)",
                DisplayName = "Norwegian, Nynorsk (Norway)",
                UniqueSeoCode = "no",
                Flag = "~/images/culture-flags/nn-NO.gif",
                Status = false,
                DisplayOrder = 25,
                IsDefault = 0,
                LangAbbreviation = "nor"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1048,
                Name = "罗马尼亚语 (罗马尼亚)",
                LanguageCulture = "ro-RO",
                EnglishName = "Romanian (Romania)",
                DisplayName = "Romanian (Romania)",
                UniqueSeoCode = "ro",
                Flag = "~/images/culture-flags/ro-RO.gif",
                Status = false,
                DisplayOrder = 26,
                IsDefault = 0,
                LangAbbreviation = "rom"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1053,
                Name = "瑞典语 (瑞典)",
                LanguageCulture = "sv-SE",
                EnglishName = "Swedish (Sweden)",
                DisplayName = "Swedish (Sweden)",
                UniqueSeoCode = "se",
                Flag = "~/images/culture-flags/sv-SE.gif",
                Status = false,
                DisplayOrder = 27,
                IsDefault = 0,
                LangAbbreviation = "swe"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 7177,
                Name = "英语 (南非)",
                LanguageCulture = "en-ZA",
                EnglishName = "English (South Africa)",
                DisplayName = "English (South Africa)",
                UniqueSeoCode = "za",
                Flag = "~/images/culture-flags/en-ZA.gif",
                Status = false,
                DisplayOrder = 28,
                IsDefault = 0,
                LangAbbreviation = "en"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 1057,
                Name = "印度尼西亚语 (印度尼西亚)",
                LanguageCulture = "id-ID",
                EnglishName = "Indonesian (Indonesia)",
                DisplayName = "Indonesian (Indonesia)",
                UniqueSeoCode = "id",
                Flag = "~/images/culture-flags/id-ID.gif",
                Status = false,
                DisplayOrder = 29,
                IsDefault = 0,
                LangAbbreviation = "id"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 3081,
                Name = "英语 (澳大利亚)",
                LanguageCulture = "en-AU",
                EnglishName = "English (Australia)",
                DisplayName = "English (Australia)",
                UniqueSeoCode = "au",
                Flag = "~/images/culture-flags/en-AU.gif",
                Status = false,
                DisplayOrder = 30,
                IsDefault = 0,
                LangAbbreviation = "en"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 2057,
                Name = "英语 (欧洲中东非洲)",
                LanguageCulture = "en-SG",
                EnglishName = "English (EMEA)",
                DisplayName = "English (EMEA)",
                UniqueSeoCode = "emea",
                Flag = "~/images/culture-flags/None.gif",
                Status = false,
                DisplayOrder = 31,
                IsDefault = 0,
                LangAbbreviation = "en"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 16393,
                Name = "英语 (印度)",
                LanguageCulture = "en-IN",
                EnglishName = "English (India)",
                DisplayName = "English (India)",
                UniqueSeoCode = "in",
                Flag = "~/images/culture-flags/en-IN.gif",
                Status = false,
                DisplayOrder = 32,
                IsDefault = 0,
                LangAbbreviation = "en"
            };
            list.Add(entity);

            entity = new Language
            {
                Lcid = 2057,
                Name = "英语 (亚太地区)",
                LanguageCulture = "en-PH",
                EnglishName = "English (APAC)",
                DisplayName = "English (APAC)",
                UniqueSeoCode = "apac",
                Flag = "~/images/culture-flags/None.gif",
                Status = false,
                DisplayOrder = 33,
                IsDefault = 0,
                LangAbbreviation = "en"
            };
            list.Add(entity);

            list.Insert();

            Meta.Cache.Clear("", true);

            if (XTrace.Debug) XTrace.WriteLine("完成初始化Language[语言]数据！");
        }

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnDelete()
        //{
        //    return base.OnDelete();
        //}
        #endregion

        #region 扩展属性

        /// <summary>标志图像文件名</summary>
        [XmlIgnore, IgnoreDataMember]
        //[ScriptIgnore]
        public String FlagImageFileName => Extends.Get(nameof(FlagImageFileName), k => $"{new CultureInfo(LanguageCulture).Name.ToLowerInvariant()[^2..]}.png");

        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static Language FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.Id == id);
        }

        /// <summary>获取所有语言</summary>
        /// <returns>语言集合</returns>
        public static IList<Language> GetAll()
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Entities;

            return FindAll();
        }

        /// <summary>获取所有语言</summary>
        /// <returns>语言集合</returns>
        public static IEnumerable<Language> GetAllLanguages(bool showHidden = false)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000)
            {
                if (showHidden) return Meta.Cache.Entities.OrderBy(l => l.DisplayOrder).ThenBy(l => l.Id);
                else return Meta.Cache.FindAll(e => e.Status).OrderBy(l => l.DisplayOrder).ThenBy(l => l.Id);
            }

            if (showHidden)
                return FindAll(null, new PageParameter { PageSize = 0, OrderBy = "DisplayOrder asc, Id asc" });

            return FindAll(_.Status == true, new PageParameter { PageSize = 0, OrderBy = "DisplayOrder asc, Id asc" });
        }

        /// <summary>
        /// 获取2个字母的ISO语言代码
        /// </summary>
        /// <param name="language">语言</param>
        /// <returns>ISO语言代码</returns>
        public static string GetTwoLetterIsoLanguageName(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            if (string.IsNullOrEmpty(language.LanguageCulture))
                return "en";

            var culture = new CultureInfo(language.LanguageCulture);
            var code = culture.TwoLetterISOLanguageName;

            return string.IsNullOrEmpty(code) ? "en" : code;
        }

        /// <summary>
        /// 获取指定状态的语言集合
        /// </summary>
        /// <returns>实体集合</returns>
        public static IEnumerable<Language> FindByStatus(Boolean status = true)
        {
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(x => x.Status == status).OrderBy(e => e.Id);

            return FindAll(_.Status == status, new PageParameter { Desc = false, Sort = "Id" });
        }

        #endregion

        #region 高级查询

        // Select Count(Id) as Id,Category From DH_Language Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
        //static readonly FieldCache<Language> _CategoryCache = new FieldCache<Language>(nameof(Category))
        //{
        //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
        //};

        ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
        ///// <returns></returns>
        //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
        #endregion

        #region 业务操作


        #endregion
    }
}