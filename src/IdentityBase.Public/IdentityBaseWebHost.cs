namespace IdentityBase.Public
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public static class IdentityBaseWebHost
    {
        public static void Run(string[] args)
        {
            IdentityBaseWebHost.Run<Startup>(
                args,
                Directory.GetCurrentDirectory());
        }

        /// <summary>
        /// Use this if you start identitybase from custom project 
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <param name="basePath"></param>
        public static void Run<TStartup>(
            string[] args,
            string basePath)
            where TStartup : class
        {
            IConfiguration config = IdentityBaseWebHost
                .LoadConfig<TStartup>(args, basePath); 

            // Use in case you changed the example data in ExampleData.cs file
            // Configuration.ExampleDataWriter.Write(config); 

            IConfigurationSection configHost = config.GetSection("Host");

            IWebHostBuilder hostBuilder = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(configHost.GetValue<string>("Urls"))
                .UseContentRoot(basePath)
                .UseConfiguration(config)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddSerilog(hostingContext.Configuration);
                })
                .UseStartup<TStartup>();

            if (configHost.GetValue<bool>("UseIISIntegration"))
            {
                Console.WriteLine("Enabling IIS Integration");
                hostBuilder = hostBuilder.UseIISIntegration();
            }

            hostBuilder
                .Build()
                .Run();
        }

        private static IConfigurationRoot LoadConfig<TStartup>(
            string[] args,
            string basePath)
            where TStartup : class
        {
            bool isDevelopment = IdentityBaseWebHost.IsDevelopment();

            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("./AppData/config.json", false, false);

            if (isDevelopment)
            {
                configBuilder.AddJsonFile(
                    "./AppData/config.development.json",
                    false,
                    false
                );

                configBuilder.AddUserSecrets<TStartup>();
            }

            configBuilder.AddEnvironmentVariables();

            if (args != null)
            {
                configBuilder.AddCommandLine(args);
            }

            return configBuilder.Build();
        }

        private static bool IsDevelopment()
        {
            return Environment
                .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                .Equals("Development", StringComparison.OrdinalIgnoreCase);
        }
    }
}