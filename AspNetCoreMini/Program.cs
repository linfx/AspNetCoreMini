namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseHttpListener()
                .Configure(app => app
                    .Use(FooMiddleware)
                    .Use(BarMiddleware)
                    .Use(BazMiddleware))
                .Build()
                .StartAsync().Wait();
        }

        public static RequestDelegate FooMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Foo=>");
            await next(context);
        };

        public static RequestDelegate BarMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Bar=>");
            await next(context);
        };

        public static RequestDelegate BazMiddleware(RequestDelegate next) => context => context.Response.WriteAsync("Baz");
    }
}
