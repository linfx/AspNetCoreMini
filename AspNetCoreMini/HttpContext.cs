using System;
using System.Collections.Specialized;
using System.IO;

namespace WebApplication1
{
    /// <summary>
    /// 第一个对象：HttpContext
    /// HttpContext对象，它可以说是ASP.NET Core应用开发中使用频率最高的对象。
    /// 要说明HttpContext的本质，还得从请求处理管道的层面来讲。
    /// 对于由一个服务器和多个中间件构建的管道来说，面向传输层的服务器负责请求的监听、接收和最终的响应，当它接收到客户端发送的请求后，需要将它分发给后续中间件进行处理。
    /// 对于某个中间件来说，当我们完成了自身的请求处理任务之后，在大部分情况下也需要将请求分发给后续的中间件。
    /// 请求在服务器与中间件之间，以及在中间件之间的分发是通过共享上下文的方式实现的。
    /// </summary>
    public class HttpContext
    {
        /// <summary>
        /// 请求
        /// </summary>
        public HttpRequest Request { get; }
        /// <summary>
        /// 响应
        /// </summary>
        public HttpResponse Response { get; }

        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }
    }

    public class HttpRequest
    {
        private readonly IHttpRequestFeature _feature;
        public Uri Url => _feature.Url;
        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;

        public HttpRequest(IFeatureCollection features) => _feature = features.Get<IHttpRequestFeature>();
    }

    public class HttpResponse
    {
        private readonly IHttpResponseFeature _feature;
        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;
        public int StatusCode { get => _feature.StatusCode; set => _feature.StatusCode = value; }

        public HttpResponse(IFeatureCollection features) => _feature = features.Get<IHttpResponseFeature>();
    }

    public interface IHttpRequestFeature
    {
        Uri Url { get; }
        NameValueCollection Headers { get; }
        Stream Body { get; }
    }

    public interface IHttpResponseFeature
    {
        int StatusCode { get; set; }
        NameValueCollection Headers { get; }
        Stream Body { get; }
    }
}
