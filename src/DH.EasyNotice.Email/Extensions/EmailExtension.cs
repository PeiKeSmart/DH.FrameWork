using EasyNotice.Email.Options;
using EasyNotice.Options;

namespace EasyNotice.Email.Extensions;

public static class EmailExtension {
    public static EasyNoticeOptions UseEmail(
        this EasyNoticeOptions options,
        Action<EmailOptions> configure)
    {
        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        options.RegisterExtension(new EmailOptionsExtension(configure));
        return options;
    }
}