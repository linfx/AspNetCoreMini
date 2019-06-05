namespace AspNetCoreMini.Hosting.Extensions
{
    public static class Host
    {
        public static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            var builder = new HostBuilder();

            //builder.UseDefaultServiceProvider((context, options) =>
            //{
            //    var isDevelopment = context.HostingEnvironment.IsDevelopment();
            //    options.ValidateScopes = isDevelopment;
            //    options.ValidateOnBuild = isDevelopment;
            //});

            return builder;
        }
    }
}
