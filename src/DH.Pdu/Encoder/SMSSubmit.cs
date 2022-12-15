namespace DH.Pdu.Encoder;

/// <summary>
/// 表示要发送的SMS消息
/// </summary>
public class SMSSubmit
{
    /// <summary>
    /// First Octet Bits 1,0 - 短信 提交
    /// </summary>
    public const byte FIRST_OCTET_SMS_SUBMIT = 0x01;

    /// <summary>
    /// First Octet Bits 4,3 - 有效期为相对格式
    /// </summary>
    public const byte FIRST_OCTET_VALIDITY_PERIOD_RELATIVEFORMAT = 0x10;

    /// <summary>
    /// First Octet Bit 5 - 要求提供状态
    /// </summary>
    public const byte FIRST_OCTET_REQUEST_DELIVERY_CONFIRMATION = 0x20;

    /// <summary>
    /// First Octet Bit 6 - 存在用户数据头
    /// </summary>
    public const byte FIRST_OCTET_USER_DATA_HEADER = 0x40;

    /// <summary>
    /// User data header - 结束的消息
    /// </summary>        
    public const byte PDU_UDH_CONCATED_MESSAGE_16BIT = 0x08;

    /// <summary>
    /// User data header - 连续消息长度的长度
    /// </summary>
    public const byte PDU_UDH_CONCATED_MESSAGE_16BIT_HEADERLENGTH = 0x04;

    /// <summary>
    /// User data header - 标题标题的长度
    /// </summary>
    public const byte CONCATED_UDH_FULL_LENGTH = 0x07;

    /// <summary>
    /// 请求短信传递确认（可选）
    /// </summary>
    public bool RequestDeliveryConfirmation { get; set; }

    /// <summary>
    /// 消息的验证期（可选）
    /// </summary>
    public TimeSpan ValidityPeriod { get; set; }

    /// <summary>
    /// 消息参考（可选）
    /// </summary>
    public byte MessageReference { get; set; }

    /// <summary>
    /// 消息协议标识符（可选）
    /// </summary>
    public byte ProtocolIdentifier { get; set; }

    /// <summary>
    /// 收信息的电话号码
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 指定如何在手机上显示和存储消息
    /// </summary>
    public MessageIndication Indication { get; set; }

    /// <summary>
    /// 消息主体内容数据
    /// </summary>
    public ISmsMessageContent MessageToSend { get; set; }

    /// <summary>
    /// 以空消息主体内容实例化
    /// </summary>
    public SMSSubmit() : this(null)
    {
    }

    /// <summary>
    /// 实例化
    /// </summary>
    /// <param name="messageToSent">要发送的消息</param>
    public SMSSubmit(ISmsMessageContent messageToSent)
    {
        // 设置默认值
        this.RequestDeliveryConfirmation = false;
        this.ValidityPeriod = TimeSpan.MinValue;
        this.MessageReference = 0x00;
        this.ProtocolIdentifier = 0x00;
        this.MessageToSend = messageToSent;

        this.Indication = new MessageIndication(MessageClass.MESpecific);
        this.Indication.Type = IndicationType.Voicemail;
        this.Indication.Operation = MessageIndicationOperation.NotSet;
        this.Indication.IsActive = false;
    }

