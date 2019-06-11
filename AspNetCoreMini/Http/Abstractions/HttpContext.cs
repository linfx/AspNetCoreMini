using AspNetCoreMini.Http.Features;
using System;

namespace AspNetCoreMini.Http
{
    /// <summary>
    /// 第一个对象：HttpContext
    /// Encapsulates all HTTP-specific information about an individual HTTP request.
    /// HttpContext对象，它可以说是ASP.NET Core应用开发中使用频率最高的对象。
    /// 要说明HttpContext的本质，还得从请求处理管道的层面来讲。
    /// 对于由一个服务器和多个中间件构建的管道来说，面向传输层的服务器负责请求的监听、接收和最终的响应，当它接收到客户端发送的请求后，需要将它分发给后续中间件进行处理。
    /// 对于某个中间件来说，当我们完成了自身的请求处理任务之后，在大部分情况下也需要将请求分发给后续的中间件。
    /// 请求在服务器与中间件之间，以及在中间件之间的分发是通过共享上下文的方式实现的。
    /// </summary>
    public abstract class HttpContext
    {
        /// <summary>
        /// Gets the collection of HTTP features provided by the server and middleware available on this request.
        /// </summary>
        public abstract IFeatureCollection Features { get; }

        /// <summary>
        /// Gets the <see cref="HttpRequest"/> object for this request.
        /// </summary>
        public abstract HttpRequest Request { get; }

        /// <summary>
        /// Get the <see cref="HttpResponse"/> object for this request.
        /// </summary>
        public abstract HttpResponse Response { get; }

        /// <summary>
        /// Gets or sets the <see cref="IServiceProvider"/> that provides access to the request's service container.
        /// </summary>
        public abstract IServiceProvider RequestServices { get; set; }
    }
}
