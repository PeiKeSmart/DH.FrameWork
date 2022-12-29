namespace DH.Cookies;

public interface ICookie
{
    T GetValue<T>(string name);

    T GetValue<T>(string name, bool expireOnceRead);

    void SetValue<T>(string name, T value);

    void SetValue<T>(string name, T value, string path);

    /// <summary>
    /// 设置Cookie项
    /// </summary>
    /// <typeparam name="T">对象</typeparam>
    /// <param name="name">键</param>
    /// <param name="value">值</param>
    /// <param name="expireDurationInMinutes">过期时间，分钟。</param>
    void SetValue<T>(string name, T value, float expireDurationInMinutes);

    void SetValue<T>(string name, T value, float expireDurationInMinutes, string path);

    void SetValue<T>(string name, T value, bool httpOnly, bool expireWithBrowser);

    void SetValue<T>(string name, T value, bool httpOnly, bool expireWithBrowser, string path);

    void SetValue<T>(string name, T value, float expireDurationInMinutes, bool httpOnly, bool expireWithBrowser, string Path);

    void Delete(string name);
}