    /// <summary>
    /// 方法返回PDU格式化消息的列表。
    /// 如果数据适合一条消息而不是列表只有一个项目。
    /// 否则数据会合并为更多消息。
    /// </summary>
    /// <returns></returns>
    public List<byte[]> GetPDUList()
    {
        int maxOctetsCount;

        switch (MessageToSend.DataEncoding)
        {
            case DataEncoding.Default7bit:
                maxOctetsCount = 160;
                break;
            case DataEncoding.Data8bit:
                maxOctetsCount = 140;     // 可能会更低，一些漫游伙伴在设置为140时不喜欢它
                break;
            case DataEncoding.UCS2_16bit:
                maxOctetsCount = 140;
                break;
            default:
                maxOctetsCount = 140;
                break;
        }

        byte[] body = MessageToSend.GetSMSBytes();
        byte[] messageUdh = MessageToSend.GetUDHBytes();

        // If UDH exists then reserve one more byte for UDH length
        int maxLen = maxOctetsCount - messageUdh.Length;
        maxLen -= (messageUdh.Length > 0) ? 1 : 0;

        // Count parts
        int parts = (int)Math.Ceiling((double)body.Length / (double)maxLen);
        parts = (int)Math.Ceiling((double)(body.Length + parts * messageUdh.Length) / (double)maxLen);

        if (parts > 1)
            parts = (int)Math.Ceiling((double)(body.Length + parts * messageUdh.Length + parts * (CONCATED_UDH_FULL_LENGTH + 1)) / (double)maxLen);

        List<byte[]> messagePart = new List<byte[]>();
        byte[] udhByteArray;
        byte[] messageByteArray;
        ConcatedUDH conUDH = default(ConcatedUDH);

        // This will hold the byteCount send in message (without UDH)
        int bytesCount = maxOctetsCount;
        // Thiis will hold the position in the message array
        int messagePos = 0;

        if (parts > 1)
        {
            conUDH.Reference = 01;
            conUDH.Parts = (byte)parts;

            for (int i = 0; i < parts; i++)
            {
                conUDH.Sequence = (byte)(i + 1);
                udhByteArray = GetConcatPdu(conUDH, messageUdh);

                // if it's not last message then
                if (i < (parts - 1))
                    bytesCount = maxOctetsCount - udhByteArray.Length - 1;
                else
                    bytesCount = body.Length - messagePos;

                byte[] bodyPart = new byte[bytesCount];
                Array.Copy(body, messagePos, bodyPart, 0, bytesCount);

                // Encode message with given encoding
                byte[] encodedBody = GetEncodedMessage(bodyPart);

                // Takes the message length before encoding
                int messageLength = bodyPart.Length + udhByteArray.Length;

                // There is a align in 7bit encoding
                if (MessageToSend.DataEncoding == DataEncoding.Default7bit)
                    messageLength++;

                //creates message
                messageByteArray = new byte[encodedBody.Length + udhByteArray.Length];
                Array.Copy(udhByteArray, messageByteArray, udhByteArray.Length);
                Array.Copy(encodedBody, 0, messageByteArray, udhByteArray.Length, encodedBody.Length);
                messagePos += bytesCount;

                messageByteArray = GetPDUBytes(messageByteArray, messageLength, udhByteArray.Length > 0);
                messagePart.Add(messageByteArray);
            }
        }
        else
        {
            conUDH.Parts = 1;
            udhByteArray = GetConcatPdu(conUDH, messageUdh);

            // Encode body according to given Encoding :)
            byte[] encodedBody = GetEncodedMessage(body);

            // Takes the message length before encoding
            int messageLength = body.Length + udhByteArray.Length;

            messageByteArray = new byte[encodedBody.Length + udhByteArray.Length];
            Array.Copy(udhByteArray, messageByteArray, udhByteArray.Length);
            Array.Copy(encodedBody, 0, messageByteArray, udhByteArray.Length, encodedBody.Length);

            messageByteArray = GetPDUBytes(messageByteArray, messageLength, udhByteArray.Length > 0);
            messagePart.Add(messageByteArray);
        }

        return messagePart;
    }

    private byte[] GetEncodedMessage(byte[] messageData)
    {
        switch (MessageToSend.DataEncoding)
        {
            case DataEncoding.Default7bit:
                return OctetsToSeptets(messageData);
            case DataEncoding.Data8bit:
                return messageData;
            case DataEncoding.UCS2_16bit:
                return messageData;
            default:
                return messageData;
        }
    }

