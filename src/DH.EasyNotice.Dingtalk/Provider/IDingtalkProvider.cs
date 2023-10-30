using EasyNotice.Dingtalk.Messages;
using EasyNotice.Models;

namespace EasyNotice.Dingtalk.Provider;

public interface IDingtalkProvider : IEasyNotice {
    Task<EasyNoticeSendResponse> SendTextAsync(string text);

    Task<EasyNoticeSendResponse> SendTextAsync(TextMessage message);

    Task<EasyNoticeSendResponse> SendMarkdownAsync(string title, string text);

    Task<EasyNoticeSendResponse> SendMarkdownAsync(MarkdownMessage message);

    Task<EasyNoticeSendResponse> SendActionCardAsync(string title, string text, string singleTitle, string singleURL, string btnOrientation = "0");

    Task<EasyNoticeSendResponse> SendActionCardAsync(ActionCardMessage message);

}