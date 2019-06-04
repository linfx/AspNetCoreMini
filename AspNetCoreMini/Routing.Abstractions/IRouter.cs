using System.Threading.Tasks;

namespace AspNetCoreMini.Routing.Abstractions
{
    public interface IRouter
    {
        Task RouteAsync(RouteContext context);

        VirtualPathData GetVirtualPath(VirtualPathContext context);
    }
}
