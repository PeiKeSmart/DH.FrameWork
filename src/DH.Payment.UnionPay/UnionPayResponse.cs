namespace DG.Payment.UnionPay
{
    /// <summary>
    /// UnionPay 响应。
    /// </summary>
    public abstract class UnionPayResponse : UnionPayObject
    {
        public string Body { get; set; }
    }
}
