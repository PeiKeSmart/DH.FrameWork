using DH.Data;

namespace DH.Services.MDB;

public class SmsSettings : CacheObject {
    static SmsSettings()
    {
        Init();
    }

    private static void Init()
    {
        var list = cdb.FindAll<SmsSettings>();
        if (list.Count == 0)
        {
            var model = new SmsSettings();
            model.Name = "fenghuo";
            model.DisplayName = "烽火";
            model.SmsType = 0; // 国内
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "fenghuo";
            model.DisplayName = "烽火";
            model.SmsType = 1; // 国际
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "lianlu";
            model.DisplayName = "联麓";
            model.SmsType = 0; // 国内
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "lianlu";
            model.DisplayName = "联麓";
            model.SmsType = 1; // 国际
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "aliyun";
            model.DisplayName = "阿里云";
            model.SmsType = 0; // 国内
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "aliyun";
            model.DisplayName = "阿里云";
            model.SmsType = 1; // 国际
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "tencent";
            model.DisplayName = "腾讯云";
            model.SmsType = 0; // 国内
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "tencent";
            model.DisplayName = "腾讯云";
            model.SmsType = 1; // 国际
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "mysubmail";
            model.DisplayName = "赛邮云";
            model.SmsType = 0; // 国内
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "mysubmail";
            model.DisplayName = "赛邮云";
            model.SmsType = 1; // 国际
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "netease";
            model.DisplayName = "网易云信";
            model.SmsType = 0; // 国内
            cdb.Insert(model);

            model = new SmsSettings();
            model.Name = "netease";
            model.DisplayName = "网易云信";
            model.SmsType = 1; // 国际
            cdb.Insert(model);
        }
    }

    public static List<SmsSettings> GetAll()
    {
        return cdb.FindAll<SmsSettings>();
    }

    /// <summary>
    /// 显示名称
    /// </summary>
    public String DisplayName { get; set; }

    /// <summary>
    /// 短信类型。0为国内通知类，1为国际通知类，2为国内营销类，3为国际营销类
    /// </summary>
    public Int32 SmsType { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public Boolean IsEnabled { get; set; }

    /// <summary>
    /// AccessKey
    /// </summary>
    public String AccessKey { get; set; }

    /// <summary>
    /// AccessSecret
    /// </summary>
    public String AccessSecret { get; set; }

    /// <summary>
    /// 短信签名
    /// </summary>
    public String PassKey { get; set; }

    /// <summary>
    /// 腾讯短信AppId
    /// </summary>
    public String AppId { get; set; }

    /// <summary>
    /// 登录短信
    /// </summary>
    public Boolean SmsLogin { get; set; }

    /// <summary>
    /// 注册短信
    /// </summary>
    public Boolean SmsRegister { get; set; }

    /// <summary>
    /// 找回密码短信
    /// </summary>
    public Boolean SmsPassword { get; set; }
}
