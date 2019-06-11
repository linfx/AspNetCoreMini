using AspNetCoreMini.Http;
using System;
using System.Collections.Generic;

namespace AspNetCoreMini.Routing.Builder
{
    public class RouteBuilder : IRouteBuilder
    {
        private IApplicationBuilder builder;

        public RouteBuilder(IApplicationBuilder builder)
        {
            this.builder = builder;
        }

        public IApplicationBuilder ApplicationBuilder => throw new System.NotImplementedException();

        public IRouter DefaultHandler { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IServiceProvider ServiceProvider => throw new System.NotImplementedException();

        public IList<IRouter> Routes => throw new System.NotImplementedException();

        public IRouter Build()
        {
            throw new System.NotImplementedException();
        }
    }
}