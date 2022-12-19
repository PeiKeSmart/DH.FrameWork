﻿namespace DH.Helpers;

public static class Guard
{
    public static T NotNull<T>(T t, string paramName)
    {
        if (t is null)
        {
            throw new ArgumentNullException(paramName);
        }
        return t;
    }

    public static string NotNullOrEmpty(string str, string paramName)
    {
        NotNull(str, paramName);
        if (string.IsNullOrEmpty(paramName))
        {
            throw new ArgumentNullException(paramName);
        }
        return str;
    }
}