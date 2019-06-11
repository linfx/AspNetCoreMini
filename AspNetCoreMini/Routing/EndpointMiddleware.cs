using AspNetCoreMini.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AspNetCoreMini.Routing
{
    public class EndpointMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        //private readonly RouteOptions _routeOptions;

        public EndpointMiddleware(
            ILogger<EndpointMiddleware> logger,
            RequestDelegate next
            //RouteOptions routeOptions
            )
        {
            _logger = logger;
            _next = next;
            //_routeOptions = routeOptions;
        }

        public Task InvokeAsync(HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();
            if (endpoint?.RequestDelegate != null)
            {
                //if (!_routeOptions.SuppressCheckForUnhandledSecurityMetadata)
                //{
                //    if (endpoint.Metadata.GetMetadata<IAuthorizeData>() != null && !httpContext.Items.ContainsKey(AuthorizationMiddlewareInvokedKey))
                //    {
                //        //ThrowMissingAuthMiddlewareException(endpoint);
                //    }

                //    if (endpoint.Metadata.GetMetadata<ICorsMetadata>() != null && !httpContext.Items.ContainsKey(CorsMiddlewareInvokedKey))
                //    {
                //        //ThrowMissingCorsMiddlewareException(endpoint);
                //    }
                //}

                //Log.ExecutingEndpoint(_logger, endpoint);

                try
                {
                    var requestTask = endpoint.RequestDelegate(httpContext);
                    if (!requestTask.IsCompletedSuccessfully)
                    {
                        return AwaitRequestTask(endpoint, requestTask, _logger);
                    }
                }
                catch (Exception exception)
                {
                    //Log.ExecutedEndpoint(_logger, endpoint);
                    return Task.FromException(exception);
                }

                //Log.ExecutedEndpoint(_logger, endpoint);
                return Task.CompletedTask;
            }
            return _next(httpContext);

            static async Task AwaitRequestTask(Endpoint endpoint, Task requestTask, ILogger logger)
            {
                try
                {
                    await requestTask;
                }
                finally
                {
                    //Log.ExecutedEndpoint(logger, endpoint);
                }
            }
        }
    }
}
