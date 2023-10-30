using EasyNotice.Options;
using EasyNotice.QyWeixin.Options;

namespace EasyNotice.QyWeixin.Extensions;

public static class WeixinExtension {
    public static EasyNoticeOptions UseWeixin(
        this EasyNoticeOptions options,
        Action<WeixinOptions> configure
        )
    {
        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        options.RegisterExtension(new WeixinOptionsExtension(configure));
        return options;
    }
}