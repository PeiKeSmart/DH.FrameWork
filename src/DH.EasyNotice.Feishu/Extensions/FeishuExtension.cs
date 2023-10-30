using EasyNotice.Options;

namespace EasyNotice.Extensions;

public static class FeishuExtension {
    public static EasyNoticeOptions UseFeishu(
        this EasyNoticeOptions options,
        Action<FeishuOptions> configure
        )
    {
        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        options.RegisterExtension(new FeishuOptionsExtension(configure));
        return options;
    }
}