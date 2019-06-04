using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMini.Http
{

    public class ApplicationBuilder : IApplicationBuilder
    {
        /// <summary>
        /// 第三个对象：Middleware
        /// 中间件在ASP.NET Core被表示成一个Func<RequestDelegate, RequestDelegate>对象
        /// </summary>
        private readonly IList<Func<RequestDelegate, RequestDelegate>> _components = new List<Func<RequestDelegate, RequestDelegate>>();

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _components.Add(middleware);
            return this;
        }

        public RequestDelegate Build()
        {
            _components.Reverse();

            return httpContext =>
            {
                RequestDelegate next = _ => { _.Response.StatusCode = 404; return Task.CompletedTask; };
                foreach (var middleware in _components)
                {
                    next = middleware(next);
                }
                return next(httpContext);
            };
        }
    }
}
