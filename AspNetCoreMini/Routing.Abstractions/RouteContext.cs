using AspNetCoreMini.Http;
using System;

namespace AspNetCoreMini.Routing.Abstractions
{
    /// <summary>
    /// A context object for <see cref="IRouter.RouteAsync(RouteContext)"/>.
    /// </summary>
    public class RouteContext
    {
        private RouteData _routeData;

        /// <summary>
        /// Creates a new instance of <see cref="RouteContext"/> for the provided <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">The <see cref="Http.HttpContext"/> associated with the current request.</param>
        public RouteContext(HttpContext httpContext)
        {
            HttpContext = httpContext;
            RouteData = new RouteData();
        }

        /// <summary>
        /// Gets or sets the handler for the request. An <see cref="IRouter"/> should set <see cref="Handler"/>
        /// when it matches.
        /// </summary>
        public RequestDelegate Handler { get; set; }

        /// <summary>
        /// Gets the <see cref="Http.HttpContext"/> associated with the current request.
        /// </summary>
        public HttpContext HttpContext { get; }

        /// <summary>
        /// Gets or sets the <see cref="Routing.RouteData"/> associated with the current context.
        /// </summary>
        public RouteData RouteData
        {
            get
            {
                return _routeData;
            }
            set
            {
                _routeData = value ?? throw new ArgumentNullException(nameof(RouteData));
            }
        }
    }
}