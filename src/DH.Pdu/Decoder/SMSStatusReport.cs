using System;

namespace DG.Pdu.Decoder
{
    public enum ReportStatus
    {
        NoResponseFromSME = 0x62,
        NotSend = 0x60,
        Success = 0
    }

    public class SMSStatusReport : SMSBase
    {
        #region Members
        protected DateTime _reportTimeStamp;
        protected ReportStatus _reportStatus;
        protected byte _messageReference;
        protected string _phoneNumber;
        protected DateTime _serviceCenterTimeStamp;
        #endregion

        #region Public Properties
        public byte MessageReference { get { return _messageReference; } }
        public string PhoneNumber { get { return _phoneNumber; } }
        public DateTime ServiceCenterTimeStamp { get { return _serviceCenterTimeStamp; } }
        public DateTime ReportTimeStamp { get { return _reportTimeStamp; } }
        public ReportStatus ReportStatus { get { return _reportStatus; } }

        public override SMSType Type { get { return SMSType.StatusReport; } }
        #endregion

        #region Public Statics
        public static void Fetch(SMSStatusReport statusReport, ref string source)
        {
            SMSBase.Fetch(statusReport, ref source);

            statusReport._messageReference = PopByte(ref source);
            statusReport._phoneNumber = PopPhoneNumber(ref source);
            statusReport._serviceCenterTimeStamp = PopDate(ref source);
            statusReport._reportTimeStamp = PopDate(ref source);
            statusReport._reportStatus = (ReportStatus)PopByte(ref source);
        }
        #endregion
    }
}
