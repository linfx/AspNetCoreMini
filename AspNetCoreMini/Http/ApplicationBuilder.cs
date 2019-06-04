using AspNetCoreMini.Http.Abstractions.Routing;
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

        public ApplicationBuilder(IServiceProvider serviceProvider)
        {
            ApplicationServices = serviceProvider;
        }

        public IServiceProvider ApplicationServices { get; set; }

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _components.Add(middleware);
            return this;
        }

        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                // If we reach the end of the pipeline, but we have an endpoint, then something unexpected has happened.
                // This could happen if user code sets an endpoint, but they forgot to add the UseEndpoint middleware.
                var endpoint = context.GetEndpoint();
                var endpointRequestDelegate = endpoint?.RequestDelegate;
                //if (endpointRequestDelegate != null)
                //{
                //    var message =
                //        $"The request reached the end of the pipeline without executing the endpoint: '{endpoint.DisplayName}'. " +
                //        $"Please register the EndpointMiddleware using '{nameof(IApplicationBuilder)}.UseEndpoints(...)' if using " +
                //        $"routing.";
                //    throw new InvalidOperationException(message);
                //}

                context.Response.StatusCode = 404;
                return Task.CompletedTask;
            };

            foreach (var component in _components.Reverse())
            {
                app = component(app);
            }

            return app;
        }
    }
}
