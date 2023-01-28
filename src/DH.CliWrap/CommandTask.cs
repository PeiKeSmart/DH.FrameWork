﻿using DH.CliWrap.Utils.Extensions;

using System.Runtime.CompilerServices;

namespace DH.CliWrap;

/// <summary>
/// Represents an asynchronous execution of a command.
/// </summary>
public partial class CommandTask<TResult> : IDisposable
{
    /// <summary>
    /// Underlying task.
    /// </summary>
    public Task<TResult> Task { get; }

    /// <summary>
    /// Underlying process ID.
    /// </summary>
    public int ProcessId { get; }

    /// <summary>
    /// Initializes an instance of <see cref="CommandTask{TResult}" />.
    /// </summary>
    public CommandTask(Task<TResult> task, int processId)
    {
        Task = task;
        ProcessId = processId;
    }

    internal CommandTask<T> Bind<T>(Func<Task<TResult>, Task<T>> selector) =>
        new(selector(Task), ProcessId);

    /// <summary>
    /// Lazily maps the result of the task using the specified transform.
    /// </summary>
    public CommandTask<T> Select<T>(Func<TResult, T> transform) =>
        Bind(task => task.Select(transform));

    /// <summary>
    /// Gets the awaiter of the underlying task.
    /// Used to enable await expressions on this object.
    /// </summary>
    public TaskAwaiter<TResult> GetAwaiter() => Task.GetAwaiter();

    /// <summary>
    /// Configures an awaiter used to await this task.
    /// </summary>
    public ConfiguredTaskAwaitable<TResult> ConfigureAwait(bool continueOnCapturedContext) =>
        Task.ConfigureAwait(continueOnCapturedContext);

    /// <inheritdoc />
    public void Dispose() => Task.Dispose();
}

public partial class CommandTask<TResult>
{
    /// <summary>
    /// Casts a command task into a regular task.
    /// </summary>
    public static implicit operator Task<TResult>(CommandTask<TResult> commandTask) => commandTask.Task;
}