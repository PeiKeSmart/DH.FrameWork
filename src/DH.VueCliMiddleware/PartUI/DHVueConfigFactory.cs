using System.Text;
using System.Text.Json;

namespace VueCliMiddleware.PartUI;

public class DHVueConfigFactory
{
    private const string FileName = "DHVue.Config.json";

    #region 即时读取
    public static DHVueConfig Create()
    {
        var DHVueConfig = new DHVueConfig();
        var DHVueConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Settings", FileName);
        var DHVueConfigJsonStr = String.Empty;
        if (!File.Exists(DHVueConfigFilePath))
        {
            // 不存在，则新建初始化默认
            DHVueConfigJsonStr = JsonSerializer.Serialize(DHVueConfig);
            File.WriteAllText(DHVueConfigFilePath, DHVueConfigJsonStr, Encoding.UTF8);

            return DHVueConfig;
        }

        DHVueConfigJsonStr = File.ReadAllText(DHVueConfigFilePath, Encoding.UTF8);
        var jsonSerializerOptions = new JsonSerializerOptions();
        jsonSerializerOptions.PropertyNameCaseInsensitive = true;
        DHVueConfig = JsonSerializer.Deserialize<DHVueConfig>(DHVueConfigJsonStr, jsonSerializerOptions);

        return DHVueConfig;
    }
    #endregion

    #region 保存
    public static void Save(DHVueConfig config)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }
        try
        {
            var DHVueConfigJsonStr = JsonSerializer.Serialize(config);
            var DHVueConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Settings", FileName);
            File.WriteAllText(DHVueConfigFilePath, DHVueConfigJsonStr, Encoding.UTF8);
        }
        catch { }
    }
    #endregion
}
