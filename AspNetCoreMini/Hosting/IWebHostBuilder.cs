using AspNetCoreMini.Http;
using System;

namespace WebApplication1
{
    /// <summary>
    /// 第七个对象：WebHostBuilder
    /// WebHostBuilder的使命非常明确：就是创建作为应用宿主的WebHost。
    /// </summary>
    public interface IWebHostBuilder
    {
        /// <summary>
        /// Builds an <see cref="IWebHost"/> which hosts a web application.
        /// </summary>
        /// <returns></returns>
        IWebHost Build();
        IWebHostBuilder UseServer(IServer server);
        IWebHostBuilder Configure(Action<IApplicationBuilder> configure);
    }
}
