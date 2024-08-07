﻿using System.Text;

namespace DH.ServerSentEvent;

/// <summary>
/// A standard Builder pattern for constructing a <see cref="Configuration"/> instance.
/// </summary>
/// <remarks>
/// <para>
/// Initialize a builder by calling <c>new ConfigurationBuilder(uri)</c> or
/// <c>Configuration.Builder(uri)</c>. The URI is always required; all other properties
/// are set to defaults. Use the builder's setter methods to modify any desired properties;
/// setter methods can be chained. Then call <c>Build()</c> to construct the final immutable
/// <c>Configuration</c>.
/// </para>
/// <para>
/// All setter methods will throw <c>ArgumentException</c> if called with an invalid value,
/// so it is never possible for <c>Build()</c> to fail.
/// </para>
/// </remarks>
public class ConfigurationBuilder {
    #region Private Fields

    internal readonly Uri _uri;
    internal TimeSpan _initialRetryDelay = Configuration.DefaultInitialRetryDelay;
    internal TimeSpan _backoffResetThreshold = Configuration.DefaultBackoffResetThreshold;
    internal string _lastEventId;
    internal IDictionary<string, string> _requestHeaders = new Dictionary<string, string>();
    internal HttpMessageHandler _httpMessageHandler;
    internal HttpClient _httpClient;
    internal TimeSpan _maxRetryDelay = Configuration.DefaultMaxRetryDelay;
    internal HttpMethod _method = HttpMethod.Get;
    internal bool _preferDataAsUtf8Bytes = false;
    internal TimeSpan _readTimeout = Configuration.DefaultReadTimeout;
    internal Func<HttpContent> _requestBodyFactory;
    internal TimeSpan _responseStartTimeout = Configuration.DefaultResponseStartTimeout;
    internal Action<HttpRequestMessage> _httpRequestModifier;
    #endregion

    #region Constructor

    internal ConfigurationBuilder(Uri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }
        this._uri = uri;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Constructs a <see cref="Configuration"/> instance based on the current builder properies.
    /// </summary>
    /// <returns>the configuration</returns>
    public Configuration Build() =>
        new Configuration(this);

    /// <summary>
    /// Obsolete name for <see cref="ResponseStartTimeout(TimeSpan)"/>.
    /// </summary>
    /// <param name="responseStartTimeout">the timeout</param>
    /// <returns>the builder</returns>
    [Obsolete("Use ResponseStartTimeout")]
    public ConfigurationBuilder ConnectionTimeout(TimeSpan responseStartTimeout) =>
        ResponseStartTimeout(responseStartTimeout);

    /// <summary>
    /// Sets a delegate hook invoked before an HTTP request is performed. This may be useful if you
    /// want to modify some properties of the request that EventSource doesn't already have an option for.
    /// </summary>
    /// <param name="httpRequestModifier">code that will be called with the request before it is sent</param>
    /// <returns>the builder</returns>
    public ConfigurationBuilder HttpRequestModifier(Action<HttpRequestMessage> httpRequestModifier)
    {
        this._httpRequestModifier = httpRequestModifier;
        return this;
    }

    /// <summary>
    /// Sets the initial amount of time to wait before attempting to reconnect to the EventSource API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the connection fails more than once, the retry delay will increase from this value using
    /// a backoff algorithm.
    /// </para>
    /// <para>
    /// The default value is <see cref="Configuration.DefaultInitialRetryDelay"/>. Negative values
    /// are changed to zero.
    /// </para>
    /// <para>
    /// The actual duration of each delay will vary slightly because there is a random jitter
    /// factor to avoid clients all reconnecting at once.
    /// </para>
    /// </remarks>
    /// <param name="initialRetryDelay">the initial retry delay</param>
    /// <returns>the builder</returns>
    /// <seealso cref="MaxRetryDelay(TimeSpan)"/>
    public ConfigurationBuilder InitialRetryDelay(TimeSpan initialRetryDelay)
    {
        _initialRetryDelay = FiniteTimeSpan(initialRetryDelay);
        return this;
    }

    /// <summary>
    /// Sets the maximum amount of time to wait before attempting to reconnect.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <c>EventSource</c> uses an exponential backoff algorithm (with random jitter) so that
    /// the delay between reconnections starts at <see cref="InitialRetryDelay(TimeSpan)"/> but
    /// increases with each subsequent attempt. <c>MaxRetryDelay</c> sets a limit on how long
    /// the delay can be.
    /// </para>
    /// <para>
    /// The default value is <see cref="Configuration.DefaultMaxRetryDelay"/>. Negative values
    /// are changed to zero.
    /// </para>
    /// </remarks>
    /// <param name="maxRetryDelay">the maximum retry delay</param>
    /// <returns>the builder</returns>
    /// <seealso cref="InitialRetryDelay(TimeSpan)"/>
    public ConfigurationBuilder MaxRetryDelay(TimeSpan maxRetryDelay)
    {
        _maxRetryDelay = FiniteTimeSpan(maxRetryDelay);
        return this;
    }

