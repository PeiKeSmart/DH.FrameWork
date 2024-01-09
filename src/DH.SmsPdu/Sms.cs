using DH.SmsPdu.Decoder;

using System.Globalization;

namespace DH.SmsPdu;

/// <summary>
/// 短信结构。
/// </summary>
public class Sms {
    /// <summary>
    /// 短信中心号码。
    /// </summary>
    public string SmscNumber { get; private set; } = default!;

    /// <summary>
    /// 发件人号码。
    /// </summary>
    public string SenderNumber { get; private set; } = default!;

    /// <summary>
    /// 发送时间。
    /// </summary>
    public DateTime TimeStamp { get; private set; }

    /// <summary>
    /// 短信编号。
    /// </summary>
    public string? SplitId { get; private set; }

    /// <summary>
    /// 短信总数。
    /// </summary>
    public int SplitCount { get; private set; } = 1;

    /// <summary>
    /// 当前第几条。
    /// </summary>
    public int SplitIndex { get; private set; } = 1;

    /// <summary>
    /// 短信正文。
    /// </summary>
    public string Content { get; private set; } = default!;

    #region 编解码方法

    static string ByteReverse(byte[] raw)
    {
        var hex = Convert.ToHexString(raw);
        var reversed = Enumerable.Range(0, hex.Length)
                                 .Where(x => x % 2 == 0)
                                 .Select(x => hex.Substring(x + 1, 1) + hex.Substring(x, 1));
        return string.Concat(reversed).TrimEnd('F');
    }

    static string GetNumber(byte type, byte[] number)
    {
        return (type & 0x70) switch
        {
            0x10 => "+" + ByteReverse(number),
            0x50 => SevenBitDecoder.GetString(number),
            _ => ByteReverse(number)
        };
    }

    static SmsType GetSmsType(byte firstOctet)
    {
        var num = firstOctet & 3;
        var num2 = firstOctet & 0x40;
        if (num == 3 && num2 == 64) return SmsType.EMS_SUBMIT;
        return (SmsType)(num + num2);
    }

    static IDecoder GetEncodingScheme(byte encodingScheme)
    {
        return (encodingScheme & 0x0c) switch
        {
            0 => new SevenBitDecoder(),
            0x04 => new EightBitDecoder(),
            0x08 => new UnicodeDecoder(),
            _ => throw new Exception("未知PDU消息编码方案"),
        };
    }

    #endregion

    #region Decode

    public static Sms Decode(string pdu)
    {
        var ms = new MemoryStream(Convert.FromHexString(pdu));
        return Decode(ms);
    }

    public static Sms Decode(Stream stream)
    {
        var sms = new Sms();
        using var reader = new BinaryReader(stream);

        var smscLength = reader.ReadByte();
        var smscType = reader.ReadByte();
        var smscNumber = reader.ReadBytes(smscLength - 1);
        sms.SmscNumber = GetNumber(smscType, smscNumber);
        var type = GetSmsType(reader.ReadByte()); // 类型

        if (type != SmsType.SMS_RECEIVED && type != SmsType.EMS_RECEIVED) throw new Exception($"不支持的短信类型 {type}");

        var senderLength = reader.ReadByte(); // 发信人号码长度
        var senderType = reader.ReadByte();// 发信人号码类型
        var senderNumber = reader.ReadBytes((int)Math.Ceiling(senderLength / 2F)); // 发信人号码
        sms.SenderNumber = GetNumber(senderType, senderNumber);

        var TP_PID = reader.ReadByte(); // 协议ID
        var TP_DCS = reader.ReadByte(); // 编码方案

        var timeStamp = ByteReverse(reader.ReadBytes(7)); // 发送时间戳
        sms.TimeStamp = DateTime.ParseExact(2000 + int.Parse(timeStamp.Substring(0, 2)) + timeStamp.Substring(2, 10), "yyyyMMddHHmmss", CultureInfo.CurrentCulture);

        var TP_UDL = reader.ReadByte();

        // 长短信分割，建议使用Redis保存每一条短信，用一个原子增长标记收到所有短信后合并
        if (type == SmsType.EMS_RECEIVED) // 也可以这样判断  (type & 0x40) == 0x40
        {
            var TP_UDHL = reader.ReadByte(); // 用户数据头长度

            var header = reader.ReadBytes(TP_UDHL); // 用户数据头
            if (TP_UDHL >= 5)
            {
                var headerId = header[0]; // Header ID
                var headerLength = header[1]; // 头长度
                if (headerLength >= 3) // 一般长度3字节，ETL公司的分割ID是2字节
                {
                    sms.SplitId = Convert.ToHexString(header[2..^2]); // 分割ID，需要跳过ID和头长度，至倒数第2个字节
                    sms.SplitCount = header[^2]; // 一共多少条短信
                    sms.SplitIndex = header[^1]; // 当前是第几条
                }
            }
        }

        var data = reader.ReadBytes(TP_UDL); // 短信内容
        sms.Content = GetEncodingScheme(TP_DCS).Decode(data);

        return sms;
    }

    #endregion
}