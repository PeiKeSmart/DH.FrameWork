# DH.AspNetCoreProxy

ASP.NET Core Proxies made easy.

## Information

### Examples

First, you must add the required services.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddProxies();
    ...
}
```

#### Run a Proxy

You can run a proxy over all endpoints by using `RunProxy` in your `Configure` method.

```csharp
app.RunProxy(proxy => proxy.UseHttp("http://google.com"));
```

In addition, you can route this proxy depending on the context.  You can return a `string` or `ValueTask<string>` from the computer.

```csharp
app.RunProxy(proxy => proxy
    .UseHttp((context, args) =>
    {
        if(context.Request.Path.StartsWithSegments("/should/forward/to/favorite"))
            return "http://myfavoriteserver.com";

        return "http://myhttpserver.com";
    })
    .UseWs((context, args) => "ws://mywsserver.com"));
```

#### Routes At Startup

You can define mapped proxy routes in your `Configure` method at startup.

```csharp
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseProxies(proxies =>
{
    // Bare string.
    proxies.Map("echo/post", proxy => proxy.UseHttp("https://postman-echo.com/post"));

    // Computed to task.
    proxies.Map("api/comments/task/{postId}", proxy => proxy.UseHttp((_, args) => new ValueTask<string>($"https://jsonplaceholder.typicode.com/comments/{args["postId"]}")));

    // Computed to string.
    proxies.Map("api/comments/string/{postId}", proxy => proxy.UseHttp((_, args) => $"https://jsonplaceholder.typicode.com/comments/{args["postId"]}"));
});
```

#### Route At Startup with Custom HttpClientHandler

ASP.NET Core allows you to configure the behavior of its HTTP client objects by registering a named HttpClient with its own HttpClientHandler, which can then be referred to by name elsewhere.  This can be used to support features such as server certificate custom validation.  The UseProxies setup supports using such a named client:

```csharp
proxies.Map( "/api/v1/...", proxy => proxy.UseHttp( 
    (context, args) => ...,
    builder => builder.WithHttpClientName("myClientName")));
```
...where "myClientName" was previously registered as:
```csharp
services
    .AddHttpClient("myClientName")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() {
        ServerCertificateCustomValidationCallback = MyValidateCertificateMethod,
        UseDefaultCredentials = true
    });
```

#### Route All Unhandled Requests

You can also route all unhandled requests to another server, e.g.

```csharp
app.UseStatusCodePages(async statusCodeContext =>
{
    var context = statusCodeContext.HttpContext;
    if (context.Response.StatusCode == StatusCodes.Status404NotFound)
    {
        var request = context.Features.Get<IHttpRequestFeature>();
        if (request != null)
        {
            await context.HttpProxyAsync($"https://example.com{request.RawTarget}");
        }
    }
});
```

#### Existing Controller

You can define a proxy over a specific endpoint on an existing `Controller` by leveraging the `ProxyAsync` extension methods.

```csharp
public class MyController : Controller
{
    [Route("api/posts/{postId}")]
    public Task GetPosts(int postId)
    {
        return this.HttpProxyAsync($"https://jsonplaceholder.typicode.com/posts/{postId}");
    }
}
```

> NOTE: The body of the request should not be consumed by the controller (i.e., the controller should not have any `[FromBody]` parameters); 
> otherwise, the proxy operation will fail.  This is due to the fact that the body is read from a `Stream`, and that `Stream` is progressed 
> when the body is read.

You can "catch all" using ASP.NET `**rest` semantics.

```csharp
[Route("api/google/{**rest}")]
public Task ProxyCatchAll(string rest)
{
    // If you don't need the query string, then you can remove this.
    var queryString = this.Request.QueryString.Value;
    return this.HttpProxyAsync($"https://google.com/{rest}{queryString}");
}
```

In addition, you can proxy to WebSocket endpoints.

```csharp
public class MyController : Controller
{
    [Route("ws")]
    public Task OpenWs()
    {
        return this.WsProxyAsync($"wss://mywsendpoint.com/ws");
    }
}
```

#### Uber Example

You can also pass special options that apply when the proxy operation occurs.

```csharp
public class MyController : Controller
{
    private HttpProxyOptions _httpOptions = HttpProxyOptionsBuilder.Instance
        .WithShouldAddForwardedHeaders(false)
        .WithHttpClientName("MyCustomClient")
        .WithIntercept(async context =>
        {
            if(context.Connection.RemotePort == 7777)
            {
                context.Response.StatusCode = 300;
                await context.Response.WriteAsync("I don't like this port, so I am not proxying this request!");
                return true;
            }

            return false;
        })
        .WithBeforeSend((c, hrm) =>
        {
            // Set something that is needed for the downstream endpoint.
            hrm.Headers.Authorization = new AuthenticationHeaderValue("Basic");

            return Task.CompletedTask;
        })
        .WithAfterReceive((c, hrm) =>
        {
            // Alter the content in  some way before sending back to client.
            var newContent = new StringContent("It's all greek...er, Latin...to me!");
            hrm.Content = newContent;

            return Task.CompletedTask;
        })
        .WithHandleFailure(async (c, e) =>
        {
            // Return a custom error response.
            c.Response.StatusCode = 403;
            await c.Response.WriteAsync("Things borked.");
        }).Build();

    private WsProxyOptions _wsOptions = WsProxyOptionsBuilder.Instance
        .WithBufferSize(8192)
        .WithIntercept(context => new ValueTask<bool>(context.WebSockets.WebSocketRequestedProtocols.Contains("interceptedProtocol")))
        .WithBeforeConnect((context, wso) =>
        {
            wso.AddSubProtocol("myRandomProto");
            return Task.CompletedTask;
        })
        .WithHandleFailure(async (context, e) =>
        {
            context.Response.StatusCode = 599;
            await context.Response.WriteAsync("Failure handled.");
        }).Build();
    
    [Route("api/posts/{postId}")]
    public Task GetPosts(int postId)
    {
        return this.ProxyAsync("http://myhttpendpoint.com", "ws://mywsendpoint.com", _httpOptions, _wsOptions);
    }
}
```

## License

```
The MIT License (MIT)

Copyright (c) 2017 Aaron Roney

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
