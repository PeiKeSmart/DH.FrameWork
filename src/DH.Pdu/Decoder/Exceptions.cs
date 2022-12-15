using System;

namespace DG.Pdu.Decoder
{
    class UnknownSMSTypeException : Exception
    {
        public UnknownSMSTypeException(byte pduType) : base(string.Format("未知短信类型。 PDU类型二进制: {0}.", Convert.ToString(pduType, 2)))
        { }
    }
}
