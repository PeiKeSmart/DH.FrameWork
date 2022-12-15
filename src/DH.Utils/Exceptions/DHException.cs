using NewLife.Log;

using System.Runtime.Serialization;

namespace DH.Exceptions;

/// <summary>
/// 表示在应用程序执行期间发生的错误
/// </summary>
[Serializable]
public class DHException : Exception
{
    /// <summary>
    /// 初始化Exception类的新实例.
    /// </summary>
    public DHException()
    {
        XTrace.WriteException(this);
    }

    /// <summary>
    /// 使用指定的错误消息初始化Exception类的新实例.
    /// </summary>
    /// <param name="message">描述错误的消息.</param>
    public DHException(string message)
        : base(message)
    {
        XTrace.Log.Error($"{message}/r/n{this}");
    }

    /// <summary>
    /// 使用指定的错误消息初始化Exception类的新实例.
    /// </summary>
    /// <param name="messageFormat">异常消息格式.</param>
    /// <param name="args">异常消息参数.</param>
    public DHException(string messageFormat, params object[] args)
        : base(string.Format(messageFormat, args))
    {
        XTrace.Log.Error(messageFormat, args);
    }

    /// <summary>
    /// 使用序列化的数据初始化Exception类的新实例.
    /// </summary>
    /// <param name="info">包含有关引发异常的序列化对象数据的序列化信息.</param>
    /// <param name="context">包含有关源或目标的上下文信息的流上下文.</param>
    protected DHException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// 使用指定的错误消息和对引起该异常的内部异常的引用来初始化Exception类的新实例。.
    /// </summary>
    /// <param name="message">解释异常原因的错误消息.</param>
    /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则为null引用.</param>
    public DHException(string message, Exception innerException)
        : base(message, innerException)
    {
        XTrace.Log.Error($"{message}/r/n{innerException}");
    }
}