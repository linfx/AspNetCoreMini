using AspNetCoreMini.Routing.Abstractions;

namespace AspNetCoreMini.Routing
{
    internal class RoutingFeature
    {
        public RoutingFeature()
        {
        }

        public RouteData RouteData { get; set; }
    }
}