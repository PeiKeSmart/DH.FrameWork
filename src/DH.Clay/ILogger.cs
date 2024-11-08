﻿namespace DH.Clay;

public enum LogLevel
{
    Debug,
    Information,
    Warning,
    Error,
    Fatal
}

public interface ILogger
{
    bool IsEnabled(LogLevel level);
    void Log(LogLevel level, Exception exception, string format, params object[] args);
}