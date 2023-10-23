using ACMESharp.Protocol.Resources;

using McMaster.Extensions.CommandLineUtils;

using NewLife;
using NewLife.Log;

using Newtonsoft.Json;

namespace ACMECLI;

[Command]
internal class Program {
    [Option(ShortName = "", Description = "用于存储有状态信息的目录；默认为当前")]
    public string State { get; } = ".";

    [Option(ShortName = "", Description = "预定义ACME CA基本终结点的名称（指定无效值以查看列表）")]
    [AllowedValues(
            Constants.LetsEncryptName,
            Constants.LetsEncryptStagingName,
            IgnoreCase = true
        )]
    public string CaName { get; } = Constants.LetsEncryptName;

    [Option(ShortName = "", Description = "ACME CA端点的完整URL；该选项替代" + nameof(CaName))]
    public string? CaUrl { get; }

    [Option(CommandOptionType.MultipleValue,
                ShortName = "", Description = "一封或多封要注册为帐户联系信息的电子邮件（可以重复）")]
    public IEnumerable<string>? Email { get; }



    private string? _statePath;

    static async Task Main(string[] args)
    {
        XTrace.UseConsole();

        await CommandLineApplication.ExecuteAsync<Program>(args);
    }

    public async Task OnExecute()
    {
        _statePath = State.GetFullPath();
        XTrace.WriteLine("################################################################################");
        XTrace.WriteLine("## ACCOUNT");
        XTrace.WriteLine("################################################################################");
        XTrace.WriteLine("");

        if (!Directory.Exists(_statePath))
        {
            XTrace.WriteLine($"正在创建状态持久性路径[{_statePath}]");
            Directory.CreateDirectory(_statePath);
            XTrace.WriteLine("");
        }

        var url = CaUrl;
        if (url.IsNullOrWhiteSpace())
        {
            Constants.NameUrlMap.TryGetValue(CaName, out url);
        }
        if (url.IsNullOrWhiteSpace())
        {
            var ex = new Exception("未解析的ACME CA URL；必须指定名称或URL参数");
            XTrace.WriteException(ex);
            throw ex;
        }

        ServiceDirectory? acmeDir = default;
        if (LoadStateInto(ref acmeDir, failThrow: false,
                    Constants.AcmeDirectoryFile))
        {
            XTrace.WriteLine("已加载现有服务目录");
            XTrace.WriteLine("");
        }

        //if (NameServer != null)
        //{
        //    DnsUtil.DnsServers = NameServer.ToArray();
        //}


        XTrace.WriteLine($"进来了吗？");
    }

    private bool LoadStateInto<T>(ref T? value, bool failThrow, string nameFormat, params object[] nameArgs)
    {
        var name = string.Format(nameFormat, nameArgs);
        var fullPath = Path.Combine(_statePath!, name);
        if (!File.Exists(fullPath))
            if (failThrow)
            {
                var ex = new Exception($"无法从不存在的路径[{fullPath}]读取对象");
                XTrace.WriteException(ex);
                throw ex;
            }
            else
                return false;

        var ser = File.ReadAllText(fullPath);
        value = JsonConvert.DeserializeObject<T>(ser);
        return true;
    }
}
