namespace DH.Sms;

public enum SmsProvider {
    Unset,
    Alibaba,
    Tencent
}

public interface ISmsProvider {
    public ISmsProvider Config(params string[] args);

    public (bool, String) Send(string templateParam, params string[] phoneNums);

    public (bool, String) Send2(string signName, string templateCode, string templateParam, params string[] phoneNums);

    public Task<(bool, String)> SendAsync(string templateParam, params string[] phoneNums);

    public Task<(bool, String)> Send2Async(string signName, string templateCode, string templateParam, params string[] phoneNums);
}