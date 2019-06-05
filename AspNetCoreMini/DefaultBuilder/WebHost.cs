using AspNetCoreMini.Hosting;

namespace AspNetCoreMini
{
    public static class WebHost
    {
        public static IWebHostBuilder CreateDefaultBuilder() => CreateDefaultBuilder(args: null);

        public static IWebHostBuilder CreateDefaultBuilder(string[] args)
        {
            var builder = new WebHostBuilder();

            //if (string.IsNullOrEmpty(builder.GetSetting(WebHostDefaults.ContentRootKey)))
            //{
            //    builder.UseContentRoot(Directory.GetCurrentDirectory());
            //}
            //if (args != null)
            //{
            //    builder.UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build());
            //}

            //builder.ConfigureAppConfiguration((hostingContext, config) =>
            //{
            //    var env = hostingContext.HostingEnvironment;

            //    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            //    if (env.IsDevelopment())
            //    {
            //        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
            //        if (appAssembly != null)
            //        {
            //            config.AddUserSecrets(appAssembly, optional: true);
            //        }
            //    }

            //    config.AddEnvironmentVariables();

            //    if (args != null)
            //    {
            //        config.AddCommandLine(args);
            //    }
            //})
            //.ConfigureLogging((hostingContext, logging) =>
            //{
            //    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            //    logging.AddConsole();
            //    logging.AddDebug();
            //    logging.AddEventSourceLogger();
            //}).
            //UseDefaultServiceProvider((context, options) =>
            //{
            //    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
            //});

            ConfigureWebDefaults(builder);

            return builder;
        }


        internal static void ConfigureWebDefaults(IWebHostBuilder builder)
        {
            builder
            //builder.ConfigureAppConfiguration((ctx, cb) =>
            //{
            //    if (ctx.HostingEnvironment.IsDevelopment())
            //    {
            //        StaticWebAssetsLoader.UseStaticWebAssets(ctx.HostingEnvironment);
            //    }
            //});
            //builder.UseKestrel((builderContext, options) =>
            //{
            //    options.Configure(builderContext.Configuration.GetSection("Kestrel"));
            //})
            .ConfigureServices((hostingContext, services) =>
            {
                // Fallback
                //services.PostConfigure<HostFilteringOptions>(options =>
                //{
                //    if (options.AllowedHosts == null || options.AllowedHosts.Count == 0)
                //    {
                //        // "AllowedHosts": "localhost;127.0.0.1;[::1]"
                //        var hosts = hostingContext.Configuration["AllowedHosts"]?.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                //        // Fall back to "*" to disable.
                //        options.AllowedHosts = (hosts?.Length > 0 ? hosts : new[] { "*" });
                //    }
                //});
                // Change notification
                //services.AddSingleton<IOptionsChangeTokenSource<HostFilteringOptions>>(
                //            new ConfigurationChangeTokenSource<HostFilteringOptions>(hostingContext.Configuration));

                //services.AddTransient<IStartupFilter, HostFilteringStartupFilter>();

                //if (string.Equals("true", hostingContext.Configuration["ForwardedHeaders_Enabled"], StringComparison.OrdinalIgnoreCase))
                //{
                //    services.Configure<ForwardedHeadersOptions>(options =>
                //    {
                //        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                //        // Only loopback proxies are allowed by default. Clear that restriction because forwarders are
                //        // being enabled by explicit configuration.
                //        options.KnownNetworks.Clear();
                //        options.KnownProxies.Clear();
                //    });

                //    services.AddTransient<IStartupFilter, ForwardedHeadersStartupFilter>();
                //}

                //services.AddRouting();
            });
        }
    }
}