    /// <summary>
    /// Sets the amount of time a connection must stay open before the EventSource resets its backoff delay.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If a connection fails before the threshold has elapsed, the delay before reconnecting will be greater
    /// than the last delay; if it fails after the threshold, the delay will start over at the initial minimum
    /// value. This prevents long delays from occurring on connections that are only rarely restarted.
    /// </para>
    /// <para>
    /// The default value is <see cref="Configuration.DefaultBackoffResetThreshold"/>. Negative
    /// values are changed to zero.
    /// </para>
    /// </remarks>
    /// <param name="backoffResetThreshold">the threshold time</param>
    /// <returns>the builder</returns>
    public ConfigurationBuilder BackoffResetThreshold(TimeSpan backoffResetThreshold)
    {
        _backoffResetThreshold = FiniteTimeSpan(backoffResetThreshold);
        return this;
    }

    /// <summary>
    /// Sets the maximum amount of time EventSource will wait between starting an HTTP request and
    /// receiving the response headers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is the same as the <c>Timeout</c> property in .NET's <c>HttpClient</c>. The default value is
    /// <see cref="Configuration.DefaultConnectionTimeout"/>.
    /// </para>
    /// <para>
    /// It is <i>not</i> the same as a TCP connection timeout. A connection timeout would include only the
    /// time of establishing the connection, not the time it takes for the server to prepare the beginning
    /// of the response. .NET does not consistently support a connection timeout, but if you are using .NET
    /// Core or .NET 5+ you can implement it by using <c>SocketsHttpHandler</c> as your
    /// <see cref="HttpMessageHandler(System.Net.Http.HttpMessageHandler)"/> and setting the
    /// <c>ConnectTimeout</c> property there.
    /// </para>
    /// </remarks>
    /// <param name="responseStartTimeout">the timeout</param>
    /// <returns></returns>
    public ConfigurationBuilder ResponseStartTimeout(TimeSpan responseStartTimeout)
    {
        _responseStartTimeout = TimeSpanCanBeInfinite(responseStartTimeout);
        return this;
    }

    /// <summary>
    /// Sets the timeout when reading from the EventSource API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The connection will be automatically dropped and restarted if the server sends no data within
    /// this interval. This prevents keeping a stale connection that may no longer be working. It is common
    /// for SSE servers to send a simple comment line (":") as a heartbeat to prevent timeouts.
    /// </para>
    /// <para>
    /// The default value is <see cref="Configuration.DefaultReadTimeout"/>.
    /// </para>
    /// </remarks>
    public ConfigurationBuilder ReadTimeout(TimeSpan readTimeout)
    {
        _readTimeout = TimeSpanCanBeInfinite(readTimeout);
        return this;
    }

    /// <summary>
    /// Sets the last event identifier.
    /// </summary>
    /// <remarks>
    /// Setting this value will cause EventSource to add a "Last-Event-ID" header in its HTTP request.
    /// This normally corresponds to the <see cref="MessageEvent.LastEventId"/> field of a previously
    /// received event.
    /// </remarks>
    /// <param name="lastEventId">the event identifier</param>
    /// <returns>the builder</returns>
    public ConfigurationBuilder LastEventId(string lastEventId)
    {
        _lastEventId = lastEventId;
        return this;
    }

    /// <summary>
    /// Specifies whether to use UTF-8 byte arrays internally if possible when
    /// reading the stream.
    /// </summary>
    /// <remarks>
    /// As described in <see cref="MessageEvent"/>, in some applications it may be
    /// preferable to store and process event data as UTF-8 byte arrays rather than
    /// strings. By default, <c>EventSource</c> will use the <c>string</c> type when
    /// processing the event stream; if you then use <see cref="MessageEvent.DataUtf8Bytes"/>
    /// to get the data, it will be converted to a byte array as needed. However, if
    /// you set <c>PreferDataAsUtf8Bytes</c> to <see langword="true"/>, the event data
    /// will be stored internally as a UTF-8 byte array so that if you read
    /// <see cref="MessageEvent.DataUtf8Bytes"/>, you will get the same array with no
    /// extra copying or conversion. Therefore, for greatest efficiency you should set
    /// this to <see langword="true"/> if you intend to process the data as UTF-8. Note
    /// that Server-Sent Event streams always use UTF-8 encoding, as required by the
    /// SSE specification.
    /// </remarks>
    /// <param name="preferDataAsUtf8Bytes">true if you intend to request the event
    /// data as UTF-8 bytes</param>
    /// <returns>the builder</returns>
    public ConfigurationBuilder PreferDataAsUtf8Bytes(bool preferDataAsUtf8Bytes)
    {
        _preferDataAsUtf8Bytes = preferDataAsUtf8Bytes;
        return this;
    }

