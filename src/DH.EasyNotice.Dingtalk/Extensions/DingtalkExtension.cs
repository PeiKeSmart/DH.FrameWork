using EasyNotice.Dingtalk.Options;

using EasyNotice.Options;

namespace EasyNotice.Dingtalk.Extensions;

public static class DingtalkExtension {
    public static EasyNoticeOptions UseDingTalk(
        this EasyNoticeOptions options,
        Action<DingtalkOptions> configure
        )
    {
        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        options.RegisterExtension(new DingtalkOptionsExtension(configure));
        return options;
    }
}