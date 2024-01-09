using System.Text;

namespace DH.SmsPdu.Decoder;

/// <summary>
/// 7Bit解码器。
/// </summary>
public class SevenBitDecoder : IDecoder {
    /// <summary>
    /// 特殊字符字典表。
    /// </summary>
    const string GMS_ALPHABET = "\x40\xa3\x24\xa5\xe8\xe9\xf9\xec\xf2\xc7\n\xd8\xf8\r\xc5\xe5\u0394\x5f\u03a6\u0393\u039b\u03a9\u03a0\u03a8\u03a3\u0398\u039e\x1b\xc6\xe6\xdf\xc9\x20\x21\x22\x23\xa4\x25\x26\x27\x28\x29\x2a\x2b\x2c\x2d\x2e\x2f\x30\x31\x32\x33\x34\x35\x36\x37\x38\x39\x3a\x3b\x3c\x3d\x3e\x3f\xa1\x41\x42\x43\x44\x45\x46\x47\x48\x49\x4a\x4b\x4c\x4d\x4e\x4f\x50\x51\x52\x53\x54\x55\x56\x57\x58\x59\x5a\xc4\xd6\xd1\xdc\xa7\xbf\x61\x62\x63\x64\x65\x66\x67\x68\x69\x6a\x6b\x6c\x6d\x6e\x6f\x70\x71\x72\x73\x74\x75\x76\x77\x78\x79\x7a\xe4\xf6\xf1\xfc\xe0";

    public string Decode(byte[] raw) => GetString(raw);

    public static string GetString(byte[] raw)
    {
        var buffer = Decompress(raw);
        var sb = new StringBuilder();

        for (var i = 0; i < buffer.Length; i++)
        {
            var c = buffer[i];

            if (c == 27 && ++i < buffer.Length)
            {
                switch (buffer[i])
                {
                    case 10:
                        sb.Append('\x0c');
                        continue;
                    case 20:
                        sb.Append('\x5e');
                        continue;
                    case 40:
                        sb.Append('\x7b');
                        continue;
                    case 41:
                        sb.Append('\x7d');
                        continue;
                    case 47:
                        sb.Append('\x5c');
                        continue;
                    case 60:
                        sb.Append('\x5b');
                        continue;
                    case 61:
                        sb.Append('\x7e');
                        continue;
                    case 62:
                        sb.Append('\x5d');
                        continue;
                    case 64:
                        sb.Append('\x7c');
                        continue;
                    case 101:
                        sb.Append('\u20ac');
                        continue;
                    default:
                        --i;
                        break;
                }
            }
            sb.Append(GMS_ALPHABET[c]);
        }

        return sb.ToString();
    }

    static byte[] Decompress(byte[] raw)
    {
        using var ms = new MemoryStream();
        byte nLeft = 0;
        byte nByte = 0;

        foreach (var b in raw)
        {
            var sevenBits = (byte)(((b << nByte) | nLeft) & 0x7f);

            ms.WriteByte(sevenBits);

            nLeft = (byte)(b >> (7 - nByte));
            nByte++;

            if (nByte == 7)
            {
                if (nLeft != 0) ms.WriteByte(nLeft);
                nLeft = 0;
                nByte = 0;
            }
        }

        return ms.ToArray();
    }
}