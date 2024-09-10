namespace System.IO;

public static class DHStreamExtensions
{
    public static byte[] GetAllBytes(this Stream stream)
    {
        using (var memoryStream = new MemoryStream())
        {
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public static async Task<byte[]> GetAllBytesAsync(this Stream stream, CancellationToken cancellationToken = default)
    {
        using (var memoryStream = new MemoryStream())
        {
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }
            await stream.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }
    }

    public static Task CopyToAsync(this Stream stream, Stream destination, CancellationToken cancellationToken)
    {
        if (stream.CanSeek)
        {
            stream.Position = 0;
        }
        return stream.CopyToAsync(
            destination,
            81920, // 这已经是默认值，但需要设置才能传递cancellationToken
            cancellationToken
        );
    }
}
