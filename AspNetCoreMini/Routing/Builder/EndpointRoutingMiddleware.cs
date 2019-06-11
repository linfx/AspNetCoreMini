using AspNetCoreMini.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AspNetCoreMini.Routing.Builder
{
    internal class EndpointRoutingMiddleware
    {
        //private readonly MatcherFactory _matcherFactory;
        private readonly ILogger _logger;
        //private readonly EndpointDataSource _endpointDataSource;
        private readonly RequestDelegate _next;

        public Task Invoke(HttpContext httpContext)
        {
            // There's already an endpoint, skip maching completely
            var endpoint = httpContext.GetEndpoint();
            if (endpoint != null)
            {
                //Log.MatchSkipped(_logger, endpoint);
                return _next(httpContext);
            }

            // There's an inherent race condition between waiting for init and accessing the matcher
            // this is OK because once `_matcher` is initialized, it will not be set to null again.
            //var matcherTask = InitializeAsync();
            //if (!matcherTask.IsCompletedSuccessfully)
            //{
            //    return AwaitMatcher(this, httpContext, matcherTask);
            //}

            //var matchTask = matcherTask.Result.MatchAsync(httpContext);
            //if (!matchTask.IsCompletedSuccessfully)
            //{
            //    return AwaitMatch(this, httpContext, matchTask);
            //}

            //return SetRoutingAndContinue(httpContext);

            // Awaited fallbacks for when the Tasks do not synchronously complete
            //static async Task AwaitMatcher(EndpointRoutingMiddleware middleware, HttpContext httpContext, Task<Matcher> matcherTask)
            //{
            //    var matcher = await matcherTask;
            //    await matcher.MatchAsync(httpContext);
            //    await middleware.SetRoutingAndContinue(httpContext);
            //}

            static async Task AwaitMatch(EndpointRoutingMiddleware middleware, HttpContext httpContext, Task matchTask)
            {
                await matchTask;
                //await middleware.SetRoutingAndContinue(httpContext);
            }

            return Task.CompletedTask;
        }
    }
}