using System.Threading.Tasks;

namespace WebApplication1
{
    /// <summary>
    /// 第二个对象：RequestDelegate
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task RequestDelegate(HttpContext context);
}
