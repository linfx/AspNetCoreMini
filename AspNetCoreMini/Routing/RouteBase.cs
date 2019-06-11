using System;
using System.Threading.Tasks;

namespace AspNetCoreMini.Routing
{
    public class RouteBase : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public Task RouteAsync(RouteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            throw new NotImplementedException();
        }
    }
}