    /// <summary>
    /// Sets the request headers to be sent with each EventSource HTTP request.
    /// </summary>
    /// <param name="headers">the headers (null is equivalent to an empty dictionary)</param>
    /// <returns>the builder</returns>
    public ConfigurationBuilder RequestHeaders(IDictionary<string, string> headers)
    {
        _requestHeaders = headers is null ? new Dictionary<string, string>() :
            new Dictionary<string, string>(headers);
        return this;
    }

    /// <summary>
    /// Adds a request header to be sent with each EventSource HTTP request.
    /// </summary>
    /// <param name="name">the header name</param>
    /// <param name="value">the header value </param>
    /// <returns>the builder</returns>
    public ConfigurationBuilder RequestHeader(string name, string value)
    {
        if (name != null)
        {
            _requestHeaders[name] = value;
        }
        return this;
    }

    /// <summary>
    /// Sets the <c>HttpMessageHandler</c> that will be used for the HTTP client, or null for the default handler.
    /// </summary>
    /// <remarks>
    /// If you have specified a custom HTTP client instance with <see cref="HttpClient"/>, then
    /// <see cref="HttpMessageHandler(HttpMessageHandler)"/> is ignored.
    /// </remarks>
    /// <param name="handler">the message handler implementation</param>
    /// <returns>the builder</returns>
    public ConfigurationBuilder HttpMessageHandler(HttpMessageHandler handler)
    {
        this._httpMessageHandler = handler;
        return this;
    }

    /// <summary>
    /// Specifies that EventSource should use a specific HttpClient instance for HTTP requests.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Normally, EventSource creates its own HttpClient and disposes of it when you dispose of the
    /// EventSource. If you provide your own HttpClient using this method, you are responsible for
    /// managing the HttpClient's lifecycle-- EventSource will not dispose of it.
    /// </para>
    /// <para>
    /// EventSource will not modify this client's properties, so if you call <see cref="HttpMessageHandler"/>
    /// or <see cref="ConnectionTimeout"/>, those methods will be ignored.
    /// </para>
    /// </remarks>
    /// <param name="client">an HttpClient instance, or null to use the default behavior</param>
    /// <returns>the builder</returns>
    public ConfigurationBuilder HttpClient(HttpClient client)
    {
        this._httpClient = client;
        return this;
    }

    /// <summary>
    /// Sets the HTTP method that will be used when connecting to the EventSource API.
    /// </summary>
    /// <remarks>
    /// By default, this is <see cref="HttpMethod.Get"/>.
    /// </remarks>
    public ConfigurationBuilder Method(HttpMethod method)
    {
        this._method = method ?? throw new ArgumentNullException(nameof(method));
        return this;
    }

    /// <summary>
    /// Sets a factory for HTTP request body content, if the HTTP method is one that allows a request body.
    /// </summary>
    /// <remarks>
    /// This is in the form of a factory function because the request may need to be sent more than once.
    /// </remarks>
    /// <param name="factory">the factory function, or null for none</param>
    /// <returns>the builder</returns>
    public ConfigurationBuilder RequestBodyFactory(Func<HttpContent> factory)
    {
        this._requestBodyFactory = factory;
        return this;
    }

    /// <summary>
    /// Equivalent <see cref="RequestBodyFactory(Func{HttpContent})"/>, but for content
    /// that is a simple string.
    /// </summary>
    /// <param name="bodyString">the content</param>
    /// <param name="contentType">the Content-Type header</param>
    /// <returns>the builder</returns>
    public ConfigurationBuilder RequestBody(string bodyString, string contentType)
    {
        return RequestBodyFactory(() => new StringContent(bodyString, Encoding.UTF8, contentType));
    }

    #endregion

    #region Private methods

    // Canonicalizes the value so all negative numbers become InfiniteTimeSpan
    private static TimeSpan TimeSpanCanBeInfinite(TimeSpan t) =>
        t < TimeSpan.Zero ? Timeout.InfiniteTimeSpan : t;

    // Replaces all negative times with zero
    private static TimeSpan FiniteTimeSpan(TimeSpan t) =>
        t < TimeSpan.Zero ? TimeSpan.Zero : t;

    #endregion
}