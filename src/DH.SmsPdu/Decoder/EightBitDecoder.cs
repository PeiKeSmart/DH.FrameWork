using System.Text;

namespace DH.SmsPdu.Decoder;

/// <summary>
/// 8Bit解码器。
/// </summary>
public class EightBitDecoder : IDecoder {
    public string Decode(byte[] raw) => Encoding.Latin1.GetString(raw);
}