    /// <summary>
    /// Method adds `Concat` information into the User Header
    /// </summary>
    /// <param name="udhStruct">stucture representing the </param>
    /// <param name="messageUDH">message user header</param>        
    private byte[] GetConcatPdu(ConcatedUDH udhStruct, byte[] messageUDH)
    {
        MemoryStream stream = new MemoryStream();

        // If we have message for concating, write concating header
        if (udhStruct.Parts > 1)
        {
            MemoryStream concatHeader = new MemoryStream();
            concatHeader.WriteByte(PDU_UDH_CONCATED_MESSAGE_16BIT);
            concatHeader.WriteByte(PDU_UDH_CONCATED_MESSAGE_16BIT_HEADERLENGTH);

            concatHeader.WriteByte((byte)(udhStruct.Reference >> 8));
            concatHeader.WriteByte((byte)udhStruct.Reference);

            concatHeader.WriteByte(udhStruct.Parts);
            concatHeader.WriteByte(udhStruct.Sequence);

            stream.WriteByte((byte)(concatHeader.Length + messageUDH.Length));
            concatHeader.WriteTo(stream);
        }
        // if we have some messageuUDH, write the header length
        else if (messageUDH.Length > 0)
            stream.WriteByte((byte)messageUDH.Length);

        stream.Write(messageUDH, 0, messageUDH.Length);
        return stream.ToArray();
    }

    /// <summary>
    /// Returns given bytes PDU formated with the user header
    /// </summary>
    /// <param name="messageBody">Message body</param>
    /// <param name="messageLength">Length of the message</param>
    /// <param name="hasCustomHeader">Does message have custom header inside</param>
    private byte[] GetPDUBytes(byte[] messageBody, int messageLength, bool hasCustomHeader)
    {
        MemoryStream message = new MemoryStream();

        byte[] header = GetPDUHeader(messageLength, hasCustomHeader);

        message.Write(header, 0, header.Length);
        message.Write(messageBody, 0, messageBody.Length);

        byte[] messageBytes = message.ToArray();
        message.Close();

        return messageBytes;
    }

    /// <summary>
    /// Returns PDU header of the text message.
    /// Including phone number and other flags
    /// </summary>
    /// <param name="dataLength">lenght of data in message</param>   
    /// <param name="hasCustomHeader">Does message have custom header inside</param>
    private byte[] GetPDUHeader(int dataLength, bool hasCustomHeader)
    {
        MemoryStream header = new MemoryStream();
        header.WriteByte(0x00);  // Length of SMSC
        // TP-MTI Message type
        header.WriteByte(GetFirstOctet(hasCustomHeader));
        header.WriteByte(this.MessageReference); // TP-MR Message Reference 

        WritePhoneNumber(header, PhoneNumber);
        // TP-PID. Protocol identifier.
        header.WriteByte(ProtocolIdentifier);
        // TP-DCS Data coding scheme
        header.WriteByte(this.Indication.ToByte(this.MessageToSend.DataEncoding));

        // TP-SCTS. Time stamp (semi-octets)
        if (ValidityPeriod > TimeSpan.MinValue)
            header.WriteByte(GetValidityPeriod());

        header.WriteByte((byte)dataLength);  // +1 is to count also this byte

        byte[] headerBytes = header.ToArray();
        header.Close();

        return headerBytes;
    }

    /// <summary>
    /// Return first octet of the PDU header depending message properties settings
    /// </summary>
    /// <returns></returns>
    private byte GetFirstOctet(bool hasCustomHeader)
    {
        byte firstOctet = FIRST_OCTET_SMS_SUBMIT;

        if (ValidityPeriod > TimeSpan.MinValue)
            firstOctet |= FIRST_OCTET_VALIDITY_PERIOD_RELATIVEFORMAT;

        if (RequestDeliveryConfirmation)
            firstOctet |= FIRST_OCTET_REQUEST_DELIVERY_CONFIRMATION;

        if (hasCustomHeader)
            firstOctet |= FIRST_OCTET_USER_DATA_HEADER;

        return firstOctet;
    }

    /// <summary>
    /// Returns value representing current validity period
    /// </summary>
    /// <returns></returns>
    private byte GetValidityPeriod()
    {
        TimeSpan value = ValidityPeriod;
        byte validity;

        if (value.Days > 441)
            value = new TimeSpan(440, 0, 0, 0);

        if (value.Days > 30) //Up to 441 days
            validity = (byte)(192 + (int)(value.Days / 7));
        else if (value.Days > 1) //Up to 30 days
            validity = (byte)(166 + value.Days);
        else if (value.Hours > 12) //Up to 24 hours
            validity = (byte)(143 + (value.Hours - 12) * 2 + value.Minutes / 30);
        else if (value.Hours > 1 || value.Minutes > 1) //Up to 12 days
            validity = (byte)(value.Hours * 12 + value.Minutes / 5 - 1);
        else
            validity = 0x00;

        return validity;
    }

