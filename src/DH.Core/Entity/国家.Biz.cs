using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;
using XCode.Shards;

namespace DH.Entity;

/// <summary>国家</summary>
public partial class Country : DHEntityBase<Country> {
    #region 对象操作
    static Country()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(nameof(DisplayOrder));

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add<UserModule>();
        Meta.Modules.Add<TimeModule>();
        Meta.Modules.Add<IPModule>();
    }

    /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
    /// <param name="isNew">是否插入</param>
    public override void Valid(Boolean isNew)
    {
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return;

        // 在新插入数据或者修改了指定字段时进行修正
        // 处理当前已登录用户信息，可以由UserModule过滤器代劳
        /*var user = ManageProvider.User;
        if (user != null)
        {
            if (isNew && !Dirtys[nameof(CreateUserID)]) CreateUserID = user.ID;
            if (!Dirtys[nameof(UpdateUserID)]) UpdateUserID = user.ID;
        }*/
        //if (isNew && !Dirtys[nameof(CreateTime)]) CreateTime = DateTime.Now;
        //if (!Dirtys[nameof(UpdateTime)]) UpdateTime = DateTime.Now;
        //if (isNew && !Dirtys[nameof(CreateIP)]) CreateIP = ManageProvider.UserHost;
        //if (!Dirtys[nameof(UpdateIP)]) UpdateIP = ManageProvider.UserHost;
    }

    /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override void InitData()
    {
        // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        if (Meta.Session.Count > 0) return;

        if (XTrace.Debug) XTrace.WriteLine("开始初始化Country[国家]数据……");

        var list = new List<Country>();

        list.Add(new Country
        {
            Name = "China",
            TwoLetterIsoCode = "CN",
            ThreeLetterIsoCode = "CHN",
            NumericIsoCode = 156,
            DisplayOrder = 1,
            IsEnabled = true,
            IsDefault = true
        });

        list.Add(new Country
        {
            Name = "United States",
            TwoLetterIsoCode = "US",
            ThreeLetterIsoCode = "USA",
            NumericIsoCode = 840,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Canada",
            TwoLetterIsoCode = "CA",
            ThreeLetterIsoCode = "CAN",
            NumericIsoCode = 124,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Afghanistan",
            TwoLetterIsoCode = "AF",
            ThreeLetterIsoCode = "AFG",
            NumericIsoCode = 4,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Albania",
            TwoLetterIsoCode = "AL",
            ThreeLetterIsoCode = "ALB",
            NumericIsoCode = 8,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Algeria",
            TwoLetterIsoCode = "DZ",
            ThreeLetterIsoCode = "DZA",
            NumericIsoCode = 12,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "American Samoa",
            TwoLetterIsoCode = "AS",
            ThreeLetterIsoCode = "ASM",
            NumericIsoCode = 16,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Andorra",
            TwoLetterIsoCode = "AD",
            ThreeLetterIsoCode = "AND",
            NumericIsoCode = 20,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Angola",
            TwoLetterIsoCode = "AO",
            ThreeLetterIsoCode = "AGO",
            NumericIsoCode = 24,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Anguilla",
            TwoLetterIsoCode = "AI",
            ThreeLetterIsoCode = "AIA",
            NumericIsoCode = 660,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Antarctica",
            TwoLetterIsoCode = "AQ",
            ThreeLetterIsoCode = "ATA",
            NumericIsoCode = 10,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Antigua and Barbuda",
            TwoLetterIsoCode = "AG",
            ThreeLetterIsoCode = "ATG",
            NumericIsoCode = 28,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Argentina",
            TwoLetterIsoCode = "AR",
            ThreeLetterIsoCode = "ARG",
            NumericIsoCode = 32,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Armenia",
            TwoLetterIsoCode = "AM",
            ThreeLetterIsoCode = "ARM",
            NumericIsoCode = 51,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Aruba",
            TwoLetterIsoCode = "AW",
            ThreeLetterIsoCode = "ABW",
            NumericIsoCode = 533,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Australia",
            TwoLetterIsoCode = "AU",
            ThreeLetterIsoCode = "AUS",
            NumericIsoCode = 36,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Austria",
            TwoLetterIsoCode = "AT",
            ThreeLetterIsoCode = "AUT",
            NumericIsoCode = 40,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Azerbaijan",
            TwoLetterIsoCode = "AZ",
            ThreeLetterIsoCode = "AZE",
            NumericIsoCode = 31,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Bahamas",
            TwoLetterIsoCode = "BS",
            ThreeLetterIsoCode = "BHS",
            NumericIsoCode = 44,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Bahrain",
            TwoLetterIsoCode = "BH",
            ThreeLetterIsoCode = "BHR",
            NumericIsoCode = 48,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Bangladesh",
            TwoLetterIsoCode = "BD",
            ThreeLetterIsoCode = "BGD",
            NumericIsoCode = 50,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Barbados",
            TwoLetterIsoCode = "BB",
            ThreeLetterIsoCode = "BRB",
            NumericIsoCode = 52,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Belarus",
            TwoLetterIsoCode = "BY",
            ThreeLetterIsoCode = "BLR",
            NumericIsoCode = 112,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Belgium",
            TwoLetterIsoCode = "BE",
            ThreeLetterIsoCode = "BEL",
            NumericIsoCode = 56,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Belize",
            TwoLetterIsoCode = "BZ",
            ThreeLetterIsoCode = "BLZ",
            NumericIsoCode = 84,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Benin",
            TwoLetterIsoCode = "BJ",
            ThreeLetterIsoCode = "BEN",
            NumericIsoCode = 204,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Bermuda",
            TwoLetterIsoCode = "BM",
            ThreeLetterIsoCode = "BMU",
            NumericIsoCode = 60,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Bhutan",
            TwoLetterIsoCode = "BT",
            ThreeLetterIsoCode = "BTN",
            NumericIsoCode = 64,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Bolivia",
            TwoLetterIsoCode = "BO",
            ThreeLetterIsoCode = "BOL",
            NumericIsoCode = 68,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Bosnia and Herzegowina",
            TwoLetterIsoCode = "BA",
            ThreeLetterIsoCode = "BIH",
            NumericIsoCode = 70,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Botswana",
            TwoLetterIsoCode = "BW",
            ThreeLetterIsoCode = "BWA",
            NumericIsoCode = 72,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Bouvet Island",
            TwoLetterIsoCode = "BV",
            ThreeLetterIsoCode = "BVT",
            NumericIsoCode = 74,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Brazil",
            TwoLetterIsoCode = "BR",
            ThreeLetterIsoCode = "BRA",
            NumericIsoCode = 76,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "British Indian Ocean Territory",
            TwoLetterIsoCode = "IO",
            ThreeLetterIsoCode = "IOT",
            NumericIsoCode = 86,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Brunei Darussalam",
            TwoLetterIsoCode = "BN",
            ThreeLetterIsoCode = "BRN",
            NumericIsoCode = 96,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Bulgaria",
            TwoLetterIsoCode = "BG",
            ThreeLetterIsoCode = "BGR",
            NumericIsoCode = 100,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Burkina Faso",
            TwoLetterIsoCode = "BF",
            ThreeLetterIsoCode = "BFA",
            NumericIsoCode = 854,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Burundi",
            TwoLetterIsoCode = "BI",
            ThreeLetterIsoCode = "BDI",
            NumericIsoCode = 108,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Cambodia",
            TwoLetterIsoCode = "KH",
            ThreeLetterIsoCode = "KHM",
            NumericIsoCode = 116,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Cameroon",
            TwoLetterIsoCode = "CM",
            ThreeLetterIsoCode = "CMR",
            NumericIsoCode = 120,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Cape Verde",
            TwoLetterIsoCode = "CV",
            ThreeLetterIsoCode = "CPV",
            NumericIsoCode = 132,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Cayman Islands",
            TwoLetterIsoCode = "KY",
            ThreeLetterIsoCode = "CYM",
            NumericIsoCode = 136,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Central African Republic",
            TwoLetterIsoCode = "CF",
            ThreeLetterIsoCode = "CAF",
            NumericIsoCode = 140,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Chad",
            TwoLetterIsoCode = "TD",
            ThreeLetterIsoCode = "TCD",
            NumericIsoCode = 148,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Chile",
            TwoLetterIsoCode = "CL",
            ThreeLetterIsoCode = "CHL",
            NumericIsoCode = 152,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Christmas Island",
            TwoLetterIsoCode = "CX",
            ThreeLetterIsoCode = "CXR",
            NumericIsoCode = 162,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Cocos (Keeling) Islands",
            TwoLetterIsoCode = "CC",
            ThreeLetterIsoCode = "CCK",
            NumericIsoCode = 166,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Colombia",
            TwoLetterIsoCode = "CO",
            ThreeLetterIsoCode = "COL",
            NumericIsoCode = 170,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Comoros",
            TwoLetterIsoCode = "KM",
            ThreeLetterIsoCode = "COM",
            NumericIsoCode = 174,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Congo",
            TwoLetterIsoCode = "CG",
            ThreeLetterIsoCode = "COG",
            NumericIsoCode = 178,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Congo (Democratic Republic of the)",
            TwoLetterIsoCode = "CD",
            ThreeLetterIsoCode = "COD",
            NumericIsoCode = 180,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Cook Islands",
            TwoLetterIsoCode = "CK",
            ThreeLetterIsoCode = "COK",
            NumericIsoCode = 184,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Costa Rica",
            TwoLetterIsoCode = "CR",
            ThreeLetterIsoCode = "CRI",
            NumericIsoCode = 188,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Cote D\'Ivoire",
            TwoLetterIsoCode = "CI",
            ThreeLetterIsoCode = "CIV",
            NumericIsoCode = 384,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Croatia",
            TwoLetterIsoCode = "HR",
            ThreeLetterIsoCode = "HRV",
            NumericIsoCode = 191,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Cuba",
            TwoLetterIsoCode = "CU",
            ThreeLetterIsoCode = "CUB",
            NumericIsoCode = 192,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Cyprus",
            TwoLetterIsoCode = "CY",
            ThreeLetterIsoCode = "CYP",
            NumericIsoCode = 196,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Czech Republic",
            TwoLetterIsoCode = "CZ",
            ThreeLetterIsoCode = "CZE",
            NumericIsoCode = 203,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Denmark",
            TwoLetterIsoCode = "DK",
            ThreeLetterIsoCode = "DNK",
            NumericIsoCode = 208,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Djibouti",
            TwoLetterIsoCode = "DJ",
            ThreeLetterIsoCode = "DJI",
            NumericIsoCode = 262,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Dominica",
            TwoLetterIsoCode = "DM",
            ThreeLetterIsoCode = "DMA",
            NumericIsoCode = 212,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Dominican Republic",
            TwoLetterIsoCode = "DO",
            ThreeLetterIsoCode = "DOM",
            NumericIsoCode = 214,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "East Timor",
            TwoLetterIsoCode = "TL",
            ThreeLetterIsoCode = "TLS",
            NumericIsoCode = 626,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Ecuador",
            TwoLetterIsoCode = "EC",
            ThreeLetterIsoCode = "ECU",
            NumericIsoCode = 218,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Egypt",
            TwoLetterIsoCode = "EG",
            ThreeLetterIsoCode = "EGY",
            NumericIsoCode = 818,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "El Salvador",
            TwoLetterIsoCode = "SV",
            ThreeLetterIsoCode = "SLV",
            NumericIsoCode = 222,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Equatorial Guinea",
            TwoLetterIsoCode = "GQ",
            ThreeLetterIsoCode = "GNQ",
            NumericIsoCode = 226,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Eritrea",
            TwoLetterIsoCode = "ER",
            ThreeLetterIsoCode = "ERI",
            NumericIsoCode = 232,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Estonia",
            TwoLetterIsoCode = "EE",
            ThreeLetterIsoCode = "EST",
            NumericIsoCode = 233,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Eswatini",
            TwoLetterIsoCode = "SZ",
            ThreeLetterIsoCode = "SWZ",
            NumericIsoCode = 748,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Ethiopia",
            TwoLetterIsoCode = "ET",
            ThreeLetterIsoCode = "ETH",
            NumericIsoCode = 231,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Falkland Islands (Malvinas)",
            TwoLetterIsoCode = "FK",
            ThreeLetterIsoCode = "FLK",
            NumericIsoCode = 238,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Faroe Islands",
            TwoLetterIsoCode = "FO",
            ThreeLetterIsoCode = "FRO",
            NumericIsoCode = 234,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Fiji",
            TwoLetterIsoCode = "FJ",
            ThreeLetterIsoCode = "FJI",
            NumericIsoCode = 242,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Finland",
            TwoLetterIsoCode = "FI",
            ThreeLetterIsoCode = "FIN",
            NumericIsoCode = 246,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "France",
            TwoLetterIsoCode = "FR",
            ThreeLetterIsoCode = "FRA",
            NumericIsoCode = 250,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "French Guiana",
            TwoLetterIsoCode = "GF",
            ThreeLetterIsoCode = "GUF",
            NumericIsoCode = 254,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "French Polynesia",
            TwoLetterIsoCode = "PF",
            ThreeLetterIsoCode = "PYF",
            NumericIsoCode = 258,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "French Southern Territories",
            TwoLetterIsoCode = "TF",
            ThreeLetterIsoCode = "ATF",
            NumericIsoCode = 260,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Gabon",
            TwoLetterIsoCode = "GA",
            ThreeLetterIsoCode = "GAB",
            NumericIsoCode = 266,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Gambia",
            TwoLetterIsoCode = "GM",
            ThreeLetterIsoCode = "GMB",
            NumericIsoCode = 270,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Georgia",
            TwoLetterIsoCode = "GE",
            ThreeLetterIsoCode = "GEO",
            NumericIsoCode = 268,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Germany",
            TwoLetterIsoCode = "DE",
            ThreeLetterIsoCode = "DEU",
            NumericIsoCode = 276,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Ghana",
            TwoLetterIsoCode = "GH",
            ThreeLetterIsoCode = "GHA",
            NumericIsoCode = 288,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Gibraltar",
            TwoLetterIsoCode = "GI",
            ThreeLetterIsoCode = "GIB",
            NumericIsoCode = 292,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Greece",
            TwoLetterIsoCode = "GR",
            ThreeLetterIsoCode = "GRC",
            NumericIsoCode = 300,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Greenland",
            TwoLetterIsoCode = "GL",
            ThreeLetterIsoCode = "GRL",
            NumericIsoCode = 304,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Grenada",
            TwoLetterIsoCode = "GD",
            ThreeLetterIsoCode = "GRD",
            NumericIsoCode = 308,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Guadeloupe",
            TwoLetterIsoCode = "GP",
            ThreeLetterIsoCode = "GLP",
            NumericIsoCode = 312,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Guam",
            TwoLetterIsoCode = "GU",
            ThreeLetterIsoCode = "GUM",
            NumericIsoCode = 316,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Guatemala",
            TwoLetterIsoCode = "GT",
            ThreeLetterIsoCode = "GTM",
            NumericIsoCode = 320,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Guinea",
            TwoLetterIsoCode = "GN",
            ThreeLetterIsoCode = "GIN",
            NumericIsoCode = 324,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Guinea-bissau",
            TwoLetterIsoCode = "GW",
            ThreeLetterIsoCode = "GNB",
            NumericIsoCode = 624,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Guyana",
            TwoLetterIsoCode = "GY",
            ThreeLetterIsoCode = "GUY",
            NumericIsoCode = 328,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Haiti",
            TwoLetterIsoCode = "HT",
            ThreeLetterIsoCode = "HTI",
            NumericIsoCode = 332,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Heard and Mc Donald Islands",
            TwoLetterIsoCode = "HM",
            ThreeLetterIsoCode = "HMD",
            NumericIsoCode = 334,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Honduras",
            TwoLetterIsoCode = "HN",
            ThreeLetterIsoCode = "HND",
            NumericIsoCode = 340,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Hong Kong",
            TwoLetterIsoCode = "HK",
            ThreeLetterIsoCode = "HKG",
            NumericIsoCode = 344,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Hungary",
            TwoLetterIsoCode = "HU",
            ThreeLetterIsoCode = "HUN",
            NumericIsoCode = 348,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Iceland",
            TwoLetterIsoCode = "IS",
            ThreeLetterIsoCode = "ISL",
            NumericIsoCode = 352,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "India",
            TwoLetterIsoCode = "IN",
            ThreeLetterIsoCode = "IND",
            NumericIsoCode = 356,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Indonesia",
            TwoLetterIsoCode = "ID",
            ThreeLetterIsoCode = "IDN",
            NumericIsoCode = 360,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Iran (Islamic Republic of)",
            TwoLetterIsoCode = "IR",
            ThreeLetterIsoCode = "IRN",
            NumericIsoCode = 364,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Iraq",
            TwoLetterIsoCode = "IQ",
            ThreeLetterIsoCode = "IRQ",
            NumericIsoCode = 368,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Ireland",
            TwoLetterIsoCode = "IE",
            ThreeLetterIsoCode = "IRL",
            NumericIsoCode = 372,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Israel",
            TwoLetterIsoCode = "IL",
            ThreeLetterIsoCode = "ISR",
            NumericIsoCode = 376,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Italy",
            TwoLetterIsoCode = "IT",
            ThreeLetterIsoCode = "ITA",
            NumericIsoCode = 380,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Jamaica",
            TwoLetterIsoCode = "JM",
            ThreeLetterIsoCode = "JAM",
            NumericIsoCode = 388,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Japan",
            TwoLetterIsoCode = "JP",
            ThreeLetterIsoCode = "JPN",
            NumericIsoCode = 392,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Jordan",
            TwoLetterIsoCode = "JO",
            ThreeLetterIsoCode = "JOR",
            NumericIsoCode = 400,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Jordan",
            TwoLetterIsoCode = "JO",
            ThreeLetterIsoCode = "JOR",
            NumericIsoCode = 400,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Kazakhstan",
            TwoLetterIsoCode = "KZ",
            ThreeLetterIsoCode = "KAZ",
            NumericIsoCode = 398,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Kenya",
            TwoLetterIsoCode = "KE",
            ThreeLetterIsoCode = "KEN",
            NumericIsoCode = 404,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Kiribati",
            TwoLetterIsoCode = "KI",
            ThreeLetterIsoCode = "KIR",
            NumericIsoCode = 296,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Korea",
            TwoLetterIsoCode = "KR",
            ThreeLetterIsoCode = "KOR",
            NumericIsoCode = 410,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Korea, Democratic People\'s Republic of",
            TwoLetterIsoCode = "KP",
            ThreeLetterIsoCode = "PRK",
            NumericIsoCode = 408,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Kuwait",
            TwoLetterIsoCode = "KW",
            ThreeLetterIsoCode = "KWT",
            NumericIsoCode = 414,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Kyrgyzstan",
            TwoLetterIsoCode = "KG",
            ThreeLetterIsoCode = "KGZ",
            NumericIsoCode = 417,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Lao People\'s Democratic Republic",
            TwoLetterIsoCode = "LA",
            ThreeLetterIsoCode = "LAO",
            NumericIsoCode = 418,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Latvia",
            TwoLetterIsoCode = "LV",
            ThreeLetterIsoCode = "LVA",
            NumericIsoCode = 428,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Lebanon",
            TwoLetterIsoCode = "LB",
            ThreeLetterIsoCode = "LBN",
            NumericIsoCode = 422,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Lesotho",
            TwoLetterIsoCode = "LS",
            ThreeLetterIsoCode = "LSO",
            NumericIsoCode = 426,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Liberia",
            TwoLetterIsoCode = "LR",
            ThreeLetterIsoCode = "LBR",
            NumericIsoCode = 430,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Libyan Arab Jamahiriya",
            TwoLetterIsoCode = "LY",
            ThreeLetterIsoCode = "LBY",
            NumericIsoCode = 434,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Liechtenstein",
            TwoLetterIsoCode = "LI",
            ThreeLetterIsoCode = "LIE",
            NumericIsoCode = 438,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Lithuania",
            TwoLetterIsoCode = "LT",
            ThreeLetterIsoCode = "LTU",
            NumericIsoCode = 440,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Luxembourg",
            TwoLetterIsoCode = "LU",
            ThreeLetterIsoCode = "LUX",
            NumericIsoCode = 442,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Macau",
            TwoLetterIsoCode = "MO",
            ThreeLetterIsoCode = "MAC",
            NumericIsoCode = 446,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Macedonia",
            TwoLetterIsoCode = "MK",
            ThreeLetterIsoCode = "MKD",
            NumericIsoCode = 807,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Madagascar",
            TwoLetterIsoCode = "MG",
            ThreeLetterIsoCode = "MDG",
            NumericIsoCode = 450,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Malawi",
            TwoLetterIsoCode = "MW",
            ThreeLetterIsoCode = "MWI",
            NumericIsoCode = 454,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Malaysia",
            TwoLetterIsoCode = "MY",
            ThreeLetterIsoCode = "MYS",
            NumericIsoCode = 458,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Maldives",
            TwoLetterIsoCode = "MV",
            ThreeLetterIsoCode = "MDV",
            NumericIsoCode = 462,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Mali",
            TwoLetterIsoCode = "ML",
            ThreeLetterIsoCode = "MLI",
            NumericIsoCode = 466,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Malta",
            TwoLetterIsoCode = "MT",
            ThreeLetterIsoCode = "MLT",
            NumericIsoCode = 470,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Marshall Islands",
            TwoLetterIsoCode = "MH",
            ThreeLetterIsoCode = "MHL",
            NumericIsoCode = 584,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Martinique",
            TwoLetterIsoCode = "MQ",
            ThreeLetterIsoCode = "MTQ",
            NumericIsoCode = 474,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Mauritania",
            TwoLetterIsoCode = "MR",
            ThreeLetterIsoCode = "MRT",
            NumericIsoCode = 478,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Mauritius",
            TwoLetterIsoCode = "MU",
            ThreeLetterIsoCode = "MUS",
            NumericIsoCode = 480,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Mayotte",
            TwoLetterIsoCode = "YT",
            ThreeLetterIsoCode = "MYT",
            NumericIsoCode = 175,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Mexico",
            TwoLetterIsoCode = "MX",
            ThreeLetterIsoCode = "MEX",
            NumericIsoCode = 484,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Micronesia",
            TwoLetterIsoCode = "FM",
            ThreeLetterIsoCode = "FSM",
            NumericIsoCode = 583,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Moldova",
            TwoLetterIsoCode = "MD",
            ThreeLetterIsoCode = "MDA",
            NumericIsoCode = 498,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Monaco",
            TwoLetterIsoCode = "MC",
            ThreeLetterIsoCode = "MCO",
            NumericIsoCode = 492,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Mongolia",
            TwoLetterIsoCode = "MN",
            ThreeLetterIsoCode = "MNG",
            NumericIsoCode = 496,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Montenegro",
            TwoLetterIsoCode = "ME",
            ThreeLetterIsoCode = "MNE",
            NumericIsoCode = 499,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Montserrat",
            TwoLetterIsoCode = "MS",
            ThreeLetterIsoCode = "MSR",
            NumericIsoCode = 500,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Morocco",
            TwoLetterIsoCode = "MA",
            ThreeLetterIsoCode = "MAR",
            NumericIsoCode = 504,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Mozambique",
            TwoLetterIsoCode = "MZ",
            ThreeLetterIsoCode = "MOZ",
            NumericIsoCode = 508,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Myanmar",
            TwoLetterIsoCode = "MM",
            ThreeLetterIsoCode = "MMR",
            NumericIsoCode = 104,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Namibia",
            TwoLetterIsoCode = "NA",
            ThreeLetterIsoCode = "NAM",
            NumericIsoCode = 516,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Nauru",
            TwoLetterIsoCode = "NR",
            ThreeLetterIsoCode = "NRU",
            NumericIsoCode = 520,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Nepal",
            TwoLetterIsoCode = "NP",
            ThreeLetterIsoCode = "NPL",
            NumericIsoCode = 524,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Netherlands",
            TwoLetterIsoCode = "NL",
            ThreeLetterIsoCode = "NLD",
            NumericIsoCode = 528,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "New Caledonia",
            TwoLetterIsoCode = "NC",
            ThreeLetterIsoCode = "NCL",
            NumericIsoCode = 540,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "New Zealand",
            TwoLetterIsoCode = "NZ",
            ThreeLetterIsoCode = "NZL",
            NumericIsoCode = 554,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Nicaragua",
            TwoLetterIsoCode = "NI",
            ThreeLetterIsoCode = "NIC",
            NumericIsoCode = 558,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Niger",
            TwoLetterIsoCode = "NE",
            ThreeLetterIsoCode = "NER",
            NumericIsoCode = 562,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Nigeria",
            TwoLetterIsoCode = "NG",
            ThreeLetterIsoCode = "NGA",
            NumericIsoCode = 566,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Niue",
            TwoLetterIsoCode = "NU",
            ThreeLetterIsoCode = "NIU",
            NumericIsoCode = 570,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Norfolk Island",
            TwoLetterIsoCode = "NF",
            ThreeLetterIsoCode = "NFK",
            NumericIsoCode = 574,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Northern Mariana Islands",
            TwoLetterIsoCode = "MP",
            ThreeLetterIsoCode = "MNP",
            NumericIsoCode = 580,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Norway",
            TwoLetterIsoCode = "NO",
            ThreeLetterIsoCode = "NOR",
            NumericIsoCode = 578,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Oman",
            TwoLetterIsoCode = "OM",
            ThreeLetterIsoCode = "OMN",
            NumericIsoCode = 512,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Pakistan",
            TwoLetterIsoCode = "PK",
            ThreeLetterIsoCode = "PAK",
            NumericIsoCode = 586,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Palau",
            TwoLetterIsoCode = "PW",
            ThreeLetterIsoCode = "PLW",
            NumericIsoCode = 585,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Palestine",
            TwoLetterIsoCode = "PS",
            ThreeLetterIsoCode = "PSE",
            NumericIsoCode = 275,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Panama",
            TwoLetterIsoCode = "PA",
            ThreeLetterIsoCode = "PAN",
            NumericIsoCode = 591,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Papua New Guinea",
            TwoLetterIsoCode = "PG",
            ThreeLetterIsoCode = "PNG",
            NumericIsoCode = 598,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Paraguay",
            TwoLetterIsoCode = "PY",
            ThreeLetterIsoCode = "PRY",
            NumericIsoCode = 598,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Peru",
            TwoLetterIsoCode = "PE",
            ThreeLetterIsoCode = "PER",
            NumericIsoCode = 604,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Philippines",
            TwoLetterIsoCode = "PH",
            ThreeLetterIsoCode = "PHL",
            NumericIsoCode = 608,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Pitcairn",
            TwoLetterIsoCode = "PN",
            ThreeLetterIsoCode = "PCN",
            NumericIsoCode = 612,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Poland",
            TwoLetterIsoCode = "PL",
            ThreeLetterIsoCode = "POL",
            NumericIsoCode = 616,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Portugal",
            TwoLetterIsoCode = "PT",
            ThreeLetterIsoCode = "PRT",
            NumericIsoCode = 620,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Puerto Rico",
            TwoLetterIsoCode = "PR",
            ThreeLetterIsoCode = "PRI",
            NumericIsoCode = 630,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Qatar",
            TwoLetterIsoCode = "QA",
            ThreeLetterIsoCode = "QAT",
            NumericIsoCode = 634,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Reunion",
            TwoLetterIsoCode = "RE",
            ThreeLetterIsoCode = "REU",
            NumericIsoCode = 638,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Romania",
            TwoLetterIsoCode = "RO",
            ThreeLetterIsoCode = "ROU",
            NumericIsoCode = 642,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Russian Federation",
            TwoLetterIsoCode = "RU",
            ThreeLetterIsoCode = "RUS",
            NumericIsoCode = 643,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Rwanda",
            TwoLetterIsoCode = "RW",
            ThreeLetterIsoCode = "RWA",
            NumericIsoCode = 646,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Saint Kitts and Nevis",
            TwoLetterIsoCode = "KN",
            ThreeLetterIsoCode = "KNA",
            NumericIsoCode = 659,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Saint Lucia",
            TwoLetterIsoCode = "LC",
            ThreeLetterIsoCode = "LCA",
            NumericIsoCode = 662,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Saint Vincent and the Grenadines",
            TwoLetterIsoCode = "VC",
            ThreeLetterIsoCode = "VCT",
            NumericIsoCode = 670,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Samoa",
            TwoLetterIsoCode = "WS",
            ThreeLetterIsoCode = "WSM",
            NumericIsoCode = 882,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "San Marino",
            TwoLetterIsoCode = "SM",
            ThreeLetterIsoCode = "SMR",
            NumericIsoCode = 674,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Sao Tome and Principe",
            TwoLetterIsoCode = "ST",
            ThreeLetterIsoCode = "STP",
            NumericIsoCode = 678,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Saudi Arabia",
            TwoLetterIsoCode = "SA",
            ThreeLetterIsoCode = "SAU",
            NumericIsoCode = 682,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Senegal",
            TwoLetterIsoCode = "SN",
            ThreeLetterIsoCode = "SEN",
            NumericIsoCode = 686,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Serbia",
            TwoLetterIsoCode = "RS",
            ThreeLetterIsoCode = "SRB",
            NumericIsoCode = 688,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Seychelles",
            TwoLetterIsoCode = "SC",
            ThreeLetterIsoCode = "SYC",
            NumericIsoCode = 690,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Sierra Leone",
            TwoLetterIsoCode = "SL",
            ThreeLetterIsoCode = "SLE",
            NumericIsoCode = 694,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Singapore",
            TwoLetterIsoCode = "SG",
            ThreeLetterIsoCode = "SGP",
            NumericIsoCode = 702,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Slovakia (Slovak Republic)",
            TwoLetterIsoCode = "SK",
            ThreeLetterIsoCode = "SVK",
            NumericIsoCode = 703,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Slovenia",
            TwoLetterIsoCode = "SI",
            ThreeLetterIsoCode = "SVN",
            NumericIsoCode = 705,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Solomon Islands",
            TwoLetterIsoCode = "SB",
            ThreeLetterIsoCode = "SLB",
            NumericIsoCode = 90,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Somalia",
            TwoLetterIsoCode = "SO",
            ThreeLetterIsoCode = "SOM",
            NumericIsoCode = 706,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "South Africa",
            TwoLetterIsoCode = "ZA",
            ThreeLetterIsoCode = "ZAF",
            NumericIsoCode = 710,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "South Georgia & South Sandwich Islands",
            TwoLetterIsoCode = "GS",
            ThreeLetterIsoCode = "SGS",
            NumericIsoCode = 239,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "South Sudan",
            TwoLetterIsoCode = "SS",
            ThreeLetterIsoCode = "SSD",
            NumericIsoCode = 728,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Spain",
            TwoLetterIsoCode = "ES",
            ThreeLetterIsoCode = "ESP",
            NumericIsoCode = 724,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Sri Lanka",
            TwoLetterIsoCode = "LK",
            ThreeLetterIsoCode = "LKA",
            NumericIsoCode = 144,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "St. Helena",
            TwoLetterIsoCode = "SH",
            ThreeLetterIsoCode = "SHN",
            NumericIsoCode = 654,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "St. Pierre and Miquelon",
            TwoLetterIsoCode = "PM",
            ThreeLetterIsoCode = "SPM",
            NumericIsoCode = 666,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Sudan",
            TwoLetterIsoCode = "SD",
            ThreeLetterIsoCode = "SDN",
            NumericIsoCode = 736,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Suriname",
            TwoLetterIsoCode = "SR",
            ThreeLetterIsoCode = "SUR",
            NumericIsoCode = 740,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Svalbard and Jan Mayen Islands",
            TwoLetterIsoCode = "SJ",
            ThreeLetterIsoCode = "SJM",
            NumericIsoCode = 744,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Sweden",
            TwoLetterIsoCode = "SE",
            ThreeLetterIsoCode = "SWE",
            NumericIsoCode = 752,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Switzerland",
            TwoLetterIsoCode = "CH",
            ThreeLetterIsoCode = "CHE",
            NumericIsoCode = 756,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Syrian Arab Republic",
            TwoLetterIsoCode = "SY",
            ThreeLetterIsoCode = "SYR",
            NumericIsoCode = 760,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Taiwan",
            TwoLetterIsoCode = "TW",
            ThreeLetterIsoCode = "TWN",
            NumericIsoCode = 158,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Tajikistan",
            TwoLetterIsoCode = "TJ",
            ThreeLetterIsoCode = "TJK",
            NumericIsoCode = 762,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Tanzania",
            TwoLetterIsoCode = "TZ",
            ThreeLetterIsoCode = "TZA",
            NumericIsoCode = 834,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Thailand",
            TwoLetterIsoCode = "TH",
            ThreeLetterIsoCode = "THA",
            NumericIsoCode = 764,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Togo",
            TwoLetterIsoCode = "TG",
            ThreeLetterIsoCode = "TGO",
            NumericIsoCode = 768,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Tokelau",
            TwoLetterIsoCode = "TK",
            ThreeLetterIsoCode = "TKL",
            NumericIsoCode = 772,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Tonga",
            TwoLetterIsoCode = "TO",
            ThreeLetterIsoCode = "TON",
            NumericIsoCode = 776,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Trinidad and Tobago",
            TwoLetterIsoCode = "TT",
            ThreeLetterIsoCode = "TTO",
            NumericIsoCode = 780,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Tunisia",
            TwoLetterIsoCode = "TN",
            ThreeLetterIsoCode = "TUN",
            NumericIsoCode = 788,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Turkey",
            TwoLetterIsoCode = "TR",
            ThreeLetterIsoCode = "TUR",
            NumericIsoCode = 792,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Turkmenistan",
            TwoLetterIsoCode = "TM",
            ThreeLetterIsoCode = "TKM",
            NumericIsoCode = 795,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Turks and Caicos Islands",
            TwoLetterIsoCode = "TC",
            ThreeLetterIsoCode = "TCA",
            NumericIsoCode = 796,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Tuvalu",
            TwoLetterIsoCode = "TV",
            ThreeLetterIsoCode = "TUV",
            NumericIsoCode = 798,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Uganda",
            TwoLetterIsoCode = "UG",
            ThreeLetterIsoCode = "UGA",
            NumericIsoCode = 800,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Ukraine",
            TwoLetterIsoCode = "UA",
            ThreeLetterIsoCode = "UKR",
            NumericIsoCode = 804,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "United Arab Emirates",
            TwoLetterIsoCode = "AE",
            ThreeLetterIsoCode = "ARE",
            NumericIsoCode = 784,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "United Kingdom",
            TwoLetterIsoCode = "GB",
            ThreeLetterIsoCode = "GBR",
            NumericIsoCode = 826,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "United States minor outlying islands",
            TwoLetterIsoCode = "UM",
            ThreeLetterIsoCode = "UMI",
            NumericIsoCode = 581,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Uruguay",
            TwoLetterIsoCode = "UY",
            ThreeLetterIsoCode = "URY",
            NumericIsoCode = 858,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Uzbekistan",
            TwoLetterIsoCode = "UZ",
            ThreeLetterIsoCode = "UZB",
            NumericIsoCode = 860,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Vanuatu",
            TwoLetterIsoCode = "VU",
            ThreeLetterIsoCode = "VUT",
            NumericIsoCode = 548,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Vatican City State (Holy See)",
            TwoLetterIsoCode = "VA",
            ThreeLetterIsoCode = "VAT",
            NumericIsoCode = 336,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Venezuela",
            TwoLetterIsoCode = "VE",
            ThreeLetterIsoCode = "VEN",
            NumericIsoCode = 862,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Viet Nam",
            TwoLetterIsoCode = "VN",
            ThreeLetterIsoCode = "VNM",
            NumericIsoCode = 704,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Virgin Islands (British)",
            TwoLetterIsoCode = "VG",
            ThreeLetterIsoCode = "VGB",
            NumericIsoCode = 92,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Virgin Islands (U.S.)",
            TwoLetterIsoCode = "VI",
            ThreeLetterIsoCode = "VIR",
            NumericIsoCode = 850,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Wallis and Futuna Islands",
            TwoLetterIsoCode = "WF",
            ThreeLetterIsoCode = "WLF",
            NumericIsoCode = 876,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Western Sahara",
            TwoLetterIsoCode = "EH",
            ThreeLetterIsoCode = "ESH",
            NumericIsoCode = 732,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Yemen",
            TwoLetterIsoCode = "YE",
            ThreeLetterIsoCode = "YEM",
            NumericIsoCode = 887,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Zambia",
            TwoLetterIsoCode = "ZM",
            ThreeLetterIsoCode = "ZMB",
            NumericIsoCode = 894,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Add(new Country
        {
            Name = "Zimbabwe",
            TwoLetterIsoCode = "ZW",
            ThreeLetterIsoCode = "ZWE",
            NumericIsoCode = 716,
            DisplayOrder = 100,
            IsEnabled = true
        });

        list.Insert();

        if (XTrace.Debug) XTrace.WriteLine("完成初始化Country[国家]数据！");
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
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static Country FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>
    /// 当不为多语言时默认的国家区域
    /// </summary>
    /// <returns></returns>
    public static Country FindByDefault()
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.IsDefault);

        return Find(_.IsDefault == true);
    }

    /// <param name="name">设备DeviceName</param>
    /// <returns>实体对象</returns>
    public static Country FindByName(String name)
    {
        if (name.IsNullOrWhiteSpace()) return null;

        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Name == name);

        return Find(_.Name == name);
    }

    /// <param name="ThreeLetterIsoCode">搜索三个字母的ISO</param>
    /// <returns>实体对象</returns>
    public static Country FindByThreeLetterIsoCode(String ThreeLetterIsoCode)
    {
        if (ThreeLetterIsoCode.IsNullOrWhiteSpace()) return null;

        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ThreeLetterIsoCode == ThreeLetterIsoCode);

        return Find(_.ThreeLetterIsoCode == ThreeLetterIsoCode);
    }

    /// <returns>搜索两个个字母的ISO</returns>
    public static Country FindByTwoLetterIsoCode(String TwoLetterIsoCode)
    {
        if (TwoLetterIsoCode.IsNullOrWhiteSpace()) return null;

        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.TwoLetterIsoCode == TwoLetterIsoCode);

        return Find(_.TwoLetterIsoCode == TwoLetterIsoCode);
    }

    /// <returns>搜索数字的ISO</returns>
    public static Country FindByNumericIsoCode(int NumericIsoCode)
    {
        if (NumericIsoCode <= 0) return null;

        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.NumericIsoCode == NumericIsoCode);

        return Find(_.NumericIsoCode == NumericIsoCode);
    }

    /// <summary>
    /// 根据列表Ids获取列表
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public static IList<Country> FindByIds(String ids)
    {
        if (ids.IsNullOrWhiteSpace()) return new List<Country>();

        ids = ids.Trim(',');

        if (Meta.Session.Count < 1000)
        {
            return Meta.Cache.FindAll(x => ids.SplitAsInt(",").Contains(x.Id));
        }

        return FindAll(_.Id.In(ids.Split(',')));
    }
    #endregion

    #region 高级查询

    // Select Count(Id) as Id,Category From Country Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<Country> _CategoryCache = new FieldCache<Country>(nameof(Category))
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