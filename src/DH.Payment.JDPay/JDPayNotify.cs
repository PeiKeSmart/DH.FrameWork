using System.Xml.Serialization;

namespace DG.Payment.JDPay
{
    /// <summary>
    /// JDPay 通知。
    /// </summary>
    public abstract class JDPayNotify : JDPayObject
    {
        /// <summary>
        /// 原始参数
        /// </summary>
        [XmlIgnore]
        public JDPayDictionary Parameters { get; internal set; }
    }
}
