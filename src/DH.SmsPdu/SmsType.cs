namespace DH.SmsPdu;

/// <summary>
/// 短信类型。
/// </summary>
enum SmsType {
    /// <summary>
    /// 普通短信。
    /// </summary>
    SMS_RECEIVED = 0,
    SMS_STATUS_REPORT = 2,
    SMS_SUBMIT = 1,
    /// <summary>
    /// EMS短信。
    /// </summary>
    EMS_RECEIVED = 0x40,
    EMS_SUBMIT = 65
}