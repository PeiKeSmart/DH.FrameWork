using DH.Cookies;
using DH.Helpers;

using Microsoft.AspNetCore.Http;

using System.ComponentModel;

namespace DH.AspNetCore.Cookies;

public class Cookie : ICookie
{
    private readonly HttpContext _httpContext;
    private const float DefaultExpireDurationMinutes = 43200; // 1 month
    private const bool DefaultHttpOnly = true;
    private const bool ExpireWithBrowser = false;
    private const String DefaultPath = "/";

    public Cookie(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    public T GetValue<T>(string name)
    {
        return GetValue<T>(name, false);
    }

    public T GetValue<T>(string name, bool expireOnceRead)
    {
        T value = default;

        if (_httpContext.Request.Cookies.TryGetValue(name, out string valuStr))
        {
            if (!string.IsNullOrWhiteSpace(valuStr))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

                try
                {
                    value = (T)converter.ConvertFromString(valuStr);
                }
                catch (NotSupportedException)
                {
                    try
                    {
                        value = valuStr.ToObject<T>();
                    }
                    catch (NotSupportedException)
                    {
                        if (converter.CanConvertFrom(typeof(string)))
                        {
                            value = (T)converter.ConvertFrom(valuStr);
                        }
                    }
                }
            }

            if (expireOnceRead)
            {
                Delete(name);
            }
        }

        return value;
    }

    public void SetValue<T>(string name, T value)
    {
        SetValue(name, value, DefaultExpireDurationMinutes, DefaultHttpOnly, ExpireWithBrowser, DefaultPath);
    }

    public void SetValue<T>(string name, T value, String path)
    {
        SetValue(name, value, DefaultExpireDurationMinutes, DefaultHttpOnly, ExpireWithBrowser, path);
    }

    public void SetValue<T>(string name, T value, float expireDurationInMinutes)
    {
        SetValue(name, value, expireDurationInMinutes, DefaultHttpOnly, ExpireWithBrowser, DefaultPath);
    }

    public void SetValue<T>(string name, T value, float expireDurationInMinutes, String path)
    {
        SetValue(name, value, expireDurationInMinutes, DefaultHttpOnly, ExpireWithBrowser, path);
    }

    public void SetValue<T>(string name, T value, bool httpOnly, bool expireWithBrowser)
    {
        SetValue(name, value, DefaultExpireDurationMinutes, httpOnly, expireWithBrowser, DefaultPath);
    }

    public void SetValue<T>(string name, T value, bool httpOnly, bool expireWithBrowser, String path)
    {
        SetValue(name, value, DefaultExpireDurationMinutes, httpOnly, expireWithBrowser, path);
    }

    public void SetValue<T>(string name, T value, float expireDurationInMinutes, bool httpOnly, bool expireWithBrowser, String path)
    {
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

        string cookieValue = string.Empty;

        try
        {
            cookieValue = converter.ConvertToString(value);
        }
        catch (NotSupportedException)
        {
            if (converter.CanConvertTo(typeof(string)))
            {
                cookieValue = (string)converter.ConvertTo(value, typeof(string));
            }
        }

        if (!string.IsNullOrWhiteSpace(cookieValue))
        {

            if (expireWithBrowser)
            {

                _httpContext.Response.Cookies.Append(name, cookieValue);
            }
            else
            {
                _httpContext.Response.Cookies.Append(name, cookieValue, new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(expireDurationInMinutes),
                    HttpOnly = httpOnly,
                    Path = path
                });
            }

        }
    }

    public void Delete(string name)
    {
        _httpContext.Response.Cookies.Append(name, "", new CookieOptions { Expires = DateTime.Now.AddDays(-1d) });
    }
}