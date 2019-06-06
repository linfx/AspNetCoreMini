using Microsoft.Extensions.Configuration;

namespace AspNetCoreMini.Extensions.Hosting
{
    public static class Host
    {
        public static IHostBuilder CreateDefaultBuilder() => CreateDefaultBuilder(args: null);

        public static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            var builder = new HostBuilder();

            builder.ConfigureHostConfiguration(config =>
            {
                config.AddEnvironmentVariables(prefix: "DOTNET_");
                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            });

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
