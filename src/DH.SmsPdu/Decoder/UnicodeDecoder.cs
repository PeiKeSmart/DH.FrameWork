using System.Text;

namespace DH.SmsPdu.Decoder;

/// <summary>
/// Unicode解码器。
/// </summary>
public class UnicodeDecoder : IDecoder {
    public string Decode(byte[] raw) => Encoding.BigEndianUnicode.GetString(raw);
}