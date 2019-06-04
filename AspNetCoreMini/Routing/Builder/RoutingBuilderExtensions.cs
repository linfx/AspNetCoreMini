using AspNetCoreMini.Http;
using AspNetCoreMini.Http.Abstractions.Extensions;
using AspNetCoreMini.Routing.Abstractions;
using System;

namespace AspNetCoreMini.Routing.Builder
{
    public static class RoutingBuilderExtensions
    {
        /// <summary>
        /// Adds a <see cref="RouterMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/> with the specified <see cref="IRouter"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <param name="router">The <see cref="IRouter"/> to use for routing requests.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseRouter(this IApplicationBuilder builder, IRouter router)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (router == null)
            {
                throw new ArgumentNullException(nameof(router));
            }

            //if (builder.ApplicationServices.GetService(typeof(RoutingMarkerService)) == null)
            //{
            //    throw new InvalidOperationException(Resources.FormatUnableToFindServices(
            //        nameof(IServiceCollection),
            //        nameof(RoutingServiceCollectionExtensions.AddRouting),
            //        "ConfigureServices(...)"));
            //}

            return builder.UseMiddleware<RouterMiddleware>(router);
        }
    }
}
