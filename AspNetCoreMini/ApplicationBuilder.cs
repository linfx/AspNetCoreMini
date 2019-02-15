using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1
{
    /// <summary>
    /// 第四个对象：ApplicationBuilder
    /// </summary>
    public interface IApplicationBuilder
    {
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
        RequestDelegate Build();
    }

    public class ApplicationBuilder : IApplicationBuilder
    {
        /// <summary>
        /// 第三个对象：Middleware
        /// 中间件在ASP.NET Core被表示成一个Func<RequestDelegate, RequestDelegate>对象
        /// </summary>
        private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        public RequestDelegate Build()
        {
            _middlewares.Reverse();

            return httpContext =>
            {
                RequestDelegate next = _ => { _.Response.StatusCode = 404; return Task.CompletedTask; };
                foreach (var middleware in _middlewares)
                {
                    next = middleware(next);
                }
                return next(httpContext);
            };
        }

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}
