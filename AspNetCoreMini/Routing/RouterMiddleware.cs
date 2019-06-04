using AspNetCoreMini.Http;
using AspNetCoreMini.Routing.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMini.Routing
{
    public class RouterMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly IRouter _router;

        public RouterMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory,
            IRouter router)
        {
            _next = next;
            _router = router;

            _logger = loggerFactory.CreateLogger<RouterMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var context = new RouteContext(httpContext);
            //context.RouteData.Routers.Add(_router);

            await _router.RouteAsync(context);

            if (context.Handler == null)
            {
                //_logger.RequestNotMatched();
                await _next.Invoke(httpContext);
            }
            else
            {
                var routingFeature = new RoutingFeature()
                {
                    RouteData = context.RouteData
                };

                // Set the RouteValues on the current request, this is to keep the IRouteValuesFeature inline with the IRoutingFeature
                //httpContext.Request.RouteValues = context.RouteData.Values;
                //httpContext.Features.Set<IRoutingFeature>(routingFeature);

                await context.Handler(context.HttpContext);
            }
        }
    }
}
