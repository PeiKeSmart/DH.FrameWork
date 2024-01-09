namespace DH.SmsPdu.Decoder;

/// <summary>
/// 解码器。
/// </summary>
public interface IDecoder {
    /// <summary>
    /// 解码。
    /// </summary>
    /// <param name="raw">字节</param>
    /// <returns>返回已解码的字符串。</returns>
    string Decode(byte[] raw);
}