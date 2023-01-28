﻿namespace DH.CliWrap;

/// <summary>
/// Result of a command execution.
/// </summary>
public class CommandResult
{
    /// <summary>
    /// Exit code set by the underlying process.
    /// </summary>
    public int ExitCode { get; }

    /// <summary>
    /// When the command started executing.
    /// </summary>
    public DateTimeOffset StartTime { get; }

    /// <summary>
    /// When the command finished executing.
    /// </summary>
    public DateTimeOffset ExitTime { get; }

    /// <summary>
    /// Total duration of the command execution.
    /// </summary>
    public TimeSpan RunTime => ExitTime - StartTime;

    /// <summary>
    /// Initializes an instance of <see cref="CommandResult" />.
    /// </summary>
    public CommandResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime)
    {
        ExitCode = exitCode;
        StartTime = startTime;
        ExitTime = exitTime;
    }
}