    /// <summary>
    /// Writes phone in PDU format
    /// </summary>
    /// <param name="stream">Destination stream</param>
    /// <param name="phoneNumber">Phone number to write</param>        
    private void WritePhoneNumber(MemoryStream stream, string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
            throw new ArgumentException("Phone number is not set");

        bool isInternational = phoneNumber.StartsWith("+");
        byte numberFormat = 0x81;

        if (isInternational)
        {
            phoneNumber = phoneNumber.Remove(0, 1);
            numberFormat = 0x91;
        }

        byte numberLength = (byte)phoneNumber.Length;

        stream.WriteByte(numberLength);
        stream.WriteByte(numberFormat);
        WriteBcdNumber(stream, phoneNumber);
    }

    /// <summary>
    /// Encodes phone number into BCD.        
    /// </summary>
    /// <param name="stream">Destination stream</param>
    /// <param name="phoneNumber">Phone number to write</param>        
    private void WriteBcdNumber(MemoryStream stream, string phoneNumber)
    {
        int bcd = 0x00;
        int n = 0;

        // First convert to a "half octet" value
        for (int i = 0; i < phoneNumber.Length; i++)
        {
            switch (phoneNumber[i])
            {
                case '0':
                    bcd |= 0x00;
                    break;
                case '1':
                    bcd |= 0x10;
                    break;
                case '2':
                    bcd |= 0x20;
                    break;
                case '3':
                    bcd |= 0x30;
                    break;
                case '4':
                    bcd |= 0x40;
                    break;
                case '5':
                    bcd |= 0x50;
                    break;
                case '6':
                    bcd |= 0x60;
                    break;
                case '7':
                    bcd |= 0x70;
                    break;
                case '8':
                    bcd |= 0x80;
                    break;
                case '9':
                    bcd |= 0x90;
                    break;
                case '*':
                    bcd |= 0xA0;
                    break;
                case '#':
                    bcd |= 0xB0;
                    break;
                case 'a':
                    bcd |= 0xC0;
                    break;
                case 'b':
                    bcd |= 0xE0;
                    break;
            }

            n++;

            if (n == 2)
            {
                stream.WriteByte((byte)bcd);
                n = 0;
                bcd = 0x00;
            }
            else
            {
                bcd >>= 4;
            }
        }

        if (n == 1)
        {
            bcd |= 0xF0;
            stream.WriteByte((byte)bcd);
        }
    }

    /// <summary>
    /// Converts array of characters into septets
    /// </summary>
    /// <param name="octetsArray">Array of octets</param>
    private byte[] OctetsToSeptets(byte[] octetsArray)
    {
        int arrayLength = octetsArray.Length;
        // Extend octets array with byte
        byte[] workingArray = new byte[arrayLength + 1];
        Array.Copy(octetsArray, workingArray, arrayLength);

        arrayLength = workingArray.Length;

        MemoryStream octets = new MemoryStream();

        for (int i = 0; i < arrayLength; ++i)
        {
            int m = i % 8;
            if (m != 7)
            {
                int n;
                if (i == arrayLength - 1)
                    n = 0 & PowSum(0, m);
                else
                {
                    n = workingArray[i + 1] & PowSum(0, m);
                    workingArray[i + 1] -= (byte)n;
                }

                workingArray[i] /= (byte)Math.Pow(2, m);
                workingArray[i] += (byte)(Math.Pow(2, 7 - m) * n);

                // We dont want the last 0x00 byte to be added
                if ((arrayLength - i) > 1)
                    octets.WriteByte(workingArray[i]);
            }
        }

        return octets.ToArray();
    }

    /// <summary>
    /// Sum POW(2, i), where i goes through 0 to n.
    /// </summary>
    /// <param name="startBit">Start bit</param>
    /// <param name="n">Number of bits</param>        
    private static int PowSum(int startBit, int n)
    {
        int sum = 0;
        for (int i = n; i >= startBit; --i)
        {
            sum += (int)Math.Pow(2, i);
        }
        return sum;
    }

    private struct ConcatedUDH
    {
        public byte Parts;
        public byte Sequence;
        public byte Reference;
    }
}
