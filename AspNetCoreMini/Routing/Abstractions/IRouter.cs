using System.Threading.Tasks;

namespace AspNetCoreMini.Routing
{
    public interface IRouter
    {
        Task RouteAsync(RouteContext context);

        VirtualPathData GetVirtualPath(VirtualPathContext context);
    }
}
