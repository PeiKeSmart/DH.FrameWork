﻿namespace DH.FileZilla.Api;

/// <summary>
/// Exception thrown when a problem is detected in FileZillaApi 
/// </summary>
[Serializable]
public class ApiException : Exception
{
    /// <summary>
    /// Creates a new Exception.
    /// </summary>
    public ApiException()
    {
    }

    /// <summary>
    /// Creates a new Exception.
    /// </summary>
    /// <param name="message"></param>
    public ApiException(string message)
        : base(message)
    {
    }
    /// <summary>
    /// Creates a new Exception.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="inner"></param>
    public ApiException(string message, Exception inner)
        : base(message, inner)
    {
    }

#if NETFULL
		/// <summary>
		/// Creates a new Exception.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected ApiException(
			SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
		}
#endif
}