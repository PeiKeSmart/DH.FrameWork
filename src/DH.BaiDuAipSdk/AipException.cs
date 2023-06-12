namespace DG.BaiDuAipSdk
{
    /// <summary>
	///     百度AI异常类
	/// </summary>
	[Serializable]
    public class AipException : Exception
    {
        public AipException()
        {
            Code = -1;
        }

        public AipException(string message) : base(message)
        {
        }

        public AipException(int code, string message) : base(message)
        {
            Code = code;
        }

        public int Code { get; set; }

        public static AipException TokenException(string message)
        {
            return new AipException("Token request failed! " + message);
        }
    }
}
