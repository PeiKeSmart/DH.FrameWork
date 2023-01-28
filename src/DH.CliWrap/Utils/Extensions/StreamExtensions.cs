﻿using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;

namespace DH.CliWrap.Utils.Extensions;

internal static class StreamExtensions
{
    public static async Task CopyToAsync(
        this Stream source,
        Stream destination,
        bool autoFlush,
        CancellationToken cancellationToken = default)
    {
        using var buffer = MemoryPool<byte>.Shared.Rent(BufferSizes.Stream);

        int bytesRead;
        while ((bytesRead = await source.ReadAsync(buffer.Memory, cancellationToken).ConfigureAwait(false)) != 0)
        {
            await destination.WriteAsync(buffer.Memory[..bytesRead], cancellationToken).ConfigureAwait(false);

            if (autoFlush)
                await destination.FlushAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public static async IAsyncEnumerable<string> ReadAllLinesAsync(
        this StreamReader reader,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        // We could use reader.ReadLineAsync() and loop on it, but that method
        // does not support cancellation.

        var lineBuffer = new StringBuilder();
        using var buffer = MemoryPool<char>.Shared.Rent(BufferSizes.StreamReader);

        // Following sequences are treated as individual linebreaks:
        // - \r
        // - \n
        // - \r\n
        // Even though \r and \n are linebreaks on their own, \r\n together
        // should not yield two lines. To ensure that, we keep track of the
        // previous char and check if it's part of a sequence.
        var prevSeqChar = (char?)null;

        int charsRead;
        while ((charsRead = await reader.ReadAsync(buffer.Memory, cancellationToken).ConfigureAwait(false)) > 0)
        {
            for (var i = 0; i < charsRead; i++)
            {
                var curChar = buffer.Memory.Span[i];

                // If current char and last char are part of a line break sequence,
                // skip over the current char and move on.
                // The buffer was already yielded in the previous iteration, so there's
                // nothing left to do.
                if (prevSeqChar == '\r' && curChar == '\n')
                {
                    prevSeqChar = null;
                    continue;
                }

                // If current char is \n or \r, yield the buffer (even if it is empty)
                if (curChar is '\n' or '\r')
                {
                    yield return lineBuffer.ToString();
                    lineBuffer.Clear();
                }
                // For any other char, just append it to the buffer
                else
                {
                    lineBuffer.Append(curChar);
                }

                prevSeqChar = curChar;
            }
        }

        // Yield what's remaining in the buffer
        if (lineBuffer.Length > 0)
            yield return lineBuffer.ToString();
    }
}