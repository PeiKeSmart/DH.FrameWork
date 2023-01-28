﻿namespace DH.CliWrap.Utils.Extensions;

internal static partial class AsyncDisposableExtensions
{
    public static IAsyncDisposable ToAsyncDisposable(this IDisposable disposable) =>
        new AsyncDisposableAdapter(disposable);
}

internal static partial class AsyncDisposableExtensions
{
    // Provides a dynamic and uniform way to deal with async disposable.
    // Used as an abstraction to polyfill IAsyncDisposable implementations in BCL types. For example:
    // - Stream class on .NET Framework 4.6.1 -> calls Dispose()
    // - Stream class on .NET Core 3.0 -> calls DisposeAsync()
    // - Stream class on .NET Standard 2.0 -> calls DisposeAsync() or Dispose(), depending on the runtime
    private readonly struct AsyncDisposableAdapter : IAsyncDisposable
    {
        private readonly IDisposable _target;

        public AsyncDisposableAdapter(IDisposable target) => _target = target;

        public async ValueTask DisposeAsync()
        {
            if (_target is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync().ConfigureAwait(false);
            }
            else
            {
                _target.Dispose();
            }
        }
    